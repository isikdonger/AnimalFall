using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GooglePlayServicesUI : MonoBehaviour
{
    [SerializeField] Text highscoreText;
    [SerializeField] Text leaderboardRank;

    private void Awake()
    {
        DisplayHighScore();
        DisplayLeaderboardRank();
    }

    public void DisplayLeaderboard()
    {
        GooglePlayServicesManager.ShowLeaderboard();
    }

    public void DisplayHighScore()
    {
        highscoreText.text = LocalBackupManager.GetHighScore().ToString();
    }

    async void DisplayLeaderboardRank()
    {
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
    }

    public void DisplayAchievements()
    {
        GooglePlayServicesManager.ShowAchievements();
    }
}
