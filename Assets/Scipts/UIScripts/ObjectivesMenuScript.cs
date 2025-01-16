using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectivesMenuScript : MonoBehaviour
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
        ObjectivesMenu.SetActive(false);
    }

    public void Open_CloseObjectivesMenu()
    {
        if (ObjectivesMenu.activeSelf == false)
        {
            StartBtn.SetActive(false);
            ObjectivesMenu.SetActive(true);
            CustomizePanel.SetActive(false);
            SettingsPanel.SetActive(false);
            AchievmentsMenu.SetActive(false);
            CreditsMenu.SetActive(false);
            AccountMenu.SetActive(false);
        }
        else
        {
            ObjectivesMenu.SetActive(false);
            StartBtn.SetActive(true);
        }
    }
}