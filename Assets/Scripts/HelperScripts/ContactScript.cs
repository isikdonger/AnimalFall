using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class ContactScript : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown topicField;
    [SerializeField] private TMP_InputField messageField;
    [SerializeField] private GameObject errorText;
    private readonly string functionURL = "https://us-central1-formal-office-453312-t7.cloudfunctions.net/contactUs";

    public void ShowError()
    {
        string message = messageField.text;
        Debug.Log(message.Length);
        if (message.Length >= 200)
        {
            errorText.SetActive(true);
        }
    }

    public void SendMessage()
    {
        string topic = topicField.options[topicField.value].text;
        string message = messageField.text;
        string gameName = Application.productName;
        SendContactCoroutine(topic, gameName, message);
        errorText.SetActive(false);
    }

    IEnumerator SendContactCoroutine(string email, string subject, string message)
    {
        WWWForm form = new WWWForm();
        form.AddField("email", email);
        form.AddField("subject", subject);
        form.AddField("message", message);

        UnityWebRequest www = UnityWebRequest.Post(functionURL, form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Contact send failed: " + www.error);
        }
        else
        {
            Debug.Log("Contact sent successfully!");
        }
    }
}
