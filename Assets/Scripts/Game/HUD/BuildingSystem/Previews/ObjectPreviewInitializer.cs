using UnityEngine;
using UnityEngine.Tilemaps;

public class ObjectPreviewInitializer : MonoBehaviour
{
    [SerializeField]
    private GameObject _elements;
    [SerializeField]
    private Tilemap _preViewTilemap;
    [SerializeField]
    private Tilemap _wallsTilemap;

    public void PrimaryInit(SelectedObjectPreview selectedObjectPreview)
    {
        selectedObjectPreview.Tile = _elements.GetComponent<Wall>().Single;
        selectedObjectPreview.ObjectTileBase = _elements.GetComponent<Wall>();
        selectedObjectPreview.Tilemap = _preViewTilemap;
        selectedObjectPreview.ObjectTilemap = _wallsTilemap;
    }
}