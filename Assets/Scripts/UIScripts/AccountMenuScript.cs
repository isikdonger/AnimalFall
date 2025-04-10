using GooglePlayGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccountMenuScript : MonoBehaviour
{
    private GameObject StartBtn;
    private GameObject AccountMenu;
    private Text StartText;
    private GameObject ObjectivesMenu;
    private GameObject CustomizePanel;
    private GameObject SettingsPanel;
    private GameObject CreditsMenu;
    private GameObject AchievmentsMenu;
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
        StartBtn = CentralUIController.Instance.StartBtn;
        AccountMenu = CentralUIController.Instance.AccountMenu;
        StartText = CentralUIController.Instance.StartText;
        ObjectivesMenu = CentralUIController.Instance.ObjectivesMenu;
        CustomizePanel = CentralUIController.Instance.CustomizePanel;
        SettingsPanel = CentralUIController.Instance.SettingsPanel;
        CreditsMenu = CentralUIController.Instance.CreditsMenu;
        AchievmentsMenu = CentralUIController.Instance.AchievmentsMenu;

        bool signedIn;
#if UNITY_ANDROID
        signedIn = PlayGamesPlatform.Instance.IsAuthenticated();
#elif UNITY_IOS
        signedIn = Apple.GameKit.GKLocalPlayer.Local.IsAuthenticated;
#endif
        if (signedIn)
        {
            SetAccountName();
            //SetLeaderboardRank();
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
        AcccountName.text = accountName;
    }

    /*async void SetLeaderboardRank()
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
    }*/

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

    public void Open_CloseAccountMenu()
    {
        bool signedIn;
#if UNITY_ANDROID
        signedIn = PlayGamesPlatform.Instance.IsAuthenticated();
#elif UNITY_IOS
        signedIn = Apple.GameKit.GKLocalPlayer.Local.IsAuthenticated;
#else
        signedIn = false;
#endif
        if (signedIn)
        {
            if (AccountMenu.activeSelf == false)
            {
                StartBtn.SetActive(false);
                AccountMenu.SetActive(true);
                CustomizePanel.SetActive(false);
                SettingsPanel.SetActive(false);
                AchievmentsMenu.SetActive(false);
                CreditsMenu.SetActive(false);
                ObjectivesMenu.SetActive(false);
            }
            else
            {
                AccountMenu.SetActive(false);
                StartBtn.SetActive(true);
            }
        }
        else
        {
            StartText.text = "Sign in to view achievements";
        }
    }
}
