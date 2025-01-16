using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsMenuScript : MonoBehaviour
{
    private GameObject CreditsMenu;
    private GameObject SettingsPanel;

    private void Awake()
    {
        CreditsMenu = GameObject.Find("CreditsMenu");
        SettingsPanel = GameObject.Find("SettingsPanel");
    }

    public void OpenCreditsMenu()
    {
        SettingsPanel.SetActive(false);
        CreditsMenu.SetActive(true);
    }
}
