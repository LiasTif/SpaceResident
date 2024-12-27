using UnityEngine;
using UnityEngine.Tilemaps;

public class ObjectPreviewInitializer : MonoBehaviour
{
    [SerializeField]
    private GameObject _elements;
    [SerializeField]
    private Tilemap _preViewTilemap;

    public void PrimaryInit(SelectedObjectPreview selectedObjectPreview)
    {
        selectedObjectPreview.Tile = _elements.GetComponent<Wall>().Single;
        selectedObjectPreview.Tilemap = _preViewTilemap;
    }
}