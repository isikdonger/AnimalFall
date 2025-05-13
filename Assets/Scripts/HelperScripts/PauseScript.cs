using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class PauseScript : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GameObject SoundOnBtn;
    [SerializeField] private GameObject SoundOffBtn;
    [SerializeField] private GameObject MusicOnBtn;
    [SerializeField] private GameObject MusicOffBtn;
    [SerializeField] private GameObject LanguageMenu;
    [SerializeField] private GameObject leadberboardBtn;
    [SerializeField] private GameObject achievementsBtn;

    void Awake()
    {
        PauseMenu.SetActive(false);
        LanguageMenu.SetActive(false);
    }

    public void PauseBtn()
    {
        if (PauseMenu.activeSelf == false)
        {
            GameManager.Instance.DeactiveGameObjects();
            PauseMenu.SetActive(true);
            Time.timeScale = 0;
            PlatformSpawner.isPaused = true;
        }
        else
        {
            GameManager.Instance.ReactiveGameObject();
            PauseMenu.SetActive(false);
            Time.timeScale = 1;
            PlatformSpawner.isPaused = false;
        }
    }

    public async void BacktoMenu()
    {
        PauseMenu.SetActive(false);
        ScoreTextScript.scoreValue = 0;
        Time.timeScale = 1;
#if !UNITY_EDITOR
        await FirestoreManager.SyncWithCloud();
        GameManager.Instance.ExitGame();
#endif
        SceneManager.LoadScene("AnimalFall UI", LoadSceneMode.Single);
    }

    public void BacktoGame()
    {
        GameManager.Instance.ReactiveGameObject();
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
        PlatformSpawner.isPaused = false;
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
            leadberboardBtn.SetActive(false);
            achievementsBtn.SetActive(false);
            LanguageMenu.SetActive(true);
        }
        else
        {
            LanguageMenu.SetActive(false);
            leadberboardBtn.SetActive(true);
            achievementsBtn.SetActive(true);
        }
    }
}
