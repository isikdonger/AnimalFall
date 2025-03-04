using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject playerParent;
    public Sprite[] characterSprites;

    private void Awake()
    {
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        playerPrefab.GetComponent<SpriteRenderer>().sprite = characterSprites[PlayerPrefs.GetInt("characterIndex")];
        Instantiate(playerPrefab, playerParent.transform);
    }
}
