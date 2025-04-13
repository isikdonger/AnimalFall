using UnityEngine;

public class SettingsPanelScript : MonoBehaviour
{
    private GameObject StartBtn;
    private GameObject AccountMenu;
    private GameObject ObjectivesMenu;
    private GameObject CustomizePanel;
    private GameObject SettingsPanel;
    private GameObject CreditsMenu;
    private GameObject AchievmentsMenu;
    [SerializeField] private GameObject SoundOnBtn;
    [SerializeField] private GameObject SoundOffBtn;
    [SerializeField] private GameObject MusicOnBtn;
    [SerializeField] private GameObject MusicOffBtn;
    [SerializeField] private GameObject LanguageMenu;
    [SerializeField] private GameObject CreditsBtn;
    [SerializeField] private GameObject FeedbackBtn;

    private void Start()
    {
        StartBtn = CentralUIController.Instance.StartBtn;
        AccountMenu = CentralUIController.Instance.AccountMenu;
        ObjectivesMenu = CentralUIController.Instance.ObjectivesMenu;
        CustomizePanel = CentralUIController.Instance.CustomizePanel;
        SettingsPanel = CentralUIController.Instance.SettingsPanel;
        CreditsMenu = CentralUIController.Instance.CreditsMenu;
        AchievmentsMenu = CentralUIController.Instance.AchievmentsMenu;
        SoundOffBtn.SetActive(false);
        MusicOffBtn.SetActive(false);
        LanguageMenu.SetActive(false);
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