using UnityEngine;

public class SettingsPanelScript : MonoBehaviour
{
    [SerializeField] private GameObject SoundOnBtn;
    [SerializeField] private GameObject SoundOffBtn;
    [SerializeField] private GameObject MusicOnBtn;
    [SerializeField] private GameObject MusicOffBtn;
    [SerializeField] private GameObject LanguageMenu;
    [SerializeField] private GameObject CreditsBtn;
    [SerializeField] private GameObject FeedbackBtn;

    private void Start()
    {
        SoundOffBtn.SetActive(false);
        MusicOffBtn.SetActive(false);
        LanguageMenu.SetActive(false);
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