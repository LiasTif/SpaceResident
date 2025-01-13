using UnityEngine;
using UnityEngine.Tilemaps;

public class PlaceTile
{
    private readonly Tilemap _tilemap;

    private readonly Tile _tile;

    private Vector3Int _start;
    private Vector3Int _end;

    private readonly TileReservationManager _reservationManager;

    private readonly ITilePlacementStrategy _strategy;

    public PlaceTile(Tilemap tilemap, Tile tile, Vector3Int start, Vector3Int end, ITilePlacementStrategy strategy, TileReservationManager reservationManager)
    {
        _tilemap = tilemap;
        _tile = tile;
        _start = start;
        _end = end;
        _reservationManager = reservationManager;
        _strategy = strategy;
    }

    public void Place()
    {
        foreach (var position in _strategy.GetPositions(_start, _end))
        {
            _reservationManager.PlaceTile(position, _tile, _tilemap);
        }
    }
}
