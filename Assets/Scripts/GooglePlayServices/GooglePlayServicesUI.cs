using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_ANDROID
public class GooglePlayServicesUI : MonoBehaviour
{
    public void DisplayLeaderboard()
    {
        GooglePlayServicesManager.ShowLeaderboard();
    }

    public void DisplayAchievements()
    {
        GooglePlayServicesManager.ShowAchievements();
    }
}
#endif