using UnityEngine.Tilemaps;
using UnityEngine;
using System.Collections.Generic;

public interface ITilePlacementStrategy
{
    void Place(Tilemap tilemap, Vector3Int start, Vector3Int end, Tile tile, TileReservationManager reservationManager);
    void PlacePreview(Tilemap tilemap, Vector3Int start, Vector3Int end, Tile tile, TileReservationManager reservationManager);

    IEnumerable<Vector3Int> GetPositions(Vector3Int start, Vector3Int end);
}