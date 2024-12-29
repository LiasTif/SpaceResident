using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class TileUnderCoursor : MonoBehaviour
{
    public Tilemap Tilemap;
    public Tile Tile;

    private Vector3Int previousTilePosition;

    private void Update()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int tilePosition = Tilemap.WorldToCell(mouseWorldPos);

        if (tilePosition != previousTilePosition)
        {
            ClearPreviousTile();
            SetTileToNewPosition(tilePosition);
        }
    }

    private void SetTileToNewPosition(Vector3Int tilePosition)
    {
        Tilemap.SetTile(tilePosition, Tile);
        previousTilePosition = tilePosition;
    }

    public void ClearPreviousTile() => Tilemap.SetTile(previousTilePosition, null);
}