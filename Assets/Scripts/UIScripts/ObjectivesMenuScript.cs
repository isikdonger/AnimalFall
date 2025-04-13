using UnityEngine;

public class ObjectivesMenuScript : MonoBehaviour
{
    private GameObject StartBtn;
    private GameObject AccountMenu;
    private GameObject ObjectivesMenu;
    private GameObject CustomizePanel;
    private GameObject SettingsPanel;
    private GameObject CreditsMenu;
    private GameObject AchievmentsMenu;

    private void Start()
    {
        StartBtn = CentralUIController.Instance.StartBtn;
        AccountMenu = CentralUIController.Instance.AccountMenu;
        ObjectivesMenu = CentralUIController.Instance.ObjectivesMenu;
        CustomizePanel = CentralUIController.Instance.CustomizePanel;
        SettingsPanel = CentralUIController.Instance.SettingsPanel;
        CreditsMenu = CentralUIController.Instance.CreditsMenu;
        AchievmentsMenu = CentralUIController.Instance.AchievmentsMenu;
    }

    public void Open_CloseObjectivesMenu()
    {
        if (ObjectivesMenu.activeSelf == false)
        {
            StartBtn.SetActive(false);
            ObjectivesMenu.SetActive(true);
            CustomizePanel.SetActive(false);
            SettingsPanel.SetActive(false);
            AchievmentsMenu.SetActive(false);
            CreditsMenu.SetActive(false);
            AccountMenu.SetActive(false);
        }
        else
        {
            ObjectivesMenu.SetActive(false);
            StartBtn.SetActive(true);
        }
    }
}