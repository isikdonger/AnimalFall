using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.Impl;
using System.Threading.Tasks;
using System;
using Firebase;
using Firebase.Firestore;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public static class GooglePlayServicesManager
{
    private static bool isInitialized = false;

    /// <summary>
    /// Initialize Google Play Services. Call this once in the first scene.
    /// </summary>
    public static void Initialize()
    {
        if (isInitialized) return; // Prevent re-initialization

        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
        .RequestIdToken() // Request ID token for Firebase Authentication
        .EnableSavedGames()  // Optional, if you want cloud saves
        .Build();

        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;  // Enable debugging (remove in production)
        PlayGamesPlatform.Activate();

        SignIn();
        isInitialized = true;
    }

    /// <summary>
    /// Sign in to Google Play Games.
    /// </summary>
    private static void SignIn()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                Debug.Log("Google Play Games sign-in successful.");
                string idToken = PlayGamesPlatform.Instance.GetIdToken();
                FirestoreManager.AuthenticateWithFirebase(idToken);
            }
            else
            {
                Debug.LogError("Google Play Games sign-in failed.");
            }
        });
    }

    /// <summary>
    /// Report a score to the leaderboard.
    /// </summary>
    public static async void ReportScore(long score)
    {
        if (Social.localUser.authenticated)
        {
            string leaderboardID = await FirestoreManager.GetFieldValue<string>("PlayServicesID", "PlayServicesID", "Leaderboard");
            Social.ReportScore(score, leaderboardID, (bool success) =>
            {
                Debug.Log(success ? "Score reported successfully!" : "Failed to report score.");
            });
        }
    }

    /// <summary>
    /// Show the leaderboard UI.
    /// </summary>
    public static void ShowLeaderboard()
    {
        if (Social.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI();
        }
        else
        {
            Debug.LogWarning("User is not authenticated! Cannot show leaderboard.");
            GameObject.Find("Tap to Start").GetComponent<Text>().text = "Sign in to view achievements";
        }
    }

    /// <summary>
    /// Get the player's current rank on a leaderboard.
    /// </summary>
    /// <returns>Rank of the player</returns>
    /// <summary>
    /// Get the player's current rank on a leaderboard.
    /// </summary>
    /// <returns>A Task that will return the player's rank, or -1 if the operation fails.</returns>
    public static async Task<int> GetLeaderboardRankAsync()
    {
        if (!Social.localUser.authenticated)
        {
            Debug.LogWarning("User is not authenticated.");
            return -1; // -1 to indicate failure due to authentication issue
        }

        // Retrieve the leaderboard ID asynchronously
        Task<string> leaderboardIDTask = FirestoreManager.GetFieldValue<string>("PlayServicesID", "PlayServicesID", "Leaderboard");
        string leaderboardID = await leaderboardIDTask;

        if (leaderboardIDTask.IsFaulted)
        {
            Debug.LogError("Failed to retrieve leaderboard ID: " + leaderboardIDTask.Exception);
            return -1;
        }

        // Load the scores for the leaderboard
        TaskCompletionSource<int> tcs = new TaskCompletionSource<int>();

        PlayGamesPlatform.Instance.LoadScores(
            leaderboardID,
            LeaderboardStart.PlayerCentered,
            1,
            LeaderboardCollection.Public,
            LeaderboardTimeSpan.AllTime,
            (LeaderboardScoreData data) =>
            {
                if (data.Valid)
                {
                    Debug.Log($"User Rank: {data.PlayerScore.rank}");
                    tcs.SetResult(data.PlayerScore.rank);
                }
                else
                {
                    Debug.LogWarning("Failed to load leaderboard rank.");
                    tcs.SetResult(-1); // -1 to indicate failure
                }
            }
        );

        return await tcs.Task; // Wait for the result and return it
    }


    /// <summary>
    /// Show the achievements UI.
    /// </summary>
    public static void ShowAchievements()
    {
        if (Social.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ShowAchievementsUI();
        }
        else
        {
            Debug.LogWarning("User is not authenticated! Cannot show achievements.");
            GameObject.Find("Tap to Start").GetComponent<Text>().text = "Sign in to view achievements";
        }
    }

    /// <summary>
    /// Unlock an achievement using a coroutine.
    /// </summary>
    public static IEnumerator UnlockAchievementCoroutine(string achievementName)
    {
        if (Social.localUser.authenticated)
        {
            // Retrieve the achievement ID asynchronously
            Task<string> task = FirestoreManager.GetFieldValue<string>("PlayServicesID", "PlayServicesID", achievementName);
            yield return new WaitUntil(() => task.IsCompleted);  // Wait for the task to complete

            if (task.IsFaulted)
            {
                Debug.LogError("Failed to retrieve achievement ID: " + task.Exception);
                yield break;
            }

            string achievementID = task.Result;

            // Report the achievement progress
            Social.ReportProgress(achievementID, 100.0f, (bool success) =>
            {
                Debug.Log(success ? "Achievement unlocked!" : "Failed to unlock achievement.");
            });
        }
    }

    /// <summary>
    /// Checks if an achievement is unlocked and calls the callback with the result.
    /// </summary>
    /// <param name="achievementName">The name of the achievement.</param>
    /// <param name="callback">The callback to call with the result (true if unlocked, false otherwise).</param>
    public static void IsAchievementUnlocked(string achievementName, Action<bool> callback)
    {
        // Retrieve the achievement ID from Firestore
        FirestoreManager.GetFieldValue<string>("PlayServicesID", "PlayServicesID", achievementName)
            .ContinueWith(task =>
            {
                if (task.IsFaulted || string.IsNullOrEmpty(task.Result))
                {
                    Debug.LogError($"Failed to retrieve achievement ID for '{achievementName}'.");
                    callback?.Invoke(false);
                    return;
                }

                string achievementID = task.Result;

                // Check if the achievement is unlocked
                Social.LoadAchievements(achievements =>
                {
                    if (achievements == null)
                    {
                        Debug.LogError("Failed to load achievements.");
                        callback?.Invoke(false);
                        return;
                    }

                    foreach (IAchievement achievement in achievements)
                    {
                        if (achievement.id == achievementID && achievement.completed)
                        {
                            Debug.Log($"Achievement '{achievementName}' is already unlocked.");
                            callback?.Invoke(true);
                            return;
                        }
                    }

                    // Achievement is not unlocked
                    callback?.Invoke(false);
                });
            });
    }

    /// <summary>
    ///  Increment Objectives
    /// </summary>
    public static void IncrementObjectives()
    {
        if (Social.localUser.authenticated)
        {
           
        }
    }
}