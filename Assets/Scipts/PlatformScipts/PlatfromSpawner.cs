using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatfromSpawner : MonoBehaviour
{
    public GameObject platformPrefab;
    public GameObject breakablePlatform;
    public GameObject freezePlatform;
    public GameObject beamPlatform;
    public GameObject[] movingPlatforms;
    public static bool isPaused = false;
    public static float platform_Spawn_Timer = 75f;
    public static float current_Platform_Spawn_Timer = 0f;
    private float min_X = -1.7f, max_X = 1.7f;

    void Update()
    {
        PlatformSpawnTimer();
        SpawnPlatforms();
    }

    void PlatformSpawnTimer()
    {
        if (current_Platform_Spawn_Timer > platform_Spawn_Timer)
        {
            current_Platform_Spawn_Timer = 0f;
            if (platform_Spawn_Timer > 50f)
            {
                platform_Spawn_Timer -= 0.035f;
            }
        }
        else if (!isPaused)
        {
            current_Platform_Spawn_Timer += 0.035f;
        }
    }
    void SpawnPlatforms()
    {
        if (current_Platform_Spawn_Timer >= platform_Spawn_Timer)
        {
            Vector3 temp = transform.position;
            temp.x = Random.Range(min_X, max_X);
            GameObject newPlatform = null;
            if (ScoreTextScript.scoreValue <= 5)
            {
                newPlatform = Instantiate(platformPrefab, temp, Quaternion.identity);
                ScoreTextScript.scoreValue += 1;
            }
            else if (ScoreTextScript.scoreValue > 5 && ScoreTextScript.scoreValue <= 10)
            {
                if (Random.Range(0, 2) > 0)
                {
                    newPlatform = Instantiate(platformPrefab, temp, Quaternion.identity);
                    ScoreTextScript.scoreValue += 1;
                }
                else
                {
                    newPlatform = Instantiate(movingPlatforms[Random.Range(0, movingPlatforms.Length)], temp, Quaternion.identity);
                    ScoreTextScript.scoreValue += 1;
                }
            }
            else if (ScoreTextScript.scoreValue > 10 && ScoreTextScript.scoreValue <= 16)
            {
                if (Random.Range(0, 3) > 1)
                {
                    newPlatform = Instantiate(platformPrefab, temp, Quaternion.identity);
                    ScoreTextScript.scoreValue += 1;
                }
                else if (Random.Range(0, 3) > 0)
                {
                    newPlatform = Instantiate(movingPlatforms[Random.Range(0, movingPlatforms.Length)], temp, Quaternion.identity);
                    ScoreTextScript.scoreValue += 1;
                }
                else
                {
                    newPlatform = Instantiate(breakablePlatform, temp, Quaternion.identity);
                    ScoreTextScript.scoreValue += 1;
                }
            }
            else if (ScoreTextScript.scoreValue > 16 && ScoreTextScript.scoreValue <= 23)
            {
                if (Random.Range(0, 4) > 2)
                {
                    newPlatform = Instantiate(platformPrefab, temp, Quaternion.identity);
                    ScoreTextScript.scoreValue += 1;
                }
                else if (Random.Range(0, 4) > 1)
                {
                    newPlatform = Instantiate(movingPlatforms[Random.Range(0, movingPlatforms.Length)], temp, Quaternion.identity);
                    ScoreTextScript.scoreValue += 1;
                }
                else if (Random.Range(0, 4) > 0)
                {
                    newPlatform = Instantiate(breakablePlatform, temp, Quaternion.identity);
                    ScoreTextScript.scoreValue += 1;
                }
                else
                {
                    newPlatform = Instantiate(freezePlatform, temp, Quaternion.identity);
                    ScoreTextScript.scoreValue += 1;
                }
            }
            else if (ScoreTextScript.scoreValue > 23)
            {
                if (Random.Range(0, 5) > 3)
                {
                    newPlatform = Instantiate(platformPrefab, temp, Quaternion.identity);
                    ScoreTextScript.scoreValue += 1;
                }
                else if (Random.Range(0, 5) > 2)
                {
                    newPlatform = Instantiate(movingPlatforms[Random.Range(0, movingPlatforms.Length)], temp, Quaternion.identity);
                    ScoreTextScript.scoreValue += 1;
                }
                else if (Random.Range(0, 5) > 1)
                {
                    newPlatform = Instantiate(breakablePlatform, temp, Quaternion.identity);
                    ScoreTextScript.scoreValue += 1;
                }
                else if (Random.Range(0, 5) > 0)
                {
                    newPlatform = Instantiate(freezePlatform, temp, Quaternion.identity);
                    ScoreTextScript.scoreValue += 1;
                }
                else
                {
                    newPlatform = Instantiate(beamPlatform, temp, Quaternion.identity);
                    ScoreTextScript.scoreValue += 1;
                }
            }
            if (newPlatform)
                newPlatform.transform.parent = transform;
        }
    }
}
