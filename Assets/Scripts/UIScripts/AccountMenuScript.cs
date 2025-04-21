using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System;

#if UNITY_ANDROID
using GooglePlayGames;
#endif

public class AccountMenuScript : MonoBehaviour
{
    [SerializeField] private Text AcccountName;
    [SerializeField] private Text LeaderboardRank;
    [SerializeField] private Text TotalGames;
    [SerializeField] private Text TotalScore;
    [SerializeField] private Text TotalCoins;
    [SerializeField] private Text CoinsSpent;
    [SerializeField] private Text AchievementsCompleted;
    [SerializeField] private Text HighScore;

    private void Start()
    {
        bool signedIn;
#if UNITY_ANDROID
        signedIn = PlayGamesPlatform.Instance.IsAuthenticated();
#elif UNITY_IOS
        signedIn = Apple.GameKit.GKLocalPlayer.Local.IsAuthenticated;
#endif
        if (signedIn)
        {
            SetAccountName();
            SetLeaderboardRank();
            SetTotalGames();
            SetTotalScore();
            SetTotalCoins();
            SetCoinsSpent();
            SetAchievementsCompleted();
            SetHighScore();
        }
    }

    private void SetAccountName()
    {
        string accountName;
#if UNITY_ANDROID
            accountName = PlayGamesPlatform.Instance.GetUserDisplayName();
#elif UNITY_IOS
        accountName = Apple.GameKit.GKLocalPlayer.Local.DisplayName;
#else
            accountName = "Guest";
#endif
        AcccountName.text = accountName.ToUpper();
    }

    async void SetLeaderboardRank()
    {
        try
        {
            int rank = await GooglePlayServicesManager.GetLeaderboardRankAsync();
            LeaderboardRank.text = rank.ToString();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error retrieving leaderboard rank: {ex.Message}");
            LeaderboardRank.text = "N/A"; // Display "N/A" or some error message
        }
    }

    public void SetTotalGames()
    {
        TotalGames.text = LocalBackupManager.GetTotalGames().ToString();
    }

    public void SetTotalScore()
    {
        TotalScore.text = LocalBackupManager.GetTotalScore().ToString();
    }

    public void SetTotalCoins()
    {
        TotalCoins.text = LocalBackupManager.GetTotalCoins().ToString();
    }

    public void SetCoinsSpent()
    {
        CoinsSpent.text = LocalBackupManager.GetSpentCoins().ToString();
    }

    public void SetAchievementsCompleted()
    {
        AchievementsCompleted.text = LocalBackupManager.GetCompletedAchievements().ToString();
    }


    public void SetHighScore()
    {
        HighScore.text = LocalBackupManager.GetHighScore().ToString();
    }
}
