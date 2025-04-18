using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public bool bronzeCoin, silverCoin, goldCoin, killCoin;
    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == "Player")
        {
            if (bronzeCoin)
            {
                //SoundManager.instance.CoinPickClip();
                CoinTextScript.AddCoins(1);
                Destroy(gameObject);
            }
            if (silverCoin)
            {
                //SoundManager.instance.CoinPickClip();
                CoinTextScript.AddCoins(2);
                Destroy(gameObject);
            }
            if (goldCoin)
            {
                //SoundManager.instance.CoinPickClip();
                CoinTextScript.AddCoins(3);
                Destroy(gameObject);
            }
            if (killCoin)
            {
                //SoundManager.instance.DeathSound();
                Destroy(gameObject);
            }
        }
    }
}
