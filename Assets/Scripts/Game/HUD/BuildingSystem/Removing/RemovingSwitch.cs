using TMPro;
using UnityEngine;

public class SwitchRemoving : MonoBehaviour
{
    [SerializeField]
    private GameObject _setObject;
    [SerializeField]
    private GameObject _removeObject;
    [SerializeField]
    private TMP_Text _removeButtonText;

    public void Switch()
    {
        _setObject.SetActive(!_setObject.activeSelf);
        _removeObject.SetActive(!_removeObject.activeSelf);
        UpdateButtonText();
    }

    private void UpdateButtonText() => _removeButtonText.text = _setObject.activeSelf ? "Remove" : "Set";
}
