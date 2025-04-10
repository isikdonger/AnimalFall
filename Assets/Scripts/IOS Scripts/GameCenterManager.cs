using UnityEngine;
using Apple.GameKit;
using System;
using System.Threading.Tasks;
using Apple.GameKit.Leaderboards;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SocialPlatforms.GameCenter;

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
                var leaderboard = await GKLeaderboard.LoadLeaderboards(new[] { leaderboardID });
                await leaderboard[0].SubmitScore(score, 0, GKLocalPlayer.Local);
                Debug.Log("Score reported: " + score);
            }
            catch (Exception e)
            {
                Debug.LogError("Failed to report score: " + e.Message);
            }
        }
    }

    public static async void ShowLeaderboardUI()
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

    public static async void UnlockAchievement(string achievementName)
    {
        string achievementID = ""; // Use the achievement name as the ID
        GKAchievement achievement = await GetAchievement(achievementID);
        achievement ??= GKAchievement.Init(achievementID);

        if (!achievement.IsCompleted)
        {
            achievement.PercentComplete = 100;
            achievement.ShowCompletionBanner = true;
            await GKAchievement.Report(achievement);
        }
    }

    public static async void ShowAchievementsUI()
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
