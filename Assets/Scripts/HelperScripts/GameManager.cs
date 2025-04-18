using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        InitiliazeGame();
    }

    public void Death()
    {
#if !UNITY_EDITOR
        LocalBackupManager.SetHighScore(ScoreTextScript.scoreValue);
        LocalBackupManager.IncrementTotalGames();
        LocalBackupManager.IncrementTotalScore(ScoreTextScript.scoreValue);
        LocalBackupManager.IncrementTotalCoins(CoinTextScript.coinAmount);
        LossCountAchievement();
        WinCountAchievement();
        PolandballAchievement();
        HundredAchievement();
        ComeOnAchievement();
#endif
        RestartGame();
    }

    public void InitiliazeGame()
    {
        Time.timeScale = 1;
        PlayerMovement.InitializeGame();
        PlatformSpawner.InitiliazeGame();
        PlatformScript.InitiliazeGame();
        CoinSpawner.InitializeGame();
        ScoreTextScript.InitiliazeGame();
        ScoreTextScript.scoreValue = 0;
        Die.animator.ResetTrigger("Freeze");
        Die.animator.ResetTrigger("Unfreeze");
    }

    public void RestartGame()
    {
        Invoke("RestartAfterTime", 0.5f);
        InitiliazeGame();
    }
    void RestartAfterTime()
    {
        SceneManager.LoadScene("AnimalFall", LoadSceneMode.Single);
    }

    public void LossCountAchievement()
    {
        LocalBackupManager.IncrementLossCount();
        if (ScoreTextScript.scoreValue < 10)
        {
            int currentCount = LocalBackupManager.GetLossCount();
            if (currentCount == 8)
            {
#if UNITY_ANDROID
                GooglePlayServicesManager.UnlockAchievementCoroutine("EIGTH!?");
#elif UNITY_IOS
                GameCenterManager.UnlockAchievementCoroutine("EIGTH!?");
#endif
            }
            else if (currentCount == 9)
            {
#if UNITY_ANDROID
                GooglePlayServicesManager.UnlockAchievementCoroutine("NINE!?");
#elif UNITY_IOS
                GameCenterManager.UnlockAchievementCoroutine("NINE!?");
#endif
            }
            else if (currentCount == 10)
            {
#if UNITY_ANDROID
                GooglePlayServicesManager.UnlockAchievementCoroutine("TEN!?");
#elif UNITY_IOS
                GameCenterManager.UnlockAchievementCoroutine("TEN!?");
#endif
            }
        }
        else
        {
            LocalBackupManager.ResetLossCount();
        }
    }

    public void WinCountAchievement()
    {
        if (ScoreTextScript.scoreValue >= 10)
        {
            int winCount = LocalBackupManager.GetWinCount();
            if (winCount == 10)
            {
#if UNITY_ANDROID
                GooglePlayServicesManager.UnlockAchievementCoroutine("Cook");
#elif UNITY_IOS
                GameCenterManager.UnlockAchievementCoroutine("Cook");
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

    public void PolandballAchievement()
    {
        if (ScoreTextScript.scoreValue == 1000)
        {
#if UNITY_ANDROID
            GooglePlayServicesManager.UnlockAchievementCoroutine("Poland Cannot Into Space");
#elif UNITY_IOS
            GameCenterManager.UnlockAchievementCoroutine("Poland Cannot Into Space");
#endif
        }
    }

    public void HundredAchievement()
    {
        if (LocalBackupManager.GetHighScore() == 100)
        {
#if UNITY_ANDROID
            GooglePlayServicesManager.UnlockAchievementCoroutine("HUNDRED");
#elif UNITY_IOS
            GameCenterManager.UnlockAchievementCoroutine("HUNDRED");
#endif
        }
    }

    public void ComeOnAchievement()
    {
        if (ScoreTextScript.scoreValue == 0)
        {
#if UNITY_ANDROID
            GooglePlayServicesManager.UnlockAchievementCoroutine("Come On");
#elif UNITY_IOS
            GameCenterManager.UnlockAchievementCoroutine("Come On");
#endif
        }
    }
}