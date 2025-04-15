using System.Collections.Generic;
using UnityEngine;

public class ActiveHUD : MonoBehaviour
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
        if (hudName == "building" && !_buildingHUD.activeSelf)
            SetHUDToActive(_buildingHUD);
        else
            SetHUDToActive(_playerHUD);
    }

    private void SetHUDToActive(GameObject hud)
    {
        DisableHUDs();
        hud.SetActive(true);
    }

    private void DisableHUDs()
    {
        foreach (var hud in _huds)
            hud.SetActive(false);
    }
}
