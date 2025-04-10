using UnityEngine;
using Apple.GameKit;
using System;
using System.Threading.Tasks;
using Apple.GameKit.Leaderboards;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SocialPlatforms.GameCenter;
using System.Collections;
using GooglePlayGames.BasicApi;
using Apple.Core.Runtime;

#if UNITY_IOS
public static class GameCenterManager
{
    public static async Task<bool> Initialize()
    {
        try
        {
            var localPlayer = await GKLocalPlayer.Authenticate();
            Debug.Log("Player authenticated: " + localPlayer.DisplayName);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError("Authentication failed: " + e.Message);
            return false;
        }
    }

    public static async void ReportScore(long score)
    {
        if (GKLocalPlayer.Local.IsAuthenticated)
        {
            try
            {
                string leaderboardID = "";
                var leaderboard = await GKLeaderboard.LoadLeaderboards(leaderboardID);
                await leaderboard[0].SubmitScore(score, 0, GKLocalPlayer.Local);
                Debug.Log("Score reported: " + score);
            }
            catch (Exception e)
            {
                Debug.LogError("Failed to report score: " + e.Message);
            }
        }
    }

    public static async void ShowLeaderboard()
    {
        if (GKLocalPlayer.Local.IsAuthenticated)
        {
            var gameCenter = GKGameCenterViewController.Init(GKGameCenterViewController.GKGameCenterViewControllerState.Leaderboards);
            await gameCenter.Present();
        }
        else
        {
            Debug.LogWarning("Player is not authenticated. Cannot show leaderboard UI.");
        }
    }

    /*public static async Task<int> GetLeaderboardRankAsync()
    {
        // Ensure the local player is authenticated
        if (!GKLocalPlayer.Local.IsAuthenticated)
        {
            Debug.LogError("Local player is not authenticated.");
            return -1; // Indicate an error
        }

        // Load the specified leaderboard
        string leaderboardID = "";
        var leaderboard = await GKLeaderboard.LoadLeaderboards(leaderboardID);
        if (leaderboard == null)
        {
            Debug.LogError($"Leaderboard with ID {leaderboardID} not found.");
            return -1;
        }

        // Load the local player's entry
        var result = await leaderboard.LoadEntriesAsync(
            GKLeaderboard.PlayerScope.Global,
            GKLeaderboard.TimeScope.AllTime,
            new NSRange(1, 1)
        );

        var localPlayerEntry = result.LocalPlayerEntry;

        if (localPlayerEntry == null)
        {
            Debug.Log("No entry found for local player on the leaderboard.");
            return -1; // Indicate no rank
        }

        return (int)localPlayerEntry.Rank;
    }*/

    public static async Task<GKAchievement> GetAchievement(string achievementID)
    {
        var achievements = await GKAchievement.LoadAchievements();
        
        foreach (var achievement in achievements)
        {
            if (achievement.Identifier == achievementID)
            {
                return achievement;
            }
        }

        return null;
    }

    public static IEnumerator UnlockAchievementCoroutine(string achievementName)
    {
        if (!GKLocalPlayer.Local.IsAuthenticated)
        {
            Debug.LogWarning("Player is not authenticated. Cannot unlock achievement.");
            yield break;
        }

        string achievementID = ""; // Use the achievement name as the ID
        GKAchievement achievement = GetAchievement(achievementID).Result;
        achievement ??= GKAchievement.Init(achievementID);

        if (!achievement.IsCompleted)
        {
            achievement.PercentComplete = 100;
            achievement.ShowCompletionBanner = true;
            GKAchievement.Report(achievement);
            LocalBackupManager.IncrementCompletedAchievements();
        }
    }

    public static async void ShowAchievements()
    {
        if (GKLocalPlayer.Local.IsAuthenticated)
        {
            var gameCenter = GKGameCenterViewController.Init(GKGameCenterViewController.GKGameCenterViewControllerState.Achievements);
            await gameCenter.Present();
        }
        else
        {
            Debug.LogWarning("Player is not authenticated. Cannot show achievements UI.");
        }
    }
}
#endif
