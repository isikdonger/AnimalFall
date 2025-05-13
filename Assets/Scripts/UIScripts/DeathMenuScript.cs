using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathMenuScript : MonoBehaviour
{

    private static GameObject DeathMenu;
    private static LocalizeStringEvent Message;

    private void Awake()
    {
        DeathMenu = GameObject.Find("DeathMenu");
        Message = GameObject.Find("Message").GetComponent<LocalizeStringEvent>();
        DeathMenu.SetActive(false);
    }

    public static void ShowDeathMenu(string message)
    {
        GameManager.Instance.DeactiveGameObjects();
        Message.StringReference = new LocalizedString { TableReference = "UI Strings", TableEntryReference = message };
        DeathMenu.SetActive(true);
        Time.timeScale = 0;
        PlatformSpawner.isPaused = true;
    }

    public void RestartGame()
    {
        GameManager.Instance.RestartGame();
    }

    public async void Return()
    {
#if !UNITY_EDITOR
        await FirestoreManager.SyncWithCloud();
#endif
        SceneManager.LoadScene("AnimalFall UI", LoadSceneMode.Single);
    }
}
