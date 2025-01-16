using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawn : MonoBehaviour
{
    public GameObject standartplatform;

    private void Start()
    {
        SpawnPlatform();
    }

    public void SpawnPlatform()
    {
        Vector2 temp = transform.position;
        temp.x = 0f;
        temp.x = 0f;
        GameObject platform = null;
        platform = Instantiate(standartplatform, temp, Quaternion.identity);
    }
}
