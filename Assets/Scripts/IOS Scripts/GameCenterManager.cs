#if UNITY_IOS
using UnityEngine;
using Apple.GameKit;
using System;
using System.Threading.Tasks;
using Apple.GameKit.Leaderboards;
using System.Collections;
using Apple.Core.Runtime;
using UnityEngine.SocialPlatforms.Impl;
using System.Linq;

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

    public static void ReportScore(int score)
    {
        if (GKLocalPlayer.Local.IsAuthenticated)
        {
            try
            {
                ReportLeaderboard(score, "main_score_leaderboard");
                ReportLeaderboard(score, "daily_score_leaderboard");
                ReportLeaderboard(score, "weekly_score_leaderboard");
                Debug.Log("Score reported: " + score);
            }
            catch (Exception e)
            {
                Debug.LogError("Failed to report score: " + e.Message);
            }
        }
    }

    public static async void ReportLeaderboard(int score, string leaderboardID)
    {
        var leaderboard = await GKLeaderboard.LoadLeaderboards(leaderboardID);
        if (leaderboard == null)
        {
            Debug.LogError("Leaderboard not found or failed to load.");
            return;
        }
        await leaderboard[0].SubmitScore(score, 0, GKLocalPlayer.Local);
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

    public static async Task<int> GetPlayerRankAsync(string leaderboardID)
    {
        if (!GKLocalPlayer.Local.IsAuthenticated)
        {
            Debug.LogError("Player is not authenticated.");
            return -1;
        }

        var leaderboards = (await GKLeaderboard.LoadLeaderboards(leaderboardID)).ToArray();
        if (leaderboards.Length == 0)
        {
            Debug.LogError($"Leaderboard with ID {leaderboardID} not found.");
            return -1;
        }

        GKLeaderboard leaderboard = leaderboards[0];

        var result = await leaderboard.LoadEntries(
            GKLeaderboard.PlayerScope.Global,
            GKLeaderboard.TimeScope.AllTime,
            0, 2147483647
        );

        var localPlayerEntry = result.LocalPlayerEntry;

        if (localPlayerEntry == null)
        {
            Debug.Log("No entry found for local player on the leaderboard.");
            return -1;
        }

        return (int)localPlayerEntry.Rank;
    }

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

    public static async Task UnlockAchievement(string achievementName)
    {
        if (!GKLocalPlayer.Local.IsAuthenticated) return;

        string achievementID = achievementName.ToLower().Replace(" ", "_"); // Match your App Store Connect ID format

        var achievement = await GetAchievement(achievementID) ?? GKAchievement.Init(achievementID);

        if (achievement.IsCompleted) return;

        achievement.PercentComplete = 100;
        achievement.ShowCompletionBanner = true;

        try
        {
            await GKAchievement.Report(achievement);
            LocalBackupManager.IncrementCompletedAchievements();
            Debug.Log("Achievement unlocked: " + achievementID);
        }
        catch (Exception ex)
        {
            Debug.LogError("Failed to report achievement: " + achievementID + " | " + ex.Message);
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
