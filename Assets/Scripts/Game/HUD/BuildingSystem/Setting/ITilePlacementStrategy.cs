using UnityEngine.Tilemaps;
using UnityEngine;
using System.Collections.Generic;

public interface ITilePlacementStrategy
{
    void Place(Tilemap tilemap, Vector3Int start, Vector3Int end, Tile tile, TileReservationManager reservationManager);
    void Place(Tilemap tilemap, Vector3Int start, Vector3Int end, Tile tile);

    IEnumerable<Vector3Int> GetPositions(Vector3Int start, Vector3Int end);
}