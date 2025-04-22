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
    public readonly List<int> timeGoals = new List<int> { 60, 300, 900, 1800, 3600, 21600, 43200, 86400, 604800, 2592000 };
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