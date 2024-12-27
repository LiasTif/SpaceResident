//using TMPro;
//using UnityEngine;

//public class BuildingElementsSwitch : MonoBehaviour
//{
//    [SerializeField]
//    private GameObject _buildingElementsPanel;
//    [SerializeField]
//    private TMP_Text _buildingElementsBtnText;
//    [SerializeField]
//    private GameObject _buildingSwitch;

//    public void Switch() => Switch(!_buildingElementsPanel.activeSelf);

//    public void Switch(bool state)
//    {
//        _buildingElementsPanel.SetActive(state);
//        ChangeBtnText(state);
//        BuildingSwitchSelectedCellHighlight(!state);
//    }

//    private void BuildingSwitchSelectedCellHighlight(bool state)
//    {
//        var buildingSwitch = _buildingSwitch.GetComponent<BuildingSwitch>();
//        buildingSwitch.SwitchTileUnderCoursor(state);
//    }

//    public void ChangeBtnText(bool isMenuActive) => _buildingElementsBtnText.text = isMenuActive ? "<" : ">";
//}