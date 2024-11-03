using UnityEngine;
using UnityEngine.Tilemaps;

public class SelectedCellHighlight : MonoBehaviour
{
    [SerializeField]
    private Tilemap _tilemap;
    [SerializeField]
    private Tile _highlightTile;

    private Vector3Int previousCellPosition;

    private void Update()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPosition = _tilemap.WorldToCell(mouseWorldPos);

        if (cellPosition != previousCellPosition)
        {
            ClearHighlight();
            _tilemap.SetTile(cellPosition, _highlightTile);
            previousCellPosition = cellPosition;
        }
    }

    public void ClearHighlight() => _tilemap.SetTile(previousCellPosition, null);
}
