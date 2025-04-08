using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

#if !UNITY_EDITOR

[Serializable]
public class GameProgress
{
    public int highScore;
    public List<string> usedCharacters = new List<string>();
    public int characterCount;
    public int breakCount;
    public int spikeDeathCount;
    public int coinSpent;
    public int lossCount;
    public int winCount;
}

public static class LocalBackupManager
{
    private const string ProgressFile = "gameProgress.dat";

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
    /// Adds coins to the local progress.
    /// </summary>
    public static void AddCoins(int amount)
    {
        GameProgress progress = LoadProgress();
        progress.coinSpent += amount;
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
    /// Gets the coins spent from local progress.
    /// </summary>
    public static int GetSpentCoins() => LoadProgress().coinSpent;

    /// <summary>
    /// Gets the loss count from local progress.
    /// </summary>
    public static int GetLossCount() => LoadProgress().lossCount;

    /// <summary>
    /// Gets the loss count from local progress.
    /// </summary>
    public static int GetWinCount() => LoadProgress().winCount;
}
#endif