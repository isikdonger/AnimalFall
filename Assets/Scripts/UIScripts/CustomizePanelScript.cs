using UnityEngine;
using UnityEngine.UI;

public class CustomizePanelScript : MonoBehaviour
{
    private CanvasGroup CustomizePanel;
    private GameObject CustomizeButton;
    [SerializeField] Sprite[] characterSprites;
    public static readonly string[] characterNames = new string[] {"owl", "narwhal", "rabbit", "panda", "penguin", "zebra", "rhino", "gorilla"};

    private void Start()
    {
        CustomizePanel = CentralUIController.Instance.customizePanel;
        CustomizeButton = CentralUIController.Instance.customizeBtn;
        CustomizeButton.transform.GetChild(0).GetComponent<Image>().sprite = characterSprites[PlayerPrefs.GetInt("characterIndex")];
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
        CentralUIController.Instance.ToggleMenu(CustomizePanel);
    }
}