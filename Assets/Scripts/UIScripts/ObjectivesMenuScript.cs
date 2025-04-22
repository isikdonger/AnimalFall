using TMPro;
using UnityEngine;
using UnityEngine.Localization;

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
        UpdateObjectiveText(LocalBackupManager.GetTimeObjectiveStep(), LocalBackupManager.GetTimeGoal(),
                          timeObjectiveText, timeObjectiveCompletedUI, localizedTimeObjective);

        UpdateRewardTexts();
    }

    private void UpdateObjectiveText(int currentStep, int currentObjective, TMP_Text textElement, GameObject completedUI, LocalizedString localizedString)
    {
        bool isCompleted = currentStep >= 10;
        textElement.gameObject.SetActive(!isCompleted);
        completedUI.SetActive(isCompleted);

        if (!isCompleted)
        {
            localizedString.Arguments = new object[] { currentObjective };
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