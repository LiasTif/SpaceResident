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

        selectedObjectPreview.Tile = name switch
        {
            "wall" => _elements.GetComponent<Wall>().Single,
            "glass" => _elements.GetComponent<Glass>().Single,
            "floor" => _elements.GetComponent<Floor>().Single,
            "door" => _elements.GetComponent<Door>().Single,
            _ => throw new System.NotImplementedException(),
        };

        selectedObjectPreview.ObjectTilemap = name switch
        {
            "wall" => _wallsTilemap,
            "glass" => _wallsTilemap,
            "floor" => _floorTilemap,
            "door" => _objectsTilemap,
            _ => throw new System.NotImplementedException(),
        };
    }
}