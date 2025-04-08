using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class PauseScript : MonoBehaviour
{
    public GameObject PauseMenu, LanguageMenu, Sound, Back;
    public GameObject SoundManager;
    private IEnumerable<GameObject> platforms;
    void Awake()
    {
        PauseMenu.SetActive(false);
        //LanguageMenu.SetActive(false);
    }
    public void PauseBtn()
    {
        if (PauseMenu.activeSelf == false)
        {
            platforms = GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None).Where(obj => obj.name.Contains("(Clone)"));
            foreach (GameObject obj in platforms)
            {
                obj.SetActive(false);
            }
            PauseMenu.SetActive(true);
            Time.timeScale = 0;
            PlatfromSpawner.isPaused = true;
        }
        else
        {
            foreach (GameObject obj in platforms)
            {
                obj.SetActive(true);
            }
            PauseMenu.SetActive(false);
            Time.timeScale = 1;
            PlatfromSpawner.isPaused = false;
        }
    }
    public void BacktoGame()
    {
        foreach (GameObject obj in platforms)
        {
            obj.SetActive(true);
        }
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
        PlatfromSpawner.isPaused = false;
    }
    public async void BacktoMenu()
    {
        PauseMenu.SetActive(false);
        ScoreTextScript.scoreValue = 0;
#if !UNITY_EDITOR
        await FirestoreManager.SyncWithCloud();
#endif
        SceneManager.LoadScene("AnimalFall UI", LoadSceneMode.Single);
    }
    public void SoundOn()
    {
        SoundManager.SetActive(true);
    }
    public void SoundOff()
    {
        SoundManager.SetActive(false);
    }
    public void OpenLanguageMenu()
    {
        LanguageMenu.SetActive(true);
        Sound.SetActive(false);
        Back.SetActive(false);
    }
}
