using UnityEngine;

public class BuildingElementsPanelSwitch : MonoBehaviour
{
    [SerializeField]
    private GameObject _panel;

    public void Switch() => _panel.SetActive(!_panel.activeSelf);
}