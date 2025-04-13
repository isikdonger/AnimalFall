using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    [SerializeField] Camera Camera;
    [SerializeField] GameObject loadingText;
    int WIDTH = 1080, HEIGHT = 2400;

    private void Awake()
    {
        // Camera resolution
        int resWidth, resHeight, multiplier, scaleHeigth, scaleWidth;
        resWidth = Camera.pixelWidth;
        resHeight = Camera.pixelHeight;
        multiplier = resHeight > 2400 ? 1 : -1;
        scaleHeigth = (resHeight - HEIGHT) / 300 * 25 * multiplier;
        scaleWidth = (resWidth - WIDTH) / 300 * 25 * multiplier;

        int heightDimension = 150 + scaleHeigth, widthDimension = 550 + scaleWidth;
        RectTransform loadingTextRT = loadingText.GetComponent<RectTransform>();
        loadingTextRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, widthDimension);
        loadingTextRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, heightDimension);
        loadingTextRT.anchoredPosition = new Vector2(0, -(heightDimension / 2 + 100));

        // Localize text
        loadingText.GetComponent<LocalizeStringEvent>().RefreshString();
    }

    private async void Start()
    {
        AsyncOperation uiLoadOp = SceneManager.LoadSceneAsync("AnimalFall UI", LoadSceneMode.Additive);
        uiLoadOp.allowSceneActivation = false;

        // Wait until scene is ready for activation
        while (uiLoadOp.progress < 0.9f)
        {
            await Task.Yield();
        }

        try
        {
            if (!Application.isEditor)
            {
#if UNITY_ANDROID
                // Android initialization sequence
                bool playGamesSuccess = await GooglePlayServicesManager.Initialize();
                bool firebaseSuccess = await FirestoreManager.Initialize();
                if (firebaseSuccess && playGamesSuccess)
                {
                    await FirestoreManager.AuthenticateFirebase();
                    await FirestoreManager.SyncWithCloud();
                }
#elif UNITY_IOS
                // iOS initialization sequence
                bool gameCenterSuccess = await GameCenterManager.Initialize();
                bool firebaseSuccess = await FirestoreManager.Initialize();
                if (firebaseSuccess && gameCenterSuccess)
                {
                    await FirestoreManager.AuthenticateFirebase();
                    await FirestoreManager.SyncWithCloud();
                }
#endif
            }
            // Activate UI scene
            uiLoadOp.allowSceneActivation = true;
            while (!uiLoadOp.isDone)
            {
                await Task.Yield();
            }

            // Scene transition
            Scene uiScene = SceneManager.GetSceneByName("AnimalFall UI");
            if (!uiScene.isLoaded)
            {
                Debug.LogError("UI scene failed to load!");
                return;
            }

            SceneManager.SetActiveScene(uiScene);

            // Unload loading scene
            Scene loadingScene = SceneManager.GetSceneByName("Loading Scene");
            if (loadingScene.isLoaded)
            {
                await SceneManager.UnloadSceneAsync(loadingScene);
                await Resources.UnloadUnusedAssets(); // Cleanup
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Initialization failed: {ex.Message}");
            // For now, just quit
            Application.Quit();
            // Retry logic
        }
    }
}
