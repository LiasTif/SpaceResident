using UnityEngine;
using UnityEngine.Tilemaps;

public class SelectBuildingElement : MonoBehaviour
{
    [Header("Tilemaps")]
    [SerializeField]
    private Tilemap _bigTilemap;
    [SerializeField]
    private Tilemap _smallTilemap;
    [Header("Berhaviours")]
    [SerializeField]
    private GameObject _buildingElementPreView;
    [Header("Elements")]
    [SerializeField]
    private GameObject _elements;
    [Header("SelectedCellHighlight")]
    [SerializeField]
    private GameObject _selectedCellHighlight;

    private Tile _selectedTile;
    private BuildingElementPreView _currentPreView;

    private void Start()
    {
        _currentPreView = _buildingElementPreView.GetComponent<BuildingElementPreView>();
        SwitchPreView(false);
    }

    public void SelectTile(string name)
    {
        ClearPreView();
        var h = _selectedCellHighlight.GetComponent<SelectedCellHighlight>();

        switch (name)
        {
            case "wall":
                var wall = _elements.GetComponent<Wall>();
                _selectedTile = wall.Single;
                SetPreView(_smallTilemap);
                h.SwitchTileMap(false);
                break;
            case "floor":
                var floor = _elements.GetComponent<Floor>();
                _selectedTile = floor.Tile;
                SetPreView(_bigTilemap);
                h.SwitchTileMap(true);
                break;
            case "door":
                var door = _elements.GetComponent<Door>();
                _selectedTile = door.Tile;
                SetPreView(_bigTilemap);
                h.SwitchTileMap(true);
                break;
            case "glass":
                var glass = _elements.GetComponent<Glass>();
                _selectedTile = glass.Single;
                SetPreView(_smallTilemap);
                h.SwitchTileMap(false);
                break;
        }
    }

    private void ClearPreView()
    {
        if (_currentPreView != null)
            _currentPreView.Clear();
    }

    private void SetPreView(Tilemap tilemap)
    {
        if (_currentPreView == null) return;

        _currentPreView.Tilemap = tilemap;
        _currentPreView.Tile = _selectedTile;

        SwitchPreView(true);
    }

    private void SwitchPreView(bool isActive) => _buildingElementPreView.SetActive(isActive);
}