using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;

public class ObjectivesMenuScript : MonoBehaviour
{
    [Header("Objective Texts")]
    [SerializeField] private LocalizedString localizedScoreObjective;
    [SerializeField] private TMP_Text scoreObjectiveText;
    [SerializeField] private GameObject scoreObjectiveCompletedUI;

    [SerializeField] private LocalizedString localizedCoinObjective;
    [SerializeField] private TMP_Text coinObjectiveText;
    [SerializeField] private GameObject coinObjectiveCompletedUI;

    [SerializeField] private LocalizedString localizedTimeObjective;
    [SerializeField] private TMP_Text timeObjectiveText;
    [SerializeField] private GameObject timeObjectiveCompletedUI;

    [Header("Reward Texts")]
    [SerializeField] private LocalizedString localizedReward;
    [SerializeField] private TMP_Text scoreRewardText;
    [SerializeField] private TMP_Text coinRewardText;
    [SerializeField] private TMP_Text timeRewardText;

    [Header("AbbreviationKeys")]
    [SerializeField] private LocalizedString localizedSecond;
    [SerializeField] private LocalizedString localizedMinute;
    [SerializeField] private LocalizedString localizedHour;
    [SerializeField] private LocalizedString localizedDay;
    [SerializeField] private LocalizedString localizedThousand;
    [SerializeField] private LocalizedString localizedMillion;

    private void Start()
    {
        UpdateAllTexts();
    }

    public void UpdateAllTexts()
    {
        UpdateObjectiveText(LocalBackupManager.GetScoreObjectiveStep(), LocalBackupManager.GetScoreGoal(),
                          scoreObjectiveText, scoreObjectiveCompletedUI, localizedScoreObjective);
        UpdateObjectiveText(LocalBackupManager.GetCoinObjectiveStep(), LocalBackupManager.GetCoinGoal(),
                          coinObjectiveText, coinObjectiveCompletedUI, localizedCoinObjective);
        UpdateTimeObjectivetext(LocalBackupManager.GetTimeObjectiveStep(), LocalBackupManager.GetFormattedTime(),
                          timeObjectiveText, timeObjectiveCompletedUI, localizedTimeObjective);

        UpdateRewardTexts();
    }

    private (float, LocalizedString) LocalizeNumber(int currentObjective)
    {
        LocalizedString localizedAbbr = null;
        float dividedValue = currentObjective;

        if (currentObjective >= 1000000)
        {
            dividedValue /= 1000000;
            localizedAbbr = localizedMillion;
        }
        else if (currentObjective >= 1000)
        {
            dividedValue /= 1000;
            localizedAbbr = localizedThousand;
        }

        return (dividedValue, localizedAbbr);
    }

    private string PreciseNumberFormatter(float number)
    {
        // Convert to string with full precision
        string rawString = number.ToString("0.################", CultureInfo.InvariantCulture);

        // If there's a decimal point
        if (rawString.Contains('.'))
        {
            // Trim trailing zeros
            rawString = rawString.TrimEnd('0');

            // If we're left with just the decimal point, remove it
            if (rawString.EndsWith("."))
            {
                rawString = rawString.TrimEnd('.');
            }
        }

        return rawString;
    }

    private void UpdateObjectiveText(int currentStep, int currentObjective, TMP_Text textElement, GameObject completedUI, LocalizedString localizedString)
    {
        bool isCompleted = currentStep >= 10;
        textElement.gameObject.SetActive(!isCompleted);
        completedUI.SetActive(isCompleted);

        if (!isCompleted)
        {
            float dividedValue;
            LocalizedString localizedAbbr;
            (dividedValue, localizedAbbr) = LocalizeNumber(currentObjective);
            if (localizedAbbr != null)
            {
                string formattedNumber = PreciseNumberFormatter(dividedValue);
                localizedString.Arguments = new object[] { formattedNumber + localizedAbbr.GetLocalizedString() };
            }
            else
            {
                localizedString.Arguments = new object[] { currentObjective };
            }
            localizedString.StringChanged += (string value) => textElement.text = value;
            localizedString.RefreshString();
        }
    }

    private LocalizedString GetLocalizedTime(string identifier)
    {
        switch (identifier)
        {
            case "days":
                return localizedDay;
            case "hours":
                return localizedHour;
            default:
                return localizedMinute;
        }
    }

    private void UpdateTimeObjectivetext(int currentStep, string formattedTime, TMP_Text textElement, GameObject completedUI, LocalizedString localizedString)
    {
        bool isCompleted = currentStep >= 10;
        textElement.gameObject.SetActive(!isCompleted);
        completedUI.SetActive(isCompleted);
        if (!isCompleted)
        {
            string[] timeParts = formattedTime.Split(':');
            int timeValue = int.Parse(timeParts[0]);
            string identifier = timeParts[1];
            string localizedAbbr = GetLocalizedTime(identifier).GetLocalizedString();
            localizedString.Arguments = new object[] { $"{timeValue}{localizedAbbr}" };
            localizedString.StringChanged += (string value) => textElement.text = value;
            localizedString.RefreshString();
        }
    }

    private void UpdateRewardTexts()
    {
        UpdateRewardText(scoreRewardText, LocalBackupManager.GetScoreReward());
        UpdateRewardText(coinRewardText, LocalBackupManager.GetCoinReward());
        UpdateRewardText(timeRewardText, LocalBackupManager.GetTimeReward());
    }

    private void UpdateRewardText(TMP_Text textElement, int value)
    {
        localizedReward.Arguments = new object[] { value };
        localizedReward.StringChanged += (string val) => textElement.text = val;
        localizedReward.RefreshString();
    }
}