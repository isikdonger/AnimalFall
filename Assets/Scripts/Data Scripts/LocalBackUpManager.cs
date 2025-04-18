using System.Collections.Generic;
using UnityEngine;

public static class LocalBackupManager
{
    private const string ProgressFile = "gameProgress.dat";
    private const string UserDataFile = "userData.dat";

    /// <summary>
    /// Loads the player's profile data from local storage.
    /// </summary>
    public static UserData LoadUserData()
    {
        byte[] encryptionKey = FirestoreManager.GetEncryptionKey();
        if (encryptionKey == null || encryptionKey.Length != 32)
        {
            Debug.LogError("Encryption key not available.");
            return new UserData();
        }

        string jsonData = SecureDataManager.LoadEncryptedData(UserDataFile, encryptionKey);
        if (!string.IsNullOrEmpty(jsonData))
        {
            Debug.Log("Loaded progress from local storage.");
            return JsonUtility.FromJson<UserData>(jsonData);
        }
        return new UserData();
    }

    /// <summary>
    /// Loads the player's progress from local storage.
    /// </summary>
    public static GameProgress LoadProgress()
    {
        byte[] encryptionKey = FirestoreManager.GetEncryptionKey();
        if (encryptionKey == null || encryptionKey.Length != 32)
        {
            Debug.LogError("Encryption key not available.");
            return new GameProgress();
        }

        string jsonData = SecureDataManager.LoadEncryptedData(ProgressFile, encryptionKey);
        if (!string.IsNullOrEmpty(jsonData))
        {
            Debug.Log("Loaded progress from local storage.");
            return JsonUtility.FromJson<GameProgress>(jsonData);
        }
        return new GameProgress();
    }

    /// <summary>
    /// Saves the player's profile data to local storage.
    /// </summary>
    public static void SaveUserData(UserData data)
    {
        if (data == null)
        {
            Debug.LogError("Progress data is null.");
            return;
        }

        byte[] encryptionKey = FirestoreManager.GetEncryptionKey();
        if (encryptionKey == null || encryptionKey.Length != 32)
        {
            Debug.LogError("Encryption key not available.");
            return;
        }

        string jsonData = JsonUtility.ToJson(data);
        SecureDataManager.SaveEncryptedData(UserDataFile, jsonData, encryptionKey);
        Debug.Log("Game progress saved locally.");
    }

    /// <summary>
    /// Saves the player's progress to local storage.
    /// </summary>
    public static void SaveProgress(GameProgress progress)
    {
        if (progress == null)
        {
            Debug.LogError("Progress data is null.");
            return;
        }

        byte[] encryptionKey = FirestoreManager.GetEncryptionKey();
        if (encryptionKey == null || encryptionKey.Length != 32)
        {
            Debug.LogError("Encryption key not available.");
            return;
        }

        string jsonData = JsonUtility.ToJson(progress);
        SecureDataManager.SaveEncryptedData(ProgressFile, jsonData, encryptionKey);
        Debug.Log("Game progress saved locally.");
    }

    /// <summary>
    /// Increments the total games in user data.
    /// </summary>
    public static void IncrementTotalGames()
    {
        UserData data = LoadUserData();
        data.totalGames++;
        SaveUserData(data);
    }

    /// <summary>
    /// Increments the total score in user data.
    /// </summary>
    public static void IncrementTotalScore(int score)
    {
        UserData data = LoadUserData();
        data.totalScore += score;
        SaveUserData(data);
    }

    /// <summary>
    /// Increments the total coins gained in user data.
    /// </summary>
    public static void IncrementTotalCoins(int coinsGained)
    {
        UserData data = LoadUserData();
        data.totalCoins += coinsGained;
        SaveUserData(data);
    }

    /// <summary>
    /// Adds spent coins to coin spent in user data.
    /// </summary>
    public static void IncrementCoinSpent(int amount)
    {
        UserData data = LoadUserData();
        data.coinSpent += amount;
        SaveUserData(data);
    }

    /// <summary>
    /// Increments the total achievements completed in user data.
    /// </summary>
    public static void IncrementCompletedAchievements()
    {
        UserData data = LoadUserData();
        data.totalCoins++;
        SaveUserData(data);
    }

    /// <summary>
    /// Updates the high score in local progress.
    /// </summary>
    public static void SetHighScore(int newHighScore)
    {
        GameProgress progress = LoadProgress();
        progress.highScore = Mathf.Max(progress.highScore, newHighScore);
        SaveProgress(progress);
    }

    /// <summary>
    /// Updates the used characters in local progress.
    /// </summary>
    public static void AddUsedCharacter(string character)
    {
        GameProgress progress = LoadProgress();
        if (!progress.usedCharacters.Contains(character))  // Avoid duplicates
        {
            progress.usedCharacters.Add(character);
            IncrementCharacterCount();
            SaveProgress(progress);
        }
    }

    /// <summary>
    /// Increments the character count in local progress.
    /// </summary>
    private static void IncrementCharacterCount()
    {
        GameProgress progress = LoadProgress();
        progress.characterCount++;
        SaveProgress(progress);
    }

    /// <summary>
    /// Increments the break count in local progress.
    /// </summary>
    public static void IncrementBreakCount()
    {
        GameProgress progress = LoadProgress();
        progress.breakCount++;
        SaveProgress(progress);
    }

    /// <summary>
    /// Increments the spike death count in local progress.
    /// </summary>
    public static void IncrementSpikeDeathCount()
    {
        GameProgress progress = LoadProgress();
        progress.spikeDeathCount++;
        SaveProgress(progress);
    }

    /// <summary>
    /// Increments the loss count in local progress.
    /// </summary>
    public static void IncrementLossCount()
    {
        GameProgress progress = LoadProgress();
        progress.lossCount++;
        SaveProgress(progress);
    }

    /// <summary>
    /// Resets the loss count in local progress.
    /// </summary>
    public static void ResetLossCount()
    {
        GameProgress progress = LoadProgress();
        progress.lossCount = 0;
        SaveProgress(progress);
    }

    /// <summary>
    /// Increments the win count in local progress.
    /// </summary>
    public static void IncrementWinCount()
    {
        GameProgress progress = LoadProgress();
        progress.winCount++;
        SaveProgress(progress);
    }

    /// <summary>
    /// Resets the win count in local progress.
    /// </summary>
    public static void ResetWinCount()
    {
        GameProgress progress = LoadProgress();
        progress.winCount = 0;
        SaveProgress(progress);
    }

    /// <summary>
    /// Gets the total games from user data.
    /// </summary>
    public static int GetTotalGames() => LoadUserData().totalGames;

    /// <summary>
    /// Gets the total score from user data.
    /// </summary>
    public static int GetTotalScore() => LoadUserData().totalScore;

    /// <summary>
    /// Gets the total coins gained from user data.
    /// </summary>
    public static int GetTotalCoins() => LoadUserData().totalCoins;

    /// <summary>
    /// Gets the coins spent from user data.
    /// </summary>
    public static int GetSpentCoins() => LoadUserData().coinSpent;

    /// <summary>
    /// Gets the count of completed achievements from user data.
    /// </summary>
    public static int GetCompletedAchievements() => LoadUserData().achievementsCompleted;

    /// <summary>
    /// Gets the high score from local progress.
    /// </summary>
    public static int GetHighScore() => LoadProgress().highScore;

    /// <summary>
    /// Gets the character count from local progress.
    /// </summary>
    public static List<string> GetUsedCharacters() => LoadProgress().usedCharacters;

    /// <summary>
    /// Gets the character count from local progress.
    /// </summary>
    public static int GetCharacterCount() => LoadProgress().characterCount;

    /// <summary>
    /// Gets the break count from local progress.
    /// </summary>
    public static int GetBreakCount() => LoadProgress().breakCount;

    /// <summary>
    /// Gets the spike death count from local progress.
    /// </summary>
    public static int GetSpikeDeathCount() => LoadProgress().spikeDeathCount;

    /// <summary>
    /// Gets the loss count from local progress.
    /// </summary>
    public static int GetLossCount() => LoadProgress().lossCount;

    /// <summary>
    /// Gets the loss count from local progress.
    /// </summary>
    public static int GetWinCount() => LoadProgress().winCount;
}