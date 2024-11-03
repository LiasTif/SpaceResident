using UnityEngine;

public class BuildingSwitch : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerHUD;
    [SerializeField]
    private GameObject _buildingHUD;

    public static bool IsBuilding { get; private set; }

    public void Switch() => Switch(!IsBuilding);

    private void Switch(bool isBuilding)
    {
        _playerHUD.SetActive(!isBuilding);
        _buildingHUD.SetActive(isBuilding);
        IsBuilding = isBuilding;
    }
}