using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField]
    private AudioSource soundFX;
    [SerializeField]
    private AudioClip landClip, deathClip, breakClip, gameOverClip, freezeClip, coinPickClip;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void LandSound()
    {
        soundFX.clip = landClip;
        soundFX.Play();
    }
    public void BreakSound()
    {
        soundFX.clip = breakClip;
        soundFX.Play();
    }
    public void DeathSound()
    {
        soundFX.clip = deathClip;
        soundFX.Play();
    }
    public void GameOverSound()
    {
        soundFX.clip = gameOverClip;
        soundFX.Play();
    }
    public void FreezeClip()
    {
        soundFX.clip = freezeClip;
        soundFX.Play();
    }
    public void CoinPickClip()
    {
        soundFX.clip = coinPickClip;
        soundFX.Play();
    }
}
