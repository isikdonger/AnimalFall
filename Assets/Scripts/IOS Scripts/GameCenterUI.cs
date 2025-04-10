using System;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_IOS
public class GameCenterUI : MonoBehaviour
{
    public void DisplayLeaderboard()
    {
        GameCenterManager.ShowLeaderboard();
    }

    public void DisplayAchievements()
    {
        GameCenterManager.ShowAchievements();
    }
}
#endif
