using UnityEngine;
using UnityEngine.UI;

public class CoinTextScript : MonoBehaviour
{
    public static long coinAmount;
    private static Text coinText;

    private void Start()
    {
        coinText = GetComponent<Text>();
        coinAmount = 0;
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
        else if (coinAmount < 1000000000000)
            coinText.text = (coinAmount / 1000000000) + "B";
        else if (coinAmount < 1000000000000000)
            coinText.text = (coinAmount / 1000000000000) + "T";
        else if (coinAmount < 1000000000000000000)
            coinText.text = (coinAmount / 1000000000000000) + "Q";
        else
            coinText.text = "F U";
    }
}
