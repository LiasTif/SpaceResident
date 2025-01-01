using UnityEngine;

public class BuildingSwitch : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerHUD;
    [SerializeField]
    private GameObject _buildingHUD;
    [SerializeField]
    private GameObject _previewsSwitch;

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
}