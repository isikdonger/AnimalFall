using UnityEngine;
using UnityEngine.Localization.Settings;
using System.Collections;

public class CentralUIController : MonoBehaviour
{
    public static CentralUIController Instance { get; private set; }

    [Header("UI References")]
    public GameObject customizeBtn;
    public CanvasGroup customizePanel;
    public CanvasGroup tapToStartScreen;
    public CanvasGroup[] subMenus;

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
            StopCoroutine(_activeTransition);

        _activeTransition = StartCoroutine(ToggleMenuRoutine(targetMenu));
    }

    private IEnumerator ToggleMenuRoutine(CanvasGroup target)
    {
        // Close current menu if different
        if (_currentMenu != null && _currentMenu != target)
        {
            yield return _currentMenu.FadeOut(this);
            _currentMenu = null;
        }

        // Toggle target menu
        if (_currentMenu == target)
        {
            yield return target.FadeOut(this);
            _currentMenu = null;
            yield return tapToStartScreen.FadeIn(this);
        }
        else
        {
            yield return tapToStartScreen.FadeOut(this);
            yield return target.FadeIn(this);
            _currentMenu = target;
        }

        _activeTransition = null;
    }

    // Specific method for character customization flow
    public void ReturnToMainFromCustomize()
    {
        if (_activeTransition != null)
            StopCoroutine(_activeTransition);

        _activeTransition = StartCoroutine(ReturnToMainRoutine());
    }

    private IEnumerator ReturnToMainRoutine()
    {
        if (_currentMenu != null)
        {
            yield return _currentMenu.FadeOut(this);
            _currentMenu = null;
        }
        yield return tapToStartScreen.FadeIn(this);
        _activeTransition = null;
    }
}