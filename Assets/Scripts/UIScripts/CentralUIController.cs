using UnityEngine;
using UnityEngine.Localization.Settings;
using System.Collections;

public class CentralUIController : MonoBehaviour
{
    public static CentralUIController Instance { get; private set; }

    [Header("UI References")]
    public GameObject customizeBtn;
    public CanvasGroup tapToStartScreen;
    public CanvasGroup customizePanel;

    private CanvasGroup _currentMenu;
    private Coroutine _activeTransition;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // No initialization needed - everything is pre-configured
            LocalizationSettings.InitializationOperation.Completed += _ =>
            {
                tapToStartScreen.gameObject.SetActive(true);
            };
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ToggleMenu(CanvasGroup targetMenu)
    {
        if (_activeTransition != null)
        {
            Debug.Log("Stopping active transition");
            StopCoroutine(_activeTransition);
            Debug.Log("Stopped active transition");
        }

        _activeTransition = StartCoroutine(ToggleMenuRoutine(targetMenu));
    }

    private IEnumerator ToggleMenuRoutine(CanvasGroup target)
    {
        Debug.Log(_currentMenu);
        Debug.Log(target);
        // Close current menu if different
        if (_currentMenu != null && _currentMenu != target)
        {
            Debug.Log("a");
            yield return _currentMenu.FadeOut(this);
            _currentMenu = null;
        }

        // Toggle target menu
        if (_currentMenu == target)
        {
            Debug.Log("b");
            yield return target.FadeOut(this);
            _currentMenu = null;
            yield return tapToStartScreen.FadeIn(this);
        }
        else
        {
            Debug.Log("c");
            yield return tapToStartScreen.FadeOut(this);
            yield return target.FadeIn(this);
            _currentMenu = target;
        }

        _activeTransition = null;
    }
}