using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.UI;

public class CustomizePanelScript : MonoBehaviour
{
    private GameObject AccountMenu;
    private GameObject StartBtn;
    private GameObject CustomizePanel;
    private GameObject SettingsPanel;
    private GameObject AchievmentsMenu;
    private GameObject CreditsMenu;
    private GameObject ObjectivesMenu;
    private GameObject CustomizeButton;
    [SerializeField] Sprite[] characterSprites;
    public static readonly string[] characterNames = new string[] {"owl", "narwhal", "rabbit", "panda", "penguin", "zebra", "rhino", "gorilla"};

    void Awake()
    {
        AccountMenu = GameObject.Find("AccountMenu");
        StartBtn = GameObject.Find("StartBtn");
        CustomizePanel = GameObject.Find("CustomizePanel");
        SettingsPanel = GameObject.Find("SettingsPanel");
        AchievmentsMenu = GameObject.Find("AchievmentsMenu");
        CreditsMenu = GameObject.Find("CreditsMenu");
        ObjectivesMenu = GameObject.Find("ObjectivesMenu");
        CustomizeButton = GameObject.Find("CustomizeBtn");
    }

    private void Start()
    {
        CustomizePanel.SetActive(false);
        CustomizeButton.transform.GetChild(0).GetComponent<Image>().sprite = characterSprites[PlayerPrefs.GetInt("characterIndex")];
    }

    public void Open_CloseCustomizePanel()
    {
        if (CustomizePanel.activeSelf == false)
        {
            StartBtn.SetActive(false);
            CustomizePanel.SetActive(true);
            SettingsPanel.SetActive(false);
            AchievmentsMenu.SetActive(false);
            CreditsMenu.SetActive(false);
            ObjectivesMenu.SetActive(false);
            AccountMenu.SetActive(false);
        }
        else
        {
            CustomizePanel.SetActive(false);
            StartBtn.SetActive(true);
        }
    }

    public void ChangeCharacter(GameObject Button)
    {
        int index = Button.transform.GetSiblingIndex();
        CustomizeButton.transform.GetChild(0).GetComponent<Image>().sprite = characterSprites[index];
        GooglePlayServicesManager.IsAchievementUnlocked("This is getting out of hand", isUnlocked =>
        {
            if (!isUnlocked)
            {
                LocalBackupManager.AddUsedCharacter(characterNames[index]);
                if (LocalBackupManager.GetCharacterCount() == 3)
                {
                    GooglePlayServicesManager.UnlockAchievementCoroutine("This is Getting Out of Hand");
                }
            }
        });
        PlayerPrefs.SetInt("characterIndex", index);
        CustomizePanel.SetActive(false);
        StartBtn.SetActive(true);
    }
}