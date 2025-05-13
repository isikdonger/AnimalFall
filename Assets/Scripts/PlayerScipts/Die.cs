using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    public static Animator animator;
    private void Awake()
    {
        GetComponent<SpriteRenderer>().sprite = null;
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        animator.ResetTrigger("Idle");
    }
    public void Freeze()
    {
        GameManager.Instance.Death("Froze");
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        if (LocalBackupManager.GetAllCharacters()[PlayerPrefs.GetInt("characterIndex")].Equals("Ocean"))
        {
            LocalBackupManager.IncrementFreezeCount();
            if (LocalBackupManager.GetFreezeCount() == 5)
            {
#if UNITY_ANDROID
                GooglePlayServicesManager.UnlockAchievement("Water Tribe");
#elif UNITY_IOS
                GameCenterManager.UnlockAchievement("Water Tribe");
#endif
            }
        }
        else if (LocalBackupManager.GetAllCharacters()[PlayerPrefs.GetInt("characterIndex")].Equals("Moon"))
        {
            LocalBackupManager.ResetSpikeCount();
        }
    }
    public void Unfreeze()
    {
        animator.SetTrigger("Idle");
    }
}
