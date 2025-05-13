using System;
using System.Collections.Generic;
using UnityEngine;

public static class LocalBackupManager
{
    private const string UserDataFile = "userData.dat";
    private const string ObjectivesProgressFile = "objectivesProgress.dat";
    private const string ProgressFile = "gameProgress.dat";
    private const string StoreDataFile = "storeData.dat";

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
    /// Saves the player's objectives progress to local storage.
    /// </summary>
    public static void SaveObjectivesProgress(ObjectivesProgress progress)
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
        SecureDataManager.SaveEncryptedData(ObjectivesProgressFile, jsonData, encryptionKey);
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
    /// Saves the store data to local storage.
    /// </summary>
    public static void SaveStoreData(StoreData data)
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
        SecureDataManager.SaveEncryptedData(StoreDataFile, jsonData, encryptionKey);
        Debug.Log("Game progress saved locally.");
    }

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
    /// Loads the player's objectives progress from local storage.
    /// </summary>
    public static ObjectivesProgress LoadObjectivesProgress()
    {
        byte[] encryptionKey = FirestoreManager.GetEncryptionKey();
        if (encryptionKey == null || encryptionKey.Length != 32)
        {
            Debug.LogError("Encryption key not available.");
            return new ObjectivesProgress();
        }

        string jsonData = SecureDataManager.LoadEncryptedData(ObjectivesProgressFile, encryptionKey);
        if (!string.IsNullOrEmpty(jsonData))
        {
            Debug.Log("Loaded progress from local storage.");
            return JsonUtility.FromJson<ObjectivesProgress>(jsonData);
        }
        return new ObjectivesProgress();
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
    /// Loads the store data from local storage.
    /// </summary>
    public static StoreData LoadStoreData()
    {
        byte[] encryptionKey = FirestoreManager.GetEncryptionKey();
        if (encryptionKey == null || encryptionKey.Length != 32)
        {
            Debug.LogError("Encryption key not available.");
            return new StoreData();
        }

        string jsonData = SecureDataManager.LoadEncryptedData(StoreDataFile, encryptionKey);
        if (!string.IsNullOrEmpty(jsonData))
        {
            Debug.Log("Loaded progress from local storage.");
            return JsonUtility.FromJson<StoreData>(jsonData);
        }
        return new StoreData();
    }

    /// <summary>
    /// Updates the high score in local progress.
    /// </summary>
    public static void SetHighScore(int newHighScore)
    {
        UserData data = LoadUserData();
        data.highScore = Math.Max(data.highScore, newHighScore);
        SaveUserData(data);
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
    public static void IncrementCoinsGained(int coinsGained)
    {
        UserData data = LoadUserData();
        data.coinsGained += coinsGained;
        SaveUserData(data);
    }

    /// <summary>
    /// Increments the total time played in user data.
    /// </summary>
    public static void IncrementTotalTime(float time)
    {
        UserData data = LoadUserData();
        data.totalTimePlayed += time;
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
        data.coinsGained++;
        SaveUserData(data);
    }

    /// <summary>
    /// Increments the step of the score objective in objectives progress.
    /// </summary>
    public static void IncrementScoreObjectiveStep()
    {
        ObjectivesProgress progress = LoadObjectivesProgress();
        if (progress.scoreObjectiveStep < progress.scoreGoals.Count - 1)
        {
            progress.scoreObjectiveStep++;
            SaveObjectivesProgress(progress);
        }
    }

    /// <summary>
    /// Increments the step of the coin objective in objectives progress.
    /// </summary>
    public static void IncrementCoinObjectiveStep()
    {
        ObjectivesProgress progress = LoadObjectivesProgress();
        if (progress.coinObjectiveStep < progress.scoreGoals.Count - 1)
        {
            progress.coinObjectiveStep++;
            SaveObjectivesProgress(progress);
        }
    }

    /// <summary>
    /// Increments the step of the time objective in objectives progress.
    /// </summary>
    public static void IncrementTimeObjectiveStep()
    {
        ObjectivesProgress progress = LoadObjectivesProgress();
        if (progress.timeObjectiveStep < progress.scoreGoals.Count - 1)
        {
            progress.timeObjectiveStep++;
            SaveObjectivesProgress(progress);
        }
    }

    /// <summary>
    /// Updates the used characters in local progress.
    /// </summary>
    public static void AddUsedCharacter(string character)
    {
        GameProgress progress = LoadProgress();
        Debug.Log("Adding character: " + character);
        if (!progress.usedCharacters.Contains(character))  // Avoid duplicates
        {
            progress.usedCharacters.Add(character);
            IncrementCharacterCount();
            SaveProgress(progress);
            Debug.Log("Character added: " + character);
        }
    }

    /// <summary>
    /// Increments the character count in local progress.
    /// </summary>
    private static void IncrementCharacterCount()
    {
        GameProgress progress = LoadProgress();
        Debug.Log("Incrementing character count: " + progress.characterCount);
        progress.characterCount++;
        SaveProgress(progress);
        Debug.Log("Character count incremented: " + progress.characterCount);
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
    /// Unlocks a character in the store data.
    /// </summary>
    public static void UnlockCharacter(string character)
    {
        StoreData data = LoadStoreData();
        if (data.Characters.Contains(character) && !data.unlockedCharacters.Contains(character))
        {
            data.unlockedCharacters.Add(character);
            SaveStoreData(data);
        }
    }

    /// <summary>
    /// Adds coins to the store data.
    /// </summary>
    public static void AddCoins(int amount)
    {
        StoreData data = LoadStoreData();
        data.avaliableCoins += amount;
        SaveStoreData(data);
    }

    /// <summary>
    /// Subtracts coins to the store data.
    /// </summary>
    public static void SubtractCoins(int amount)
    {
        StoreData data = LoadStoreData();
        data.avaliableCoins -= amount;
        SaveStoreData(data);
    }

    /// <summary>
    /// Gets the high score from local progress.
    /// </summary>
    public static int GetHighScore() => LoadUserData().highScore;

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
    public static int GetCoinsGained() => LoadUserData().coinsGained;

    /// <summary>
    /// Gets the total time played from user data.
    /// </summary>
    public static float GetTotalTime() => LoadUserData().totalTimePlayed;

    /// <summary>
    /// Gets the coins spent from user data.
    /// </summary>
    public static int GetSpentCoins() => LoadUserData().coinSpent;

    /// <summary>
    /// Gets the count of completed achievements from user data.
    /// </summary>
    public static int GetCompletedAchievements() => LoadUserData().achievementsCompleted;

    /// <summary>
    /// Gets the current reward of score objective in objectives progress.
    /// </summary>
    public static int GetScoreReward() => LoadObjectivesProgress().objectiveRewards[LoadObjectivesProgress().scoreObjectiveStep];

    /// <summary>
    /// Gets the current reward of coin objective in objectives progress.
    /// </summary>
    public static int GetCoinReward() => LoadObjectivesProgress().objectiveRewards[LoadObjectivesProgress().coinObjectiveStep];

    /// <summary>
    /// Gets the current reward of time objective in objectives progress.
    /// </summary>
    public static int GetTimeReward() => LoadObjectivesProgress().objectiveRewards[LoadObjectivesProgress().timeObjectiveStep];

    /// <summary>
    /// Gets the current goal of the score objective in objectives progress.
    /// </summary>
    public static int GetScoreGoal() => LoadObjectivesProgress().scoreGoals[LoadObjectivesProgress().scoreObjectiveStep];

    /// <summary>
    /// Gets the current goal of the coin objective in objectives progress.
    /// </summary>
    public static int GetCoinGoal() => LoadObjectivesProgress().coinGoals[LoadObjectivesProgress().timeObjectiveStep];

    /// <summary>
    /// Gets the current goal of the time objective in objectives progress.
    /// </summary>
    public static int GetTimeGoal() => LoadObjectivesProgress().timeGoals[LoadObjectivesProgress().timeObjectiveStep];

    /// <summary>
    /// Gets the step of the score objective in objectives progress.
    /// </summary>
    public static int GetScoreObjectiveStep() => LoadObjectivesProgress().scoreObjectiveStep < 10 ? LoadObjectivesProgress().scoreObjectiveStep : -1;

    /// <summary>
    /// Gets the step of the coin objective in objectives progress.
    /// </summary>
    public static int GetCoinObjectiveStep() => LoadObjectivesProgress().coinObjectiveStep < 10 ? LoadObjectivesProgress().coinObjectiveStep : -1;

    /// <summary>
    /// Gets the step of the time objective in objectives progress.
    /// </summary>
    public static int GetTimeObjectiveStep() => LoadObjectivesProgress().timeObjectiveStep < 10 ? LoadObjectivesProgress().timeObjectiveStep : -1;

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

    /// <summary>
    /// Gets all the characters from store data.
    /// <summary>
    public static List<string> GetAllCharacters() => LoadStoreData().Characters;

    /// <summary>
    /// Gets the index of a character from store data.
    /// <summary>
    public static int GetCharacterIndex(string characterName) => LoadStoreData().Characters.IndexOf(characterName);

    /// <summary>
    /// Gets a characters price from store data.
    /// <summary>
    public static int GetCharacterPrice(int index) { return LoadStoreData().characterPrices[index]; }

    /// <summary>
    /// Gets a coin purchases price from store data.
    /// <summary>
    public static int GetCoinPrice(int index) { return LoadStoreData().coinPrices[index]; }

    /// <summary>
    /// Gets the unlocked characters from store data.
    /// <summary>
    public static List<string> GetUnlockedCharacters() => LoadStoreData().unlockedCharacters;

    /// <summary>
    /// Gets the avaliable coins from store data.
    /// <summary>
    public static int GetAvailableCoins() => LoadStoreData().avaliableCoins;
}