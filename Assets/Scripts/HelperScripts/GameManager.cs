using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private GameObject player;
    private Vector3 playerVelocity;
    private IEnumerable<GameObject> platforms;
    private IEnumerable<GameObject> coins;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        InitiliazeGame();
    }

    public void DeactiveGameObjects()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerVelocity = player.GetComponent<Rigidbody2D>().linearVelocity;
        player.SetActive(false);
        platforms = GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None).Where(obj => obj.tag.Contains("Platform"));
        coins = GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None).Where(obj => obj.tag.Contains("Coin"));
        foreach (GameObject obj in platforms.Concat(coins))
        {
            obj.SetActive(false);
        }
    }

    public void ReactiveGameObject()
    {
        player.SetActive(true);
        player.GetComponent<Rigidbody2D>().linearVelocity = playerVelocity;
        foreach (GameObject obj in platforms.Concat(coins))
        {
            obj.SetActive(true);
        }
    }

    public async void Death(string message)
    {
        DeathMenuScript.ShowDeathMenu(message);
#if !UNITY_EDITOR
        await ExitGame();
#endif
    }

    public static void InitiliazeGame()
    {
        Time.timeScale = 1;
        PlayerMovement.InitializeGame();
        PlatformSpawner.InitiliazeGame();
        PlatformScript.InitiliazeGame();
        CoinSpawner.InitializeGame();
        ScoreTextScript.InitiliazeGame();
        CoinTextScript.InitiliazeGame();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("AnimalFall", LoadSceneMode.Single);
    }

    public async Task ExitGame()
    {
        // Local saves
        LocalBackupManager.IncrementTotalGames();
        LocalBackupManager.SetHighScore(ScoreTextScript.scoreValue);
        LocalBackupManager.IncrementTotalScore(ScoreTextScript.scoreValue);
        LocalBackupManager.IncrementCoinsGained(CoinTextScript.coinAmount);
        LocalBackupManager.AddCoins(CoinTextScript.coinAmount);

        // Start all achievements in parallel
        var tasks = new List<Task>
        {
            ScoreObjective(),
            CoinObjective(),
            TimeObjective(),
            LossCountAchievement(),
            ComeOnAchievement(),
            HundredAchievement(),
            StandartPlatformAchievement(),
            WinCountAchievement(),
            PolandballAchievement()
        };

        await Task.WhenAll(tasks);
    }

    public async Task ScoreObjective()
    {
        if (LocalBackupManager.GetTotalScore() >= LocalBackupManager.GetScoreGoal())
        {
#if UNITY_ANDROID
            await GooglePlayServicesManager.IncrementObjective("Score Objective");
            LocalBackupManager.AddCoins(LocalBackupManager.GetScoreReward());
            LocalBackupManager.IncrementScoreObjectiveStep();
#endif
        }
    }

    public async Task CoinObjective()
    {
        if (LocalBackupManager.GetCoinsGained() >= LocalBackupManager.GetCoinGoal())
        {
#if UNITY_ANDROID
            await GooglePlayServicesManager.IncrementObjective("Coin Objective");
            LocalBackupManager.AddCoins(LocalBackupManager.GetCoinReward());
            LocalBackupManager.IncrementCoinObjectiveStep();
#endif
        }
    }

    public async Task TimeObjective()
    {
        if (LocalBackupManager.GetTotalTime() >= LocalBackupManager.GetTimeGoal())
        {
#if UNITY_ANDROID
            await GooglePlayServicesManager.IncrementObjective("Time Objective");
            LocalBackupManager.AddCoins(LocalBackupManager.GetTimeReward());
            LocalBackupManager.IncrementTimeObjectiveStep();
#endif
        }
    }

    public async Task LossCountAchievement()
    {
        if (ScoreTextScript.scoreValue < 10)
        {
            LocalBackupManager.IncrementLossCount();
            int currentCount = LocalBackupManager.GetLossCount();
            string achievementName;
            switch (currentCount)
            {
                case 8:
                    achievementName = "EIGHT";
                    break;
                case 9:
                    achievementName = "NINE";
                    break;
                case 10:
                    achievementName = "TEN";
                    break;
                default:
                    return; // No achievement to unlock
            }
#if UNITY_ANDROID
            await GooglePlayServicesManager.UnlockAchievement(achievementName);
#elif UNITY_IOS
            await GameCenterManager.UnlockAchievement(achievementName);
#endif
        }
        else
        {
            LocalBackupManager.ResetLossCount();
        }
    }

    public async Task ComeOnAchievement()
    {
        if (ScoreTextScript.scoreValue == 0)
        {
#if UNITY_ANDROID
            await GooglePlayServicesManager.UnlockAchievement("Come On");
#elif UNITY_IOS
            await GameCenterManager.UnlockAchievement("Come On");
#endif
        }
    }

    public async Task HundredAchievement()
    {
        if (LocalBackupManager.GetHighScore() == 100)
        {
#if UNITY_ANDROID
            await GooglePlayServicesManager.UnlockAchievement("HUNDRED");
#elif UNITY_IOS
            await GameCenterManager.UnlockAchievement("HUNDRED");
#endif
        }
    }

    public async Task StandartPlatformAchievement()
    {
#if UNITY_ANDROID
        await GooglePlayServicesManager.UnlockAchievement("Field of Hopes and Dreams");
#elif UNITY_IOS
        await GameCenterManager.UnlockAchievement("Field of Hopes and Dreams");
#endif
    }

    public async Task WinCountAchievement()
    {
        if (ScoreTextScript.scoreValue >= 10)
        {
            int winCount = LocalBackupManager.GetWinCount();
            if (winCount == 10)
            {
#if UNITY_ANDROID
                await GooglePlayServicesManager.UnlockAchievement("Cook");
#elif UNITY_IOS
                await GameCenterManager.UnlockAchievement("Cook");
#endif
            }
            else
            {
                LocalBackupManager.IncrementWinCount();
            }
        }
        else
        {

            LocalBackupManager.ResetWinCount();
        }
    }

    public async Task PolandballAchievement()
    {
        if (ScoreTextScript.scoreValue == 1000)
        {
#if UNITY_ANDROID
            await GooglePlayServicesManager.UnlockAchievement("Poland Cannot Into Space");
#elif UNITY_IOS
            await GameCenterManager.UnlockAchievement("Poland Cannot Into Space");
#endif
        }
    }
}