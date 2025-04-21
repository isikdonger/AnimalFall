using System.Collections.Generic;
using System;

[Serializable]
public class UserData
{
    public long highScore;
    public long totalGames;
    public long totalScore;
    public long totalCoins;
    public long coinSpent;
    public int achievementsCompleted;
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
