﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }
    public void RestartGame()
    {
        Invoke("RestartAfterTime", 1f);
        BGScroll.scroll_Speed = 1f;
        CoinSpawner.coin_Spawn_Time = 3f;
        CoinSpawner.current_Coin_Spawn_Timer = 0f;
        PlatfromScript.move_Speed = 1.25f;
        PlatfromSpawner.platform_Spawn_Timer = 75f;
        PlatfromSpawner.current_Platform_Spawn_Timer = 0f;
        PlayerMovement.MoveSpeed = 0.1f;
        PlayerMovement.newMoveSpeed = 0.1f;
        PlayerMovement.gravityScale = 0.1f;
        PlayerMovement.speedmultiplyer = 0f;
        ScoreTextScript.scoreValue = 0;
    }
    void RestartAfterTime()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("AnimalFall");
    }
}