using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class LinePlacement : ITilePlacementStrategy
{
    public void Place(Tilemap tilemap, Vector3Int start, Vector3Int end, Tile tile, TileReservationManager reservationManager)
    {
        foreach (var position in GetPositions(start, end))
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

        foreach (var position in GetPositions(start, end))
        {
            reservationManager.PlaceTile(position, tile, tilemap);
        }
    }

    public IEnumerable<Vector3Int> GetPositions(Vector3Int start, Vector3Int end)
    {
        List<Vector3Int> linePositions = new();
        int deltaX = Mathf.Abs(end.x - start.x);
        int deltaY = Mathf.Abs(end.y - start.y);

        if (deltaX > deltaY) end.y = start.y;
        else end.x = start.x;

        if (start.x == end.x)
        {
            for (int y = Mathf.Min(start.y, end.y); y <= Mathf.Max(start.y, end.y); y++)
                linePositions.Add(new Vector3Int(start.x, y, start.z));
        }
        else
        {
            for (int x = Mathf.Min(start.x, end.x); x <= Mathf.Max(start.x, end.x); x++)
                linePositions.Add(new Vector3Int(x, start.y, start.z));
        }

        return linePositions;
    }
}