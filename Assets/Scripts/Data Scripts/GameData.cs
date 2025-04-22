using System.Collections.Generic;
using System;

[Serializable]
public class UserData
{
    public int highScore;
    public int totalGames;
    public int totalScore;
    public int coinsGained;
    public float totalTimePlayed;
    public int coinSpent;
    public int achievementsCompleted;
}

public class ObjectivesProgress
{
    public readonly List<int> objectiveRewards = new List<int> { 1, 1, 1, 3, 3, 3, 5, 5, 5, 15 };
    public readonly List<int> scoreGoals = new List<int> { 10, 25, 50, 100, 250, 500, 750, 1000, 2500, 5000 };
    public readonly List<int> coinGoals = new List<int> { 5, 15, 25, 50, 75, 100, 150, 250, 500, 1000, 1500 };
    public readonly List<float> timeGoals = new List<float> { 60.0f, 300.0f, 900.0f, 1800.0f, 3600.0f, 21600.0f, 43200.0f, 86400.0f, 604800.0f, 2592000.0f };
    public int scoreObjectiveStep = 0;
    public int coinObjectiveStep = 0;
    public int timeObjectiveStep = 0;
}

[Serializable]
public class GameProgress
{
    public List<string> usedCharacters = new List<string>();
    public int characterCount;
    public int breakCount;
    public int spikeDeathCount;
    public int lossCount;
    public int winCount;
}

[Serializable]
public class StoreData
{
    public readonly List<string> Characters = new List<string>
    {
        "Owl", "Narwhal", "Rabbit", "Panda", "Penguin", "Zebra", "Rhino", "Gorilla", "Ocean", "Polandball", "Moon"
    };
    public readonly List<int> characterPrices = new List<int>
    {
        0, 50, 50, 50, 50, 50, 50, 50, 500, 500, 500
    };
    public readonly List<int> coinPrices = new List<int>
    {
        
    };
    public List<string> unlockedCharacters = new List<string>() { "Owl" };
    public int avaliableCoins;
}