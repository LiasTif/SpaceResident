using UnityEngine;

public class SwitchRemoving : MonoBehaviour
{
    [SerializeField]
    private GameObject _setObject;
    [SerializeField]
    private GameObject _removeObject;

    public void Switch()
    {
        _setObject.SetActive(!_setObject.activeSelf);
        _removeObject.SetActive(!_removeObject.activeSelf);
    }
}
