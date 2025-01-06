using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class FilledSquarePlacement : ITilePlacementStrategy
{
    public void Place(Tilemap tilemap, Vector3Int start, Vector3Int end, Tile tile, TileReservationManager reservationManager)
    {
        foreach (var position in GetSquarePositions(start, end))
        {
            Vector2 spriteSize = tile.sprite.bounds.size * tile.sprite.pixelsPerUnit;
            int tileWidth = Mathf.CeilToInt(spriteSize.x / 32f);
            int tileHeight = Mathf.CeilToInt(spriteSize.y / 32f);

            if (!reservationManager.AreCellsAvailable(tilemap, position, tileWidth, tileHeight))
            {
                Debug.Log("Cannot place tile: Some cells are already reserved.");
                return;
            }
        }

        foreach (var position in GetSquarePositions(start, end))
        {
            reservationManager.PlaceTile(position, tile, tilemap);
        }
    }

    private IEnumerable<Vector3Int> GetSquarePositions(Vector3Int start, Vector3Int end)
    {
        List<Vector3Int> positions = new();

        int minX = Mathf.Min(start.x, end.x);
        int maxX = Mathf.Max(start.x, end.x);
        int minY = Mathf.Min(start.y, end.y);
        int maxY = Mathf.Max(start.y, end.y);

        for (int x = minX; x <= maxX; x++)
        {
            for (int y = minY; y <= maxY; y++)
            {
                positions.Add(new Vector3Int(x, y, start.z));
            }
        }

        return positions;
    }
}
