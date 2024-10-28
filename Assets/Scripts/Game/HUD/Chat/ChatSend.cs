using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatSend : MonoBehaviour
{
    [Header("Receiving")]
    [SerializeField]
    private GameObject _chatContent;
    [SerializeField]
    private TMP_Text _chatTextTemplate;

    [Header("Sending")]
    [SerializeField]
    private TMP_InputField _chatInputField;
    [SerializeField]
    private Button _sendButton;

    public void Send()
    {
        TMP_Text msg = Instantiate(_chatTextTemplate);
        DateTime time = DateTime.Now;
        msg.text = time.ToString("hh:mmtt") + $": {_chatInputField.text}";
        msg.transform.SetParent(_chatContent.transform, false);

        Debug.Log($"Received message: {msg.text}");
    }
}