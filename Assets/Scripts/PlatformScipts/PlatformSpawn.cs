using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawn : MonoBehaviour
{
    public GameObject standartplatform;

    private void Awake()
    {
        SpawnPlatform();
    }

    public void SpawnPlatform()
    {
        GameObject platform = null;
        platform = Instantiate(standartplatform, transform.position, Quaternion.identity);
    }
}
