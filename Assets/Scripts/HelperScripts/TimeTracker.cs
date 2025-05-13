using UnityEngine;

public class PersistentPlayTimeTracker : MonoBehaviour 
{
    // Singleton instance (optional but recommended)
    public static PersistentPlayTimeTracker Instance { get; private set; }

    private float _totalPlayTime; // Total seconds since first launch
    private float _lastUpdateTime; // Timestamp of last update

    void Awake()
    {
        // Singleton pattern (ensures only one instance exists)
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Survives scene changes
        }
        else
        {
            Destroy(gameObject); // Prevents duplicates
            return;
        }

        // Load saved time (implement your save system)
        _totalPlayTime = LocalBackupManager.GetTotalTime();
        _lastUpdateTime = Time.realtimeSinceStartup;
    }

    void Update()
    {
        float currentTime = Time.realtimeSinceStartup;
        _totalPlayTime += currentTime - _lastUpdateTime;
        _lastUpdateTime = currentTime;
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus) SaveTime();
    }

    void OnApplicationQuit()
    {
        SaveTime();
    }

    private void SaveTime()
    {
        LocalBackupManager.IncrementTotalTime(_totalPlayTime);
    }
}