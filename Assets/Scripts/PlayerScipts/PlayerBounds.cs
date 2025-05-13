using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBounds : MonoBehaviour
{
    private float cameraHeight, cameraWidth;
    private float min_X, max_X, min_Y;
    private bool out_of_Bounds;

    private void Awake()
    {
        Bounds prefabBound = GetComponent<Renderer>().bounds;
        cameraHeight = Camera.main.orthographicSize * 2;
        cameraWidth = cameraHeight * Camera.main.aspect;
        min_X = Camera.main.transform.position.x - cameraWidth / 2 + prefabBound.size.x / 2;
        max_X = Camera.main.transform.position.x + cameraWidth / 2 - prefabBound.size.x / 2;
        min_Y = Camera.main.transform.position.y - cameraHeight / 2 - 0.5f;
    }
    void Update()
    {
        CheckBounds();
    }
    void CheckBounds()
    {
        Vector2 temp = transform.position;
        if (temp.x > max_X)
        {
            temp.x = max_X;
        }
        if (temp.x < min_X)
        {
            temp.x = min_X;
        }
        transform.position = temp;
        if (temp.y <= min_Y)
        {
            if (!out_of_Bounds)
            {
                out_of_Bounds = true;
                //SoundManager.instance.DeathSound();
                GameManager.Instance.Death("Fell");
                LocalBackupManager.ResetStandartCount();
                if (LocalBackupManager.GetAllCharacters()[PlayerPrefs.GetInt("characterIndex")].Equals("Ocean"))
                {
                    LocalBackupManager.ResetFreezeCount();
                }
                else if (LocalBackupManager.GetAllCharacters()[PlayerPrefs.GetInt("characterIndex")].Equals("Moon"))
                {
                    LocalBackupManager.ResetSpikeCount();
                }
            }
        }
    }
    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "TopSpike")
        {
            //SoundManager.instance.DeathSound();
            Destroy(gameObject);
            GameManager.Instance.Death("Spiked");
            LocalBackupManager.ResetStandartCount();
            if (LocalBackupManager.GetAllCharacters()[PlayerPrefs.GetInt("characterIndex")].Equals("Moon"))
            {
                LocalBackupManager.IncrementSpikeCount();
                if (LocalBackupManager.GetSpikeDeathCount() == 5)
                {
#if UNITY_ANDROID
                    GooglePlayServicesManager.UnlockAchievement("Thats Rough Buddy");
#elif UNITY_IOS
                    GameCenterManager.UnlockAchievement("Thats Rough Buddy");
#endif
                }
            }
            else if (LocalBackupManager.GetAllCharacters()[PlayerPrefs.GetInt("characterIndex")].Equals("Ocean"))
            {
                LocalBackupManager.ResetFreezeCount();
            }
        }
        else if (target.tag ==  "KillCoin")
        {
            //SoundManager.instance.DeathSound();
            Destroy(gameObject);
            GameManager.Instance.Death("Killed");
            LocalBackupManager.ResetStandartCount();
            if (LocalBackupManager.GetAllCharacters()[PlayerPrefs.GetInt("characterIndex")].Equals("Ocean"))
            {
                LocalBackupManager.ResetFreezeCount();
            }
            else if (LocalBackupManager.GetAllCharacters()[PlayerPrefs.GetInt("characterIndex")].Equals("Moon"))
            {
                LocalBackupManager.ResetSpikeCount();
            }
        }
    }
}
