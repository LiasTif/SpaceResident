using UnityEngine;
using UnityEngine.Tilemaps;

public class SelectObject : MonoBehaviour
{
    [SerializeField]
    private GameObject _selectedObjectPreview;
    [SerializeField]
    private GameObject _elements;
    [SerializeField]
    private Tilemap _objectsTilemap;
    [SerializeField]
    private Tilemap _wallsTilemap;
    [SerializeField]
    private Tilemap _floorTilemap;

    public void Select(string name)
    {
        var selectedObjectPreview = _selectedObjectPreview.GetComponent<SelectedObjectPreview>();
        SetTile(name, selectedObjectPreview);
        SetObjectTilemap(name, selectedObjectPreview);
        SetObjectTileBase(name, selectedObjectPreview);
    }

    private void SetObjectTileBase(string name, SelectedObjectPreview selectedObjectPreview)
    {
        selectedObjectPreview.ObjectTileBase = name switch
        {
            "wall" => _elements.GetComponent<Wall>(),
            "glass" => _elements.GetComponent<Glass>(),
            "floor" => _elements.GetComponent<Floor>(),
            "door" => _elements.GetComponent<Door>(),
            _ => throw new System.NotImplementedException(),
        };
    }

    private void SetObjectTilemap(string name, SelectedObjectPreview selectedObjectPreview)
    {
        selectedObjectPreview.ObjectTilemap = name switch
        {
            "wall" => _wallsTilemap,
            "glass" => _wallsTilemap,
            "floor" => _floorTilemap,
            "door" => _wallsTilemap,
            _ => throw new System.NotImplementedException(),
        };
    }

    private void SetTile(string name, SelectedObjectPreview selectedObjectPreview)
    {
        selectedObjectPreview.Tile = name switch
        {
            "wall" => _elements.GetComponent<Wall>().Single,
            "glass" => _elements.GetComponent<Glass>().Single,
            "floor" => _elements.GetComponent<Floor>().Single,
            "door" => _elements.GetComponent<Door>().Single,
            _ => null,
        };
    }
}