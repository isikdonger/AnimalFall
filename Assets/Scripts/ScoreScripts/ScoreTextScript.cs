using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;
using System;
using TMPro;
using UnityEngine.Localization.Settings;

public class ScoreTextScript : MonoBehaviour
{
    public static int scoreValue = 0;  // Keep score static
    public TMP_Text scoreText;
    public LocalizedString localizedScoreString; // Keep this as an instance variable

    private void Start()
    {
        UpdateScoreText(); // Initialize text on start
    }

    public void AddScore()
    {
        scoreValue++;
#if !UNITY_EDITOR && UNITY_ANDROID
        GooglePlayServicesManager.ReportScore(scoreValue);
#endif
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        localizedScoreString.Arguments = new object[] { scoreValue };
        localizedScoreString.StringChanged += UpdateText;
        localizedScoreString.RefreshString();
    }

    private void UpdateText(string localizedText)
    {
        scoreText.text = localizedText;
    }
}
