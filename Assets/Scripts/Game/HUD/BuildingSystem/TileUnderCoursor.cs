using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class TileUnderCoursor : MonoBehaviour
{
    public Tilemap? Tilemap;
    public Tile? Tile;

    private Vector3Int previousTilePosition;

    private void Update()
    {
        if (Tilemap == null || Tile == null)
            return;

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int tilePosition = Tilemap.WorldToCell(mouseWorldPos);

        if (tilePosition != previousTilePosition)
        {
            ClearTile();
            Tilemap.SetTile(tilePosition, Tile);
            previousTilePosition = tilePosition;
        }
    }

    public void ClearTile()
    {
        if (Tilemap == null)
            return;

        Tilemap.SetTile(previousTilePosition, null);
    }
}