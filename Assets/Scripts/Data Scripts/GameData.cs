using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class GameProgress
{
    public int highScore;
    public List<string> usedCharacters = new List<string>();
    public int characterCount;
    public int breakCount;
    public int spikeDeathCount;
    public int lossCount;
    public int winCount;
}

[Serializable]
public class UserData
{
    public int totalGames;
    public int totalScore;
    public int totalCoins;
    public int coinSpent;
    public int achievementsCompleted;
}
