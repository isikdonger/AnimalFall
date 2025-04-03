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
    }

    public void Open_CloseAccountMenu()
    {
        if (Social.localUser.authenticated)
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
