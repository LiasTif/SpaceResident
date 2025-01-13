using UnityEngine;
using UnityEngine.Tilemaps;

public class PlaceTile
{
    private readonly Tilemap _tilemap;
    private readonly ObjectTileBase _tileBase;
    private Vector3Int _start;
    private Vector3Int _end;

    private readonly TileReservationManager _reservationManager;
    private readonly ITilePlacementStrategy _strategy;
    private readonly TileDataManager _tileDataManager;

    public PlaceTile(Tilemap tilemap, ObjectTileBase tileBase, Vector3Int start, Vector3Int end,
        ITilePlacementStrategy strategy, TileReservationManager reservationManager, TileDataManager tileDataManager)
    {
        _tilemap = tilemap;
        _tileBase = tileBase;
        _start = start;
        _end = end;
        _reservationManager = reservationManager;
        _strategy = strategy;
        _tileDataManager = tileDataManager;
    }

    public void Place()
    {
        foreach (var position in _strategy.GetPositions(_start, _end))
        {
            var tileToPlace = GetTileBasedOnNeighbors(position);
            _reservationManager.PlaceTile(position, tileToPlace, _tilemap);
            _tileDataManager.AddTile(position, _tileBase);
        }
    }

    private Tile GetTileBasedOnNeighbors(Vector3Int position)
    {
        if (_tileBase is ObjectStateTiles stateTiles)
            return GetStateTile(position, stateTiles);

        return _tileBase.Single;
    }

    private Tile GetStateTile(Vector3Int position, ObjectStateTiles stateTiles)
    {
        bool hasTopNeighbor = HasNeighbor(position + Vector3Int.up);
        bool hasBottomNeighbor = HasNeighbor(position + Vector3Int.down);
        bool hasLeftNeighbor = HasNeighbor(position + Vector3Int.left);
        bool hasRightNeighbor = HasNeighbor(position + Vector3Int.right);

        return stateTiles.GetTile((hasTopNeighbor, hasBottomNeighbor, hasLeftNeighbor, hasRightNeighbor) switch
        {
            (true, true, false, false) => "straight",
            (false, false, true, true) => "straight",
            (true, false, true, false) => "corner",
            (true, false, false, true) => "corner",
            (false, true, true, false) => "corner",
            (false, true, false, true) => "corner",
            (true, true, true, true) => "xshaped",
            (true, true, true, false) => "tshaped",
            (true, true, false, true) => "tshaped",
            (true, false, true, true) => "tshaped",
            (false, true, true, true) => "tshaped",
            (true, false, false, false) => "deadend",
            (false, true, false, false) => "deadend",
            (false, false, true, false) => "deadend",
            (false, false, false, true) => "deadend",
            _ => "single",
        });
    }

    private bool HasNeighbor(Vector3Int position)
    {
        var neighborTile = _tilemap.GetTile(position) as Tile;
        if (neighborTile == null) return false;

        return neighborTile.name == _tileBase.Single.name;
    }
}
