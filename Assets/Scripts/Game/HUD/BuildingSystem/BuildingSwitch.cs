using UnityEngine;

public class BuildingSwitch : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerHUD;
    [SerializeField]
    private GameObject _buildingHUD;

    [SerializeField]
    private GameObject _selectedCellHighlight;

    public static bool IsBuilding { get; private set; }

    public void Start() => Switch(IsBuilding);

    public void Switch() => Switch(!IsBuilding);

    private void Switch(bool isBuilding)
    {
        _playerHUD.SetActive(!isBuilding);
        _buildingHUD.SetActive(isBuilding);
        SwitchSelectedCellHighlight(isBuilding);

        IsBuilding = isBuilding;
    }

    private void SwitchSelectedCellHighlight(bool isBuilding)
    {
        var s = _selectedCellHighlight.GetComponent<SelectedCellHighlight>();
        s.ClearHighlight();

        _selectedCellHighlight.SetActive(isBuilding);
    }
}