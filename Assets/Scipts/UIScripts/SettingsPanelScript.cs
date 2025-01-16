using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPanelScript : MonoBehaviour
{
    private GameObject SettingsPanel;
    private GameObject StartBtn;
    private GameObject CustomizePanel;
    private GameObject AchievmentsMenu;
    private GameObject CreditsMenu;
    private GameObject AccountMenu;
    private GameObject ObjectivesMenu;
    private GameObject SoundPanel;
    private GameObject SoundOnBtn;
    private GameObject SoundOffBtn;
    private GameObject MusicPanel;
    private GameObject MusicOnBtn;
    private GameObject MusicOffBtn;
    private GameObject LanguageMenu;
    private GameObject CreditsBtn;
    private GameObject FeedbackBtn;

    void Awake()
    {
        AccountMenu = GameObject.Find("AccountMenu");
        StartBtn = GameObject.Find("StartBtn");
        CustomizePanel = GameObject.Find("CustomizePanel");
        SettingsPanel = GameObject.Find("SettingsPanel");
        AchievmentsMenu = GameObject.Find("AchievmentsMenu");
        CreditsMenu = GameObject.Find("CreditsMenu");
        ObjectivesMenu = GameObject.Find("ObjectivesMenu");
        SoundPanel = GameObject.Find("SoundSetting");
        SoundOnBtn = SoundPanel.transform.Find("OnBtn").gameObject;
        SoundOffBtn = SoundPanel.transform.Find("OffBtn").gameObject;
        MusicPanel = GameObject.Find("MusicSetting");
        MusicOnBtn = MusicPanel.transform.Find("OnBtn").gameObject;
        MusicOffBtn = MusicPanel.transform.Find("OffBtn").gameObject;
        LanguageMenu = GameObject.Find("LanguageMenu");
        CreditsBtn = GameObject.Find("CreditsBtn");
        FeedbackBtn = GameObject.Find("FeedbackBtn");
    }

    private void Start()
    {
        SettingsPanel.SetActive(false);
        SoundOffBtn.SetActive(false);
        MusicOffBtn.SetActive(false);
        LanguageMenu.SetActive(false);
        CreditsMenu.SetActive(false);
    }

    public void Open_CloseSettingsMenu()
    {
        if (SettingsPanel.activeSelf == false)
        {
            StartBtn.SetActive(false);
            SettingsPanel.SetActive(true);
            CustomizePanel.SetActive(false);
            AchievmentsMenu.SetActive(false);
            CreditsMenu.SetActive(false);
            AccountMenu.SetActive(false);
            ObjectivesMenu.SetActive(false);
        }
        else
        {
            LanguageMenu.SetActive(false);
            FeedbackBtn.SetActive(true);
            CreditsBtn.SetActive(true);
            SettingsPanel.SetActive(false);
            StartBtn.SetActive(true);
        }
    }
    public void SoundOn_Off()
    {
        if (SoundOnBtn.activeSelf == true)
        {
            SoundOnBtn.SetActive(false);
            SoundOffBtn.SetActive(true);
        }
        else
        {
            SoundOffBtn.SetActive(false);
            SoundOnBtn.SetActive(true);
        }
    }
    public void MusicOn_Off()
    {
        if (MusicOnBtn.activeSelf == true)
        {
            MusicOnBtn.SetActive(false);
            MusicOffBtn.SetActive(true);
        }
        else
        {
            MusicOffBtn.SetActive(false);
            MusicOnBtn.SetActive(true);
        }
    }
    public void Open_CloseLanguageMenu()
    {
        if (LanguageMenu.activeSelf == false)
        {
            CreditsBtn.SetActive(false);
            FeedbackBtn.SetActive(false);
            LanguageMenu.SetActive(true);
        }
        else
        {
            LanguageMenu.SetActive(false);
            FeedbackBtn.SetActive(true);
            CreditsBtn.SetActive(true);
        }
    }
}