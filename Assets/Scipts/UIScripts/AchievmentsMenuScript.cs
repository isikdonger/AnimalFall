using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievmentsMenuScript : MonoBehaviour
{
    private GameObject AccountMenu;
    private GameObject StartBtn;
    private GameObject CustomizePanel;
    private GameObject SettingsPanel;
    private GameObject AchievmentsMenu;
    private GameObject CreditsMenu;
    private GameObject ObjectivesMenu;

    void Awake()
    {
        AccountMenu = GameObject.Find("AccountMenu");
        StartBtn = GameObject.Find("StartBtn");
        CustomizePanel = GameObject.Find("CustomizePanel");
        SettingsPanel = GameObject.Find("SettingsPanel");
        AchievmentsMenu = GameObject.Find("AchievmentsMenu");
        CreditsMenu = GameObject.Find("CreditsMenu");
        ObjectivesMenu = GameObject.Find("ObjectivesMenu");
    }

    private void Start()
    {
        AchievmentsMenu.SetActive(false);
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
