using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject freezeController;
    public Sprite[] characterSprites;

    private void Awake()
    {
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        if (!LocalBackupManager.GetUnlockedCharacters().Contains(LocalBackupManager.GetAllCharacters()[PlayerPrefs.GetInt("characterIndex")]))
        {
            PlayerPrefs.SetInt("characterIndex", 0);
        }
        playerPrefab.GetComponent<SpriteRenderer>().sprite = characterSprites[PlayerPrefs.GetInt("characterIndex")];
        GameObject playerInstance = Instantiate(playerPrefab, new Vector3(0, 2, 0), Quaternion.identity);
        GameObject childInstance = Instantiate(freezeController, playerInstance.transform);
        childInstance.transform.SetParent(playerInstance.transform);
    }
}
