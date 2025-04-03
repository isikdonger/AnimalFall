using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinTextScript : MonoBehaviour
{
    public static int coinAmount = 0;
    Text coin;
    void Start()
    {
        coin = GetComponent<Text>();
    }
    void Update()
    {
        if (coinAmount < 1000)
        {
            coin.text = coinAmount.ToString();
        }
        if (coinAmount >= 1000 && coinAmount < 2000)
        {
            coin.text = "1k";
        }
        if (coinAmount >= 2000 && coinAmount < 3000)
        {
            coin.text = "2k";
        }
        if (coinAmount >= 3000 && coinAmount < 4000)
        {
            coin.text = "3k";
        }
        if (coinAmount >= 4000 && coinAmount < 5000)
        {
            coin.text = "4k";
        }
    }
}
