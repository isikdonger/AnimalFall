using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StorePanelScript : MonoBehaviour
{
    [SerializeField] private TMP_Text coinText;
    [SerializeField] private GameObject characterButtonPrefab;
    [SerializeField] private GameObject CharactersParent;
    [SerializeField] private Sprite[] chracterSprites;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coinText.text = LocalBackupManager.GetAvailableCoins().ToString();
        InitiliazeCharacters();
    }

    private void InitiliazeCharacters()
    {
        List<string> lockedCharacters = LocalBackupManager.GetAllCharacters().Except(LocalBackupManager.GetUnlockedCharacters()).ToList();

        foreach (string character in lockedCharacters)
        {
            int index = LocalBackupManager.GetAllCharacters().IndexOf(character);
            GameObject characterButton = Instantiate(characterButtonPrefab, CharactersParent.transform);
            characterButton.transform.SetParent(CharactersParent.transform);
            characterButton.GetComponent<Button>().onClick.AddListener(() => BuyCharacter(character));
            characterButton.name = character;
            characterButton.transform.GetChild(0).GetComponent<TMP_Text>().text = character;
            characterButton.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = chracterSprites[index];
            characterButton.transform.GetChild(2).GetComponent<TMP_Text>().text = LocalBackupManager.GetCharacterPrice(index) + " Coins";
            CharactersParent.GetComponent<RectTransform>().anchoredPosition = new Vector2(int.MaxValue, 0);
        }
    }

    private void BuyCharacter(string characterName)
    {
        //string characterName = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        int characterIndex = LocalBackupManager.GetAllCharacters().IndexOf(characterName);
        int price = LocalBackupManager.GetCharacterPrice(characterIndex);
        if (LocalBackupManager.GetAvailableCoins() >= price)
        {
            LocalBackupManager.UnlockCharacter(characterName);
            LocalBackupManager.SubtractCoins(price);
            coinText.text = LocalBackupManager.GetAvailableCoins().ToString();
            Destroy(GameObject.Find(characterName));
        }
    }

    public void BuyCoins(Button boughtCoin)
    {         
        // Implement the logic to buy coins here
        // This could involve opening a purchase dialog or redirecting to a store
        Debug.Log("Buy Coins button clicked");
    }
}
