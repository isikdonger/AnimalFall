using UnityEngine;
using UnityEngine.UI;

public class CoinTextScript : MonoBehaviour
{
    public static int coinAmount;
    private static Text coinText;

    private void Start()
    {
        coinText = GetComponent<Text>();
    }

    public static void InitiliazeGame()
    {
        coinAmount = 0; // Reset coin amount to 0
        AlignCoinText();
    }

    public static void AddCoins(int amount)
    {
        coinAmount += amount;
        AlignCoinText();
    }

    private static void AlignCoinText()
    {
        if (coinAmount < 1000)
            coinText.text = coinAmount.ToString();
        else if (coinAmount < 1000000)
            coinText.text = (coinAmount / 1000) + "K";
        else if (coinAmount < 1000000000)
            coinText.text = (coinAmount / 1000000) + "M";
        else
            coinText.text = "F U";
    }
}
