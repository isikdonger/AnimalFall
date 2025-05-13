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
    public readonly List<int> objectiveRewards = new List<int> { 1, 1, 1, 3, 3, 3, 5, 5, 5, 15, 15, 15, 25, 25, 25 };
    public readonly List<int> scoreGoals = new List<int> { 25, 50, 150, 300, 600, 1200, 3600, 7200, 21600, 36000, 72000, 180000, 360000, 720000, 1440000};
    public readonly List<int> coinGoals = new List<int> { 5, 10, 30, 60, 180, 360, 720, 1440, 4320, 7200, 14400, 36000, 72000, 144000, 288000 };
    public readonly List<int> timeGoals = new List<int> { 300, 600, 1800, 3600, 10800, 21600, 43200, 86400, 259200, 432000, 864000, 2160000, 4320000, 8640000, 10800000 };
    public int scoreObjectiveStep = 0;
    public int coinObjectiveStep = 0;
    public int timeObjectiveStep = 0;
}

[Serializable]
public class GameProgress
{
    public List<string> usedCharacters = new List<string>();
    public int characterCount;
    public int lossCount;
    public int winCount;
    public int standartCount;
    public int breakCount;
    public int freezeCount;
    public int spikeCount;
}

[Serializable]
public class StoreData
{
    public readonly List<string> Characters = new List<string>
    {
        "Owl", "Dog", "Narwhal", "Rabbit", "Panda", "Penguin", "Zebra", "Rhino", "Gorilla", "Ocean", "Polandball", "Moon"
    };
    public readonly List<int> characterPrices = new List<int>
    {
        0, 250, 250, 250, 250, 250, 250, 250, 250, 5000, 5000, 5000
    };
    public readonly List<int> coinPrices = new List<int>
    {
        
    };
    public List<string> unlockedCharacters = new List<string>() { "Owl" };
    public int avaliableCoins;
}