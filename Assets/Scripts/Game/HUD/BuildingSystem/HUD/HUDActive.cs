using System.Collections.Generic;
using UnityEngine;

public class HUDActive : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerHUD;
    [SerializeField]
    private GameObject _buildingHUD;

    private List<GameObject> _huds = new();

    private void Start() => InitializeHUDS();

    private void InitializeHUDS()
    {
        _huds.Add(_playerHUD);
        _huds.Add(_buildingHUD);
    }

    public void ActivateHUD(string hudName)
    {
        DisableHUDs();

        switch (hudName)
        {
            case "player": _playerHUD.SetActive(true); break;
            case "building": _buildingHUD.SetActive(true); break;
        }
    }

    private void DisableHUDs()
    {
        foreach (var hud in _huds)
            hud.SetActive(false);
    }
}
