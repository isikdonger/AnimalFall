using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomizePanelScript : MonoBehaviour
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
        //PlayerSpawn.selectedSprite = GameObject.Find("OwlBtn").transform.GetChild(0).GetComponent<Image>().sprite;
    }

    private void Start()
    {
        CustomizePanel.SetActive(false);
    }

    public void Open_CloseCustomizePanel()
    {
        if (CustomizePanel.activeSelf == false)
        {
            StartBtn.SetActive(false);
            CustomizePanel.SetActive(true);
            SettingsPanel.SetActive(false);
            AchievmentsMenu.SetActive(false);
            CreditsMenu.SetActive(false);
            ObjectivesMenu.SetActive(false);
            AccountMenu.SetActive(false);
        }
        else
        {
            CustomizePanel.SetActive(false);
            StartBtn.SetActive(true);
        }
    }

    public void ChangeCharacter(GameObject Button)
    {
        //PlayerSpawn.selectedSprite = Button.transform.GetChild(0).GetComponent<Image>().sprite;
        CustomizePanel.SetActive(false);
        StartBtn.SetActive(true);
    }
}