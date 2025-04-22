#if UNITY_ANDROID
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System.Threading.Tasks;
using System;
using System.Collections;
using UnityEngine.UI;
using System.Reflection;

public static class GooglePlayServicesManager
{
    private static bool isInitialized = false;
    private static TaskCompletionSource<bool> initializationTaskSource;

    /// <summary>
    /// Initialize Google Play Services. Call this once in the first scene.
    /// </summary>
    public static async Task<bool> Initialize()
    {
        if (isInitialized)
        {
            return true;
        }

        initializationTaskSource = new TaskCompletionSource<bool>();

        try
        {
            PlayGamesPlatform.Activate();
            PlayGamesPlatform.Instance.Authenticate(status =>
            {
                if (status == SignInStatus.Success)
                {
                    Debug.Log("Google Play Games sign-in successful.");
                    initializationTaskSource.TrySetResult(true);
                }
                else
                {
                    Debug.LogError("Google Play Games sign-in failed.");
                    initializationTaskSource.TrySetException(new Exception("Google Play Games sign-in failed."));
                }
            });
            // Await authentication result
            bool success = await initializationTaskSource.Task;

            isInitialized = success;
            return success;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Google Play initialization failed: {ex.Message}");
            throw; // Rethrow exception to be caught in the caller
        }
    }

    /// <summary>
    /// Report a score to the leaderboard.
    /// </summary>
    public static void ReportScore(long score)
    {
        if (PlayGamesPlatform.Instance.IsAuthenticated())
        {
            string leaderboardID = GPGSIds.leaderboard_score;
            PlayGamesPlatform.Instance.ReportScore(score, leaderboardID, (bool success) =>
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
        if (PlayGamesPlatform.Instance.IsAuthenticated())
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI();
            Debug.Log("Showing leaderboard...");
        }
        else
        {
            Debug.LogWarning("User is not authenticated! Cannot show leaderboard.");
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
        if (!PlayGamesPlatform.Instance.IsAuthenticated())
        {
            Debug.LogWarning("User is not authenticated.");
            return -1; // -1 to indicate failure due to authentication issue
        }

        // Retrieve the leaderboard ID asynchronously
        string leaderboardID = GPGSIds.leaderboard_score;

        if (string.IsNullOrEmpty(leaderboardID))
        {
            Debug.LogError("Failed to retrieve leaderboard ID: ");
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

    public static string GetAchievementID(string achievementName)
    {
        FieldInfo[] fileds = typeof(GPGSIds).GetFields(BindingFlags.Public | BindingFlags.Static);

        foreach (FieldInfo field in fileds)
        {
            if (field.Name == achievementName)
            {
                return field.GetValue(null).ToString();
            }
        }

        return null;
    }

    /// <summary>
    /// Unlock an achievement using a coroutine.
    /// </summary>
    public static IEnumerator UnlockAchievementCoroutine(string achievementName)
    {
        Debug.Log("Unlocking achievement: " + achievementName);
        if (PlayGamesPlatform.Instance.IsAuthenticated())
        {
            // Retrieve the achievement ID
            string achievementID = GetAchievementID("achievement_" + achievementName.ToLower().Replace(" ", "_"));
            Debug.Log("Achievement ID: " + achievementID);

            if (string.IsNullOrEmpty(achievementID))
            {
                yield break;
            }

            // Report the achievement progress
            PlayGamesPlatform.Instance.ReportProgress(achievementID, 100.0f, (bool success) =>
            {
                Debug.Log(success ? "Achievement unlocked!" : "Failed to unlock achievement.");

                if (success)
                {
                    Debug.Log("Achievement Unlocked! Incrementing completed achievements...");
                    LocalBackupManager.IncrementCompletedAchievements();
                }
            });
        }
    }

    /// <summary>
    /// Show the achievements UI.
    /// </summary>
    public static void ShowAchievements()
    {
        if (PlayGamesPlatform.Instance.IsAuthenticated())
        {
            PlayGamesPlatform.Instance.ShowAchievementsUI();
            Debug.Log("Showing achievements...");
        }
        else
        {
            Debug.LogWarning("User is not authenticated! Cannot show achievements.");
        }
    }

    /// <summary>
    ///  Increment Objectives
    /// </summary>
    public static IEnumerator IncrementObjectiveCoroutine(string objectiveName)
    {
        if (PlayGamesPlatform.Instance.IsAuthenticated())
        {
            string achievementID = GetAchievementID("achievement_" + objectiveName.ToLower().Replace(" ", "_"));
            Debug.Log("Incrementing objective: " + achievementID);
            if (string.IsNullOrEmpty(achievementID))
            {
                Debug.LogError("Failed to retrieve achievement ID.");
                yield break;
            }
            PlayGamesPlatform.Instance.IncrementAchievement(achievementID, 1, (bool success) =>
            {
                Debug.Log(success ? "Objective incremented!" : "Failed to increment objective.");
            });
        }
    }
}
#endif