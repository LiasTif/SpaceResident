using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatSend : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _chatTextTemplate;

    [SerializeField]
    private GameObject _chatContent;

    [SerializeField]
    private Button _sendButton;

    public void Send()
    {
        TMP_Text msg = Instantiate(_chatTextTemplate);
        DateTime time = DateTime.Now;
        msg.text = time.ToString("hh:mmtt") + ": msg";
        msg.transform.SetParent(_chatContent.transform, false);
    }
}