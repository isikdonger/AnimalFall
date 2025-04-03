using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public GameObject MainMenu; //OptionsMenu, LanguageMenu, Sound, Back;
    //public GameObject SoundManager, standartplatform, player;
    public GameObject PlayerSpawn, PlatformSpawn;
    //void Awake()
    //{
    //    OptionsMenu.SetActive(false);
    //    LanguageMenu.SetActive(false);
    //}
    public void StartGame()
    {
        MainMenu.SetActive(false);
        PlayerSpawn.GetComponent<PlayerSpawn>().SpawnPlayer();
        PlatformSpawn.GetComponent<PlatformSpawn>().SpawnPlatform();
    }
    //public void OpenOptions()
    //{
    //    MainMenu.SetActive(false);
    //    OptionsMenu.SetActive(true);
    //    OptionsMenu.SetActive(true);
    //    Time.timeScale = 1;
    //    //standartplatform.SetActive(false);
    //    //player.SetActive(false);
    //}
    //public void ExitOptions()
    //{
    //    OptionsMenu.SetActive(false);
    //    MainMenu.SetActive(true);
    //}
    //public void SoundOn()
    //{
    //    //SoundManager.SetActive(true);
    //}
    //public void SoundOff()
    //{
    //    //SoundManager.SetActive(false);
    //}
    //public void OpenLanguageMenu()
    //{
    //    LanguageMenu.SetActive(true);
    //    Sound.SetActive(false);
    //    Back.SetActive(false);
    //}
}
