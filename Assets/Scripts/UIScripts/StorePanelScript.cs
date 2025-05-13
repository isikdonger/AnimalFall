using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Tables;
using UnityEngine.UI;

public class StorePanelScript : MonoBehaviour
{
    [SerializeField] private TMP_Text coinText;
    [SerializeField] private GameObject characterButtonPrefab;
    [SerializeField] private GameObject CharactersParent;
    [SerializeField] private Sprite[] chracterSprites;
    private GameObject CharactersPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coinText.text = LocalBackupManager.GetAvailableCoins().ToString();
        CharactersPanel = CentralUIController.Instance.charactersPanel;
        InitiliazeCharacters();
    }

    private void InitiliazeCharacters()
    {
        List<string> lockedCharacters = LocalBackupManager.GetAllCharacters().Except(LocalBackupManager.GetUnlockedCharacters()).ToList();

        foreach (string character in lockedCharacters)
        {
            int index = LocalBackupManager.GetCharacterIndex(character);
            GameObject characterButton = Instantiate(characterButtonPrefab, CharactersParent.transform);
            characterButton.transform.SetParent(CharactersParent.transform);
            characterButton.GetComponent<Button>().onClick.AddListener(() => BuyCharacter(character));
            characterButton.name = character;
            LocalizeStringEvent nameLocalizer = characterButton.transform.GetChild(0).GetComponent<LocalizeStringEvent>();
            LocalizedString localizedName = new LocalizedString { TableReference = "UI Strings", TableEntryReference = character };
            nameLocalizer.StringReference = localizedName;
            characterButton.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = chracterSprites[index];
            characterButton.transform.GetChild(2).GetComponent<LocalizeStringEvent>().StringReference.Arguments = new object[] { LocalBackupManager.GetCharacterPrice(index) };
            CharactersParent.GetComponent<RectTransform>().anchoredPosition = new Vector2(int.MaxValue, 0);
        }
    }

    private void BuyCharacter(string characterName)
    {
        //string characterName = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        int characterIndex = LocalBackupManager.GetCharacterIndex(characterName);
        int price = LocalBackupManager.GetCharacterPrice(characterIndex);
        if (LocalBackupManager.GetAvailableCoins() >= price)
        {
            LocalBackupManager.UnlockCharacter(characterName);
            LocalBackupManager.SubtractCoins(price);
            LocalBackupManager.IncrementCoinSpent(price);
            CoinSpentAchievement();
            coinText.text = LocalBackupManager.GetAvailableCoins().ToString();
            CharactersPanel.transform.GetChild(characterIndex).GetChild(1).gameObject.SetActive(false);
            Destroy(GameObject.Find(characterName));
        }
    }

    private void CoinSpentAchievement()
    {
        int coinSpent = LocalBackupManager.GetSpentCoins();
        if (coinSpent == 2000)
        {
#if UNITY_ANDROID
            GooglePlayServicesManager.UnlockAchievement("Hey Big Spender");
#elif UNITY_IOS
            GameCenterManager.UnlockAchievement("Hey Big Spender");
#endif
        }
    }

    public void BuyCoins(Button boughtCoin)
    {         
        // Implement the logic to buy coins here
        // This could involve opening a purchase dialog or redirecting to a store
        Debug.Log("Buy Coins button clicked");
    }
}