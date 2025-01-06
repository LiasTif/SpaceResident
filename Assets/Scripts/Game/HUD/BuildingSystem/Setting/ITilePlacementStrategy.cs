using UnityEngine.Tilemaps;
using UnityEngine;

public interface ITilePlacementStrategy
{
    void Place(Tilemap tilemap, Vector3Int start, Vector3Int end, Tile tile, TileReservationManager reservationManager);
}