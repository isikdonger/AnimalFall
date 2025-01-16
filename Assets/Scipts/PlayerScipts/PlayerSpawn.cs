using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject[] characters;

    private void Awake()
    {
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        Vector2 temp = transform.position;
        temp.x = 0f;
        temp.x = 0f;
        Instantiate(characters[0], temp, Quaternion.identity);
    }
}
