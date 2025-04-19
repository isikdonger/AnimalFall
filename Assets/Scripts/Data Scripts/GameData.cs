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
