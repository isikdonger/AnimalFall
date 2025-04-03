using GooglePlayGames;
using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    //[SerializeField] private GameObject loadingText;

    private void Awake()
    {
        //loadingText.GetComponent<LocalizeStringEvent>().RefreshString();
    }

    private async void Start()
    {
        /*AsyncOperation uiLoadOp = SceneManager.LoadSceneAsync("AnimalFall UI", LoadSceneMode.Additive);
        uiLoadOp.allowSceneActivation = false; // Prevent immediate activation

        // Wait until the UI scene is fully loaded (but not activated)
        while (!uiLoadOp.isDone)
        {
            if (uiLoadOp.progress >= 0.9f) break; // Almost loaded
            await Task.Yield();
        }*/

        try
        {
            // Initialize Firebase
            await FirestoreManager.Initialize();

            // Initialize and authenticate with Google Play Services
            bool playGamesSuccess = await GooglePlayServicesManager.Initialize();

            if (playGamesSuccess)
            {
                await FirestoreManager.AuthenticateFirebase();
                await FirestoreManager.SyncWithCloud();
            }

            SceneManager.LoadScene("AnimalFall UI");

            //Hide the loading screen here if you have one
            //await SceneManager.UnloadSceneAsync("Loading Scene");

            // Only load scene after everything is done
            //uiLoadOp.allowSceneActivation = true;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Initialization failed: {ex.Message}");
            Application.Quit();
            // Show error to player and provide retry option
        }
    }
}
