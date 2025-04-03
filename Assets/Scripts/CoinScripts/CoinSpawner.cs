using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CoinSpawner : MonoBehaviour   
{
    public GameObject bronzeCoin, silverCoin, goldCoin, killCoin;
    public static float coin_Spawn_Time = 3f;
    public static float current_Coin_Spawn_Timer = 0f;
    private float min_X = -1.68f, max_X = 1.68f, min_Y = -3.65f, max_Y = 2.65f;
    private GameObject oldbCoin, oldsCoin, oldgCoin, oldkCoin;
    void FixedUpdate()
    {
        if (ScoreTextScript.scoreValue > 8)
        {
            coin_Spawn_Time -= 0.0005f;
            Debug.Log(current_Coin_Spawn_Timer);
            CoinSpawnTimer();
        }
    }
    void Update()
    {
        oldbCoin = GameObject.Find("coin bronze(Clone)");
        oldsCoin = GameObject.Find("silver coin(Clone)");
        oldgCoin = GameObject.Find("gold coin(Clone)");
        oldkCoin = GameObject.Find("kill coin(Clone)");
    }
    void CoinSpawnTimer()
    {
        if (current_Coin_Spawn_Timer >= coin_Spawn_Time)
        {
            current_Coin_Spawn_Timer = 0f;
            SpawnCoin();
        }
        else
        {
            current_Coin_Spawn_Timer += 0.005f;
        }
    }
    void SpawnCoin()
    {
        Vector3 temp = transform.position;
        temp.x = Random.Range(min_X, max_X);
        temp.y = Random.Range(min_Y, max_Y);
        GameObject newCoin = null;
        Debug.Log("Spawn");
        if (ScoreTextScript.scoreValue < 15)
        {
            newCoin = Instantiate(bronzeCoin, temp, Quaternion.identity);
            Debug.Log(newCoin);
            Destroy(oldbCoin, 10f);
        }
        else if (ScoreTextScript.scoreValue >= 15 && ScoreTextScript.scoreValue < 22)
        {
            if (Random.Range(0, 2) > 0)
            {
                newCoin = Instantiate(silverCoin, temp, Quaternion.identity);
                Destroy(oldsCoin, 10f);
            }
            else
            {
                newCoin = Instantiate(bronzeCoin, temp, Quaternion.identity);Destroy(oldbCoin, 10f);
            }
        }
        else if (ScoreTextScript.scoreValue >= 22 && ScoreTextScript.scoreValue < 30)
        {
            if (Random.Range(0, 3) > 1)
            {
                newCoin = Instantiate(goldCoin, temp, Quaternion.identity);
                Destroy(oldgCoin, 10f);
            }
            if (Random.Range(0, 3) > 0)
            {
                newCoin = Instantiate(silverCoin, temp, Quaternion.identity);
                Destroy(oldsCoin, 10f);
            }
            else
            {
                newCoin = Instantiate(bronzeCoin, temp, Quaternion.identity);
                Destroy(oldbCoin, 10f);
            }
        }
        else if (ScoreTextScript.scoreValue >= 30)
        {
            if (Random.Range(0, 4) > 2)
            {
                newCoin = Instantiate(killCoin, temp, Quaternion.identity);
                Destroy(oldkCoin, 10f);
            }
            if (Random.Range(0, 4) > 1)
            {
                newCoin = Instantiate(goldCoin, temp, Quaternion.identity);
                Destroy(oldgCoin, 10f);
            }
            if (Random.Range(0, 4) > 0)
            {
                newCoin = Instantiate(silverCoin, temp, Quaternion.identity);
                Destroy(oldsCoin, 10f);
            }
            else
            {
                newCoin = Instantiate(bronzeCoin, temp, Quaternion.identity);
                Destroy(oldbCoin, 10f);
            }
        }
        if (newCoin)
            newCoin.transform.parent = transform;
    }
}