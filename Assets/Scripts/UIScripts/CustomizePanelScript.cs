using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.UI;

public class CustomizePanelScript : MonoBehaviour
{
    private GameObject StartBtn;
    private GameObject AccountMenu;
    private GameObject ObjectivesMenu;
    private GameObject CustomizePanel;
    private GameObject SettingsPanel;
    private GameObject CreditsMenu;
    private GameObject AchievmentsMenu;
    private GameObject CustomizeButton;
    [SerializeField] Sprite[] characterSprites;
    public static readonly string[] characterNames = new string[] {"owl", "narwhal", "rabbit", "panda", "penguin", "zebra", "rhino", "gorilla"};

    private void Start()
    {
        StartBtn = CentralUIController.Instance.StartBtn;
        AccountMenu = CentralUIController.Instance.AccountMenu;
        ObjectivesMenu = CentralUIController.Instance.ObjectivesMenu;
        CustomizePanel = CentralUIController.Instance.CustomizePanel;
        SettingsPanel = CentralUIController.Instance.SettingsPanel;
        CreditsMenu = CentralUIController.Instance.CreditsMenu;
        AchievmentsMenu = CentralUIController.Instance.AchievmentsMenu;
        CustomizeButton = CentralUIController.Instance.CustomizeBtn;
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
        LocalBackupManager.AddUsedCharacter(characterNames[index]);
        if (LocalBackupManager.GetCharacterCount() == 3)
        {
#if UNITY_ANDROID
            GooglePlayServicesManager.UnlockAchievementCoroutine("This is Getting Out of Hand");
#elif UNITY_IOS
            GameCenterManager.UnlockAchievementCoroutine("This is Getting Out of Hand");
#endif
        }
        PlayerPrefs.SetInt("characterIndex", index);
        CustomizePanel.SetActive(false);
        StartBtn.SetActive(true);
    }
}