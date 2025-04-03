using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievmentsMenuScript : MonoBehaviour
{
    private GameObject StartBtn;
    private GameObject AccountMenu;
    private GameObject ObjectivesMenu;
    private GameObject CustomizePanel;
    private GameObject SettingsPanel;
    private GameObject CreditsMenu;
    private GameObject AchievmentsMenu;
    private void Start()
    {
        StartBtn = CentralUIController.Instance.StartBtn;
        AccountMenu = CentralUIController.Instance.AccountMenu;
        ObjectivesMenu = CentralUIController.Instance.ObjectivesMenu;
        CustomizePanel = CentralUIController.Instance.CustomizePanel;
        SettingsPanel = CentralUIController.Instance.SettingsPanel;
        CreditsMenu = CentralUIController.Instance.CreditsMenu;
        AchievmentsMenu = CentralUIController.Instance.AchievmentsMenu;
    }

    public void Open_CloseAchievmentsMenu()
    {
        if (AchievmentsMenu.activeSelf == false)
        {
            StartBtn.SetActive(false);
            AchievmentsMenu.SetActive(true);
            CustomizePanel.SetActive(false);
            SettingsPanel.SetActive(false);
            CreditsMenu.SetActive(false);
            ObjectivesMenu.SetActive(false);
            AccountMenu.SetActive(false);
        }
        else
        {
            AchievmentsMenu.SetActive(false);
            StartBtn.SetActive(true);
        }
    }
}
