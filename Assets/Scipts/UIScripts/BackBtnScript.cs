using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackBtnScript : MonoBehaviour
{
    private GameObject CustomizePanel;
    private GameObject StartBtn;
    private GameObject SettingsPanel;
    private GameObject CreditsMenu;

    private void Awake()
    {
        StartBtn = GameObject.Find("StartBtn");
        CustomizePanel = GameObject.Find("CustomizePanel");
        SettingsPanel = GameObject.Find("SettingsPanel");
        CreditsMenu = GameObject.Find("CreditsMenu");
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
