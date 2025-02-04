using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileReservationManager
{
    const float TILESIZE = 32f;

    private HashSet<(Tilemap, Vector3Int)> reservedCells = new();

    public bool AreCellsAvailable(Tilemap tilemap, Vector3Int anchorPosition, int width, int height)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3Int offsetPosition = anchorPosition + new Vector3Int(x, y, 0);

                if (reservedCells.Contains((tilemap, offsetPosition)))
                    return false;
            }
        }
        return true;
    }

    public void ReserveCells(Tilemap tilemap, Vector3Int anchorPosition, int width, int height)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3Int offsetPosition = anchorPosition + new Vector3Int(x, y, 0);
                reservedCells.Add((tilemap, offsetPosition));
            }
        }
    }

    public void PlaceTile(Vector3Int position, Tile tile, Tilemap tilemap)
    {
        Vector2 spriteSize = tile.sprite.bounds.size * tile.sprite.pixelsPerUnit;
        int tileWidth = Mathf.CeilToInt(spriteSize.x / TILESIZE);
        int tileHeight = Mathf.CeilToInt(spriteSize.y / TILESIZE);

        if (AreCellsAvailable(tilemap, position, tileWidth, tileHeight))
        {
            tilemap.SetTile(position, tile);
            ReserveCells(tilemap, position, tileWidth, tileHeight);
        }
    }

    public bool IsCellAvailable(Tilemap tilemap, Vector3Int position) => !reservedCells.Contains((tilemap, position));
}
