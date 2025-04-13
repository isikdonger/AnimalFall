using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatfromSpawner : MonoBehaviour
{
    public ScoreTextScript scoreTextScript;
    public GameObject platformPrefab;
    public GameObject breakablePlatform;
    public GameObject freezePlatform;
    public GameObject beamPlatform;
    public GameObject[] movingPlatforms;
    public static float platform_Spawn_Timer;
    public static float current_Platform_Spawn_Timer;
    private float cameraHeight, cameraWidth;
    private float min_X, max_X;
    public static bool isPaused;

    private void Start()
    {
        Bounds prefabBound = platformPrefab.GetComponent<Renderer>().bounds;
        cameraHeight = Camera.main.orthographicSize * 2;
        cameraWidth = cameraHeight * Camera.main.aspect;
        min_X = Camera.main.transform.position.x - cameraWidth / 2 + prefabBound.size.x / 2;
        max_X = Camera.main.transform.position.x + cameraWidth / 2 - prefabBound.size.x / 2;
    }

    public static void InitiliazeGame()
    {
        platform_Spawn_Timer = 7.5f;
        current_Platform_Spawn_Timer = 0f;
        isPaused = false;
    }

    private void FixedUpdate()
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
            scoreTextScript.AddScore();
            if (ScoreTextScript.scoreValue <= 5)
            {
                newPlatform = Instantiate(platformPrefab, temp, Quaternion.identity);
            }
            else if (ScoreTextScript.scoreValue > 5 && ScoreTextScript.scoreValue <= 10)
            {
                if (Random.Range(0, 2) > 0)
                {
                    newPlatform = Instantiate(platformPrefab, temp, Quaternion.identity);
                }
                else
                {
                    newPlatform = Instantiate(movingPlatforms[Random.Range(0, movingPlatforms.Length)], temp, Quaternion.identity);
                }
            }
            else if (ScoreTextScript.scoreValue > 10 && ScoreTextScript.scoreValue <= 16)
            {
                if (Random.Range(0, 3) > 1)
                {
                    newPlatform = Instantiate(platformPrefab, temp, Quaternion.identity);
                }
                else if (Random.Range(0, 3) > 0)
                {
                    newPlatform = Instantiate(movingPlatforms[Random.Range(0, movingPlatforms.Length)], temp, Quaternion.identity);
                }
                else
                {
                    newPlatform = Instantiate(breakablePlatform, temp, Quaternion.identity);
                }
            }
            else if (ScoreTextScript.scoreValue > 16 && ScoreTextScript.scoreValue <= 23)
            {
                if (Random.Range(0, 4) > 2)
                {
                    newPlatform = Instantiate(platformPrefab, temp, Quaternion.identity);
                }
                else if (Random.Range(0, 4) > 1)
                {
                    newPlatform = Instantiate(movingPlatforms[Random.Range(0, movingPlatforms.Length)], temp, Quaternion.identity);
                }
                else if (Random.Range(0, 4) > 0)
                {
                    newPlatform = Instantiate(breakablePlatform, temp, Quaternion.identity);
                }
                else
                {
                    newPlatform = Instantiate(freezePlatform, temp, Quaternion.identity);
                }
            }
            else if (ScoreTextScript.scoreValue > 23)
            {
                if (Random.Range(0, 5) > 3)
                {
                    newPlatform = Instantiate(platformPrefab, temp, Quaternion.identity);
                }
                else if (Random.Range(0, 5) > 2)
                {
                    newPlatform = Instantiate(movingPlatforms[Random.Range(0, movingPlatforms.Length)], temp, Quaternion.identity);
                }
                else if (Random.Range(0, 5) > 1)
                {
                    newPlatform = Instantiate(breakablePlatform, temp, Quaternion.identity);
                }
                else if (Random.Range(0, 5) > 0)
                {
                    newPlatform = Instantiate(freezePlatform, temp, Quaternion.identity);
                }
                else
                {
                    newPlatform = Instantiate(beamPlatform, temp, Quaternion.identity);
                }
            }
            if (newPlatform)
                newPlatform.transform.parent = transform;
        }
    }
}
