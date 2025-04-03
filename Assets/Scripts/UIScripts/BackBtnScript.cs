using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackBtnScript : MonoBehaviour
{
    private GameObject StartBtn;
    private GameObject CustomizePanel;
    private GameObject SettingsPanel;
    private GameObject CreditsMenu;

    private void Start()
    {
        StartBtn = CentralUIController.Instance.StartBtn;
        CustomizePanel = CentralUIController.Instance.CustomizePanel;
        SettingsPanel = CentralUIController.Instance.SettingsPanel;
        CreditsMenu = CentralUIController.Instance.CreditsMenu;
    }

    public void CustomizePanelBack()
    {
        CustomizePanel.SetActive(false);
        StartBtn.SetActive(true);
    }
    public void SettingsPanelBack()
    {
        SettingsPanel.SetActive(false);
        StartBtn.SetActive(true);
    }
    public void CreditsMenuBack()
    {
        CreditsMenu.SetActive(false);
        SettingsPanel.SetActive(true);
    }
}
