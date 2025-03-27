using GooglePlayGames;
using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    private async void Start()
    {
        try
        {
            // Initialize Firebase
            await FirestoreManager.Initialize();

            // Initialize and authenticate with Google Play Services
            bool playGamesSuccess = await GooglePlayServicesManager.Initialize();

            if (playGamesSuccess)
            {
                await FirestoreManager.AuthenticateFirebase();
                //await FirestoreManager.SyncWithCloud();
            }

            // Only load scene after everything is done
            SceneManager.LoadScene("AnimalFall UI");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Initialization failed: {ex.Message}");
            Application.Quit();
            // Show error to player and provide retry option
        }
    }
}
