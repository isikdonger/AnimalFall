using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
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
        LocalBackupManager.SetHighScore(ScoreTextScript.scoreValue);
        LossCountAchievement();
        WinCountAchievement();
        PolandballAchievement();
        HundredAchievement();
        ComeOnAchievement();
        RestartGame();
    }

    public void InitiliazeGame()
    {
        Time.timeScale = 1;
        BGScroll.scroll_Speed = 1f;
        CoinSpawner.coin_Spawn_Time = 3f;
        CoinSpawner.current_Coin_Spawn_Timer = 0f;
        PlatfromScript.move_Speed = 1.25f;
        PlatfromSpawner.platform_Spawn_Timer = 7.5f;
        PlatfromSpawner.current_Platform_Spawn_Timer = 0f;
        PlatfromSpawner.isPaused = false;
        PlayerMovement.MoveSpeed = 0.1f;
        PlayerMovement.newMoveSpeed = 0.1f;
        PlayerMovement.gravityScale = 0.1f;
        PlayerMovement.speedmultiplyer = 0f;
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
        GooglePlayServicesManager.IsAchievementUnlocked("TEN!?", isUnlocked =>
        {
            if (!isUnlocked)
            {
                LocalBackupManager.IncrementLossCount();
                if (ScoreTextScript.scoreValue < 10)
                {
                    int currentCount = LocalBackupManager.GetLossCount();
                    if (currentCount == 8)
                    {
                        GooglePlayServicesManager.UnlockAchievementCoroutine("EIGTH!?");
                    }
                    else if (currentCount == 9)
                    {
                        GooglePlayServicesManager.UnlockAchievementCoroutine("NINE!?");
                    }
                    else if (currentCount == 10)
                    {
                        GooglePlayServicesManager.UnlockAchievementCoroutine("TEN!?");
                    }
                }
                else
                {
                    LocalBackupManager.ResetLossCount();
                }
            }
        });
    }

    public void WinCountAchievement()
    {
        GooglePlayServicesManager.IsAchievementUnlocked("Cook", isUnlocked =>
        {
            if (!isUnlocked)
            {
                if (ScoreTextScript.scoreValue >= 10)
                {
                    int winCount = LocalBackupManager.GetWinCount();
                    if (winCount == 10)
                    {
                        GooglePlayServicesManager.UnlockAchievementCoroutine("Cook");
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
        });
    }

    public void PolandballAchievement()
    {
        GooglePlayServicesManager.IsAchievementUnlocked("Poland Cannot Into Space", isUnlocked =>
        {
            if (!isUnlocked)
            {
                if (ScoreTextScript.scoreValue == 1000)
                {
                    GooglePlayServicesManager.UnlockAchievementCoroutine("Poland Cannot Into Space");
                }
            }
        });
    }

    public void HundredAchievement()
    {
        GooglePlayServicesManager.IsAchievementUnlocked("HUNDRED", isUnlocked =>
        {
            if (!isUnlocked)
            {
                if (LocalBackupManager.GetHighScore() == 100)
                {
                    GooglePlayServicesManager.UnlockAchievementCoroutine("HUNDRED");
                }
            }
        });
    }

    public void ComeOnAchievement()
    {
        GooglePlayServicesManager.IsAchievementUnlocked("Come On", isUnlocked =>
        {
            if (!isUnlocked)
            {
                if (ScoreTextScript.scoreValue == 0)
                {
                    GooglePlayServicesManager.UnlockAchievementCoroutine("Come On");
                }
            }
        });
    }
}