using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GooglePlayServicesUI : MonoBehaviour
{
    [SerializeField] Text highscoreText;
    [SerializeField] Text leaderboardRank;

    private void Start()
    {
        DisplayHighScore();
        DisplayLeaderboardRank();
    }

    public void DisplayLeaderboard()
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        GooglePlayServicesManager.ShowLeaderboard();
#endif
    }

    public void DisplayHighScore()
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        highscoreText.text = LocalBackupManager.GetHighScore().ToString();
#endif
    }

    async void DisplayLeaderboardRank()
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        try
        {
            int rank = await GooglePlayServicesManager.GetLeaderboardRankAsync();
            leaderboardRank.text = rank.ToString();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error retrieving leaderboard rank: {ex.Message}");
            leaderboardRank.text = "N/A"; // Display "N/A" or some error message
        }
#endif
    }

    public void DisplayAchievements()
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        GooglePlayServicesManager.ShowAchievements();
#endif
    }
}
