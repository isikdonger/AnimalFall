using GooglePlayGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccountMenuScript : MonoBehaviour
{
    private GameObject StartBtn;
    private Text AcccountName;
    private GameObject AccountMenu;
    private Text StartText;
    private GameObject ObjectivesMenu;
    private GameObject CustomizePanel;
    private GameObject SettingsPanel;
    private GameObject CreditsMenu;
    private GameObject AchievmentsMenu;

    private void Start()
    {
        StartBtn = CentralUIController.Instance.StartBtn;
        AcccountName = CentralUIController.Instance.AccountName;
        AccountMenu = CentralUIController.Instance.AccountMenu;
        StartText = CentralUIController.Instance.StartText;
        ObjectivesMenu = CentralUIController.Instance.ObjectivesMenu;
        CustomizePanel = CentralUIController.Instance.CustomizePanel;
        SettingsPanel = CentralUIController.Instance.SettingsPanel;
        CreditsMenu = CentralUIController.Instance.CreditsMenu;
        AchievmentsMenu = CentralUIController.Instance.AchievmentsMenu;
        ChangeAccountName();
    }

    private void ChangeAccountName()
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
