using TMPro;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _buildingElementsMenu;
    [SerializeField]
    private TMP_Text _buildingElementsBtnText;
    [SerializeField]
    private GameObject _buildingSwitch;

    public void Switch()
    {
        bool newState = !_buildingElementsMenu.activeSelf;
        _buildingElementsMenu.SetActive(newState);
        ChangeBtnText(newState);
        BuildingSwitchSetState(newState);
    }

    private void BuildingSwitchSetState(bool newState)
    {
        var buildingSwitch = _buildingSwitch.GetComponent<BuildingSwitch>();
        buildingSwitch.Switch(newState);
    }

    public void ChangeBtnText(bool isMenuActive) => _buildingElementsBtnText.text = isMenuActive? "<" : ">";
}
