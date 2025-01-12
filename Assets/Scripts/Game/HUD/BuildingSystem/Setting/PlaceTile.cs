using UnityEngine;
using UnityEngine.Tilemaps;

public class PlaceTile
{
    private Tilemap _tilemap;

    private Tile _tile;

    private Vector3Int _start;
    private Vector3Int _end;

    private TileReservationManager _reservationManager;

    private ITilePlacementStrategy _strategy;

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
        Vector2 spriteSize = _tile.sprite.bounds.size * _tile.sprite.pixelsPerUnit;
        int tileWidth = Mathf.CeilToInt(spriteSize.x / 32f);
        int tileHeight = Mathf.CeilToInt(spriteSize.y / 32f);

        foreach (var position in _strategy.GetPositions(_start, _end))
        {
            if (_reservationManager.AreCellsAvailable(_tilemap, position, tileWidth, tileHeight))
                _reservationManager.PlaceTile(position, _tile, _tilemap);
        }
    }
}
