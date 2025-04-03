using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsMenuScript : MonoBehaviour
{
    private GameObject SettingsPanel;
    private GameObject CreditsMenu;

    private void Start()
    {
        SettingsPanel = CentralUIController.Instance.SettingsPanel;
        CreditsMenu = CentralUIController.Instance.CreditsMenu;
    }

    public void OpenCreditsMenu()
    {
        SettingsPanel.SetActive(false);
        CreditsMenu.SetActive(true);
    }
}
