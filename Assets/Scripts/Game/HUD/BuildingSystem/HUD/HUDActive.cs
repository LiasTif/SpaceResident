using System.Collections.Generic;
using UnityEngine;

public class HUDActive : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerHUD;
    [SerializeField]
    private GameObject _buildingHUD;

    private readonly List<GameObject> _huds = new();

    private void Start() => InitializeHUDS();

    private void InitializeHUDS()
    {
        _huds.Add(_playerHUD);
        _huds.Add(_buildingHUD);
    }

    public void ActivateHUD(string hudName)
    {
        if (hudName == "building" && _buildingHUD.activeSelf == false)
        {
            DisableHUDs();
            _buildingHUD.SetActive(true);
        }
        else if (hudName == "player" && !_playerHUD.activeSelf == false)
        {
            DisableHUDs();
            _playerHUD.SetActive(true);
        }
        else
        {
            DisableHUDs();
            _playerHUD.SetActive(true);
        }

        //switch (hudName)
        //{
        //    case "player": _playerHUD.SetActive(true); break;
        //    case "building": _buildingHUD.SetActive(true); break;
        //}
    }

    private void DisableHUDs()
    {
        foreach (var hud in _huds)
            hud.SetActive(false);
    }
}
