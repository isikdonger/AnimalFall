using UnityEngine;

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