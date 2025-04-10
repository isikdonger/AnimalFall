using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class CentralUIController : MonoBehaviour
{
    public static CentralUIController Instance { get; private set; }

    [Header("UI References")]
    public GameObject MainMenu;
    public GameObject StartBtn;
    public Text StartText;
    public Text AccountName;
    public GameObject AccountMenu;
    public GameObject ObjectivesMenu;
    public GameObject CustomizePanel;
    public GameObject SettingsPanel;
    public GameObject CreditsMenu;
    public GameObject AchievmentsMenu;
    public GameObject CustomizeBtn;
    public GameObject SettingBtn;
    public GameObject AchievmentsBtn;

    private void Awake()
    {
        // Singleton setup (MUST BE FIRST)
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Ensures only one instance exists
            return;
        }

        // Hide UI until localization is applied
        MainMenu.SetActive(false);

        // Force preload localization (if using Unity's Localization Package)
        LocalizationSettings.InitializationOperation.Completed += (op) =>
        {
            // Now show the UI
            MainMenu.SetActive(true);
        };
    }

    private void Start()
    {
        AccountMenu.SetActive(false);
        ObjectivesMenu.SetActive(false);
        CustomizePanel.SetActive(false);
        SettingsPanel.SetActive(false);
        CreditsMenu.SetActive(false);
        AchievmentsMenu.SetActive(false);
    }
}
