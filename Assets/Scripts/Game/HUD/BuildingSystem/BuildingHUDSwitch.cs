using UnityEngine;

public class BuildingSwitch : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerHUD;
    [SerializeField]
    private GameObject _buildingHUD;
    [SerializeField]
    private GameObject _previewsSwitch;
    //[SerializeField]
    //private GameObject _selectedCellHighlight;
    //[SerializeField]
    //private GameObject _buildingElementPreView;
    //[SerializeField]
    //private GameObject _buildingElementsSwitch;

    public void Switch()
    {
        SwitchHUDsState();
        SwitchPreview();
    }
    // використати патерн підписник для виключання інших худів та худу гравця,
    // щоб уникнути накладання декількох худів

    private void SwitchPreview()
    {
        var p = _previewsSwitch.GetComponent<PreviewsSwitch>();
        p.ClearPreview();
        p.Switch();
    }

    private void SwitchHUDsState()
    {
        var state = _playerHUD.activeSelf;

        _buildingHUD.SetActive(state);
        _playerHUD.SetActive(!state);
    }

    //public static bool IsBuilding { get; private set; }

    //public void Start() => Switch(false);

    //public void Switch() => Switch(!IsBuilding);

    //private void Switch(bool isBuilding)
    //{
    //    _playerHUD.SetActive(!isBuilding);
    //    _buildingHUD.SetActive(isBuilding);

    //    if (IsBuilding)
    //        TurnOffBuildingElementsPanel();

    //    SwitchTileUnderCoursor(isBuilding);

    //    IsBuilding = isBuilding;
    //}

    //private void TurnOffBuildingElementsPanel()
    //{
    //    var b = _buildingElementsSwitch.GetComponent<BuildingElementsSwitch>();
    //    b.Switch(false);
    //}

    //public void SwitchTileUnderCoursor(bool isBuilding)
    //{
    //    SwitchComponentState(_selectedCellHighlight, isBuilding);
    //    SwitchComponentState(_buildingElementPreView, isBuilding);
    //}

    //private void SwitchComponentState(GameObject o, bool isBuilding)
    //{
    //    var r = o.GetComponent<TileUnderCoursor>();
    //    r.ClearTile();

    //    o.SetActive(isBuilding);
    //}
}