using UnityEngine;
using UnityEngine.Tilemaps;

public class SetObject : MonoBehaviour
{
    [SerializeField]
    private GameObject _selectedObjectPreview;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            PlaceTile();
    }

    private void PlaceTile()
    {
        if (_selectedObjectPreview == null)
        {
            Debug.LogError("ObjectTile not bind!");
            return;
        }

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var s = _selectedObjectPreview.GetComponent<SelectedObjectPreview>();
        Vector3Int tilePosition = s.Tilemap.WorldToCell(mouseWorldPos);

        Tile tileToPlace = s.Tile;

        s.Tilemap.SetTile(tilePosition, tileToPlace);
    }
}