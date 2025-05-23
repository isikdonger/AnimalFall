﻿using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CustomizePanelScript : MonoBehaviour
{
    private GameObject CharactersPanel;

    private void Start()
    {
        CharactersPanel = CentralUIController.Instance.charactersPanel;
        LockCharacters();
    }

    private void LockCharacters()
    {
        List<string> unlockedCharacters = LocalBackupManager.GetUnlockedCharacters();

        foreach (Transform child in CharactersPanel.transform)
        {
            string childName = child.name;
            Debug.Log("Child Name: " + childName);
            string characterName = childName.Remove(childName.Length - 3); // Remove the "Btn"
            Debug.Log("Character Name: " + characterName);
            if (unlockedCharacters.Contains(characterName))
            {
                Debug.Log("Unlocked Character: " + characterName);
                child.GetChild(1).gameObject.SetActive(false);
            }
        }
    }

    public void OnCharacterClick(GameObject Button)
    {
        string buttonName = Button.name;
        string characterName = buttonName.Remove(buttonName.Length - 3); // Remove the "Btn"
        if (LocalBackupManager.GetUnlockedCharacters().Contains(characterName))
        {
            ChangeCharacter(Button);
        }
        else
        {
            CentralUIController.Instance.ToggleMenu(CentralUIController.Instance.storePanel.GetComponent<CanvasGroup>());
        }
    }

    private void ChangeCharacter(GameObject Button)
    {
        int index = Button.transform.GetSiblingIndex();
        LocalBackupManager.AddUsedCharacter(LocalBackupManager.GetAllCharacters()[index]);
        if (LocalBackupManager.GetCharacterCount() == 3)
        {
#if UNITY_ANDROID
            Debug.Log("Unity Android");
            GooglePlayServicesManager.UnlockAchievementCoroutine("This is Getting Out of Hand");
#elif UNITY_IOS
            GameCenterManager.UnlockAchievementCoroutine("This is Getting Out of Hand");
#endif
        }
        PlayerPrefs.SetInt("characterIndex", index);
        CentralUIController.Instance.ToggleMenu(CharactersPanel.GetComponent<CanvasGroup>());
    }
}