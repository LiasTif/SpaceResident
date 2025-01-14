using UnityEngine;
using UnityEngine.Tilemaps;

public class PlaceTile
{
    private readonly SelectedObjectPreview _selectedObjectPreview;

    private Vector3Int _start;
    private Vector3Int _end;

    private readonly TileReservationManager _reservationManager;
    private readonly ITilePlacementStrategy _strategy;

    private float _rotationAngle;

    public PlaceTile(SelectedObjectPreview selectedObjectPreview,
        Vector3Int start, Vector3Int end, ITilePlacementStrategy strategy, TileReservationManager reservationManager)
    {
        _selectedObjectPreview = selectedObjectPreview;
        _start = start;
        _end = end;
        _reservationManager = reservationManager;
        _strategy = strategy;
    }

    public void Place()
    {
        foreach (var position in _strategy.GetPositions(_start, _end))
        {
            var tileToPlace = GetTileBasedOnNeighbors(position);
            _reservationManager.PlaceTile(position, tileToPlace, _selectedObjectPreview.ObjectTilemap);
            RotateTile(position, _selectedObjectPreview.ObjectTilemap);
            RecalculateNeighborsRotation(position);
        }
    }

    private void RotateTile(Vector3Int position, Tilemap tilemap)
    {
        Matrix4x4 rotationMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 0, _rotationAngle));
        tilemap.SetTransformMatrix(position, rotationMatrix);
    }

    private Tile GetTileBasedOnNeighbors(Vector3Int position)
    {
        if (_selectedObjectPreview.ObjectTileBase is not ObjectStateTiles stateTiles)
            return _selectedObjectPreview.Tile;

        var neighbors = GetNeighborsState(position);

        string tileName = GetTileName(neighbors);

        var tile = stateTiles.GetTile(tileName);
        CalcRotationAngle(neighbors);

        return tile;
    }

    private void RecalculateNeighborsRotation(Vector3Int position)
    {
        Vector3Int[] directions = { Vector3Int.up, Vector3Int.down, Vector3Int.left, Vector3Int.right };

        foreach (var direction in directions)
        {
            Vector3Int neighborPosition = position + direction;
            var neighborTile = _selectedObjectPreview.ObjectTilemap.GetTile(neighborPosition) as Tile;

            if (neighborTile == null)
                continue;

            var neighbors = GetNeighborsState(neighborPosition);

            var tileName = GetTileName(neighbors);

            if (_selectedObjectPreview.ObjectTileBase is ObjectStateTiles stateTiles)
            {
                var tile = stateTiles.GetTile(tileName);

                _selectedObjectPreview.ObjectTilemap.SetTile(neighborPosition, tile);
                CalcRotationAngle(neighbors);
                RotateTile(neighborPosition, _selectedObjectPreview.ObjectTilemap);
            }
        }
    }

    private void CalcRotationAngle((bool hasTop, bool hasBottom, bool hasLeft, bool hasRight) neighbors)
    {
        _rotationAngle = neighbors switch
        {
            (true, true, false, false) => 90f,
            (true, false, false, true) => 270f,
            (false, true, true, false) => 90f,
            (false, true, false, true) => 180f,
            (true, true, false, true) => 180f,
            (true, false, true, true) => 270f,
            (false, true, true, true) => 90f,
            (true, false, false, false) => 270f,
            (false, true, false, false) => 90f,
            (false, false, false, true) => 180f,
            _ => 0f,
        };
    }

    private static string GetTileName((bool hasTop, bool hasBottom, bool hasLeft, bool hasRight) neighbors)
    {
        return neighbors switch
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
        };
    }

    private (bool hasTop, bool hasBottom, bool hasLeft, bool hasRight) GetNeighborsState(Vector3Int position)
    {
        return (
            HasNeighbor(position + Vector3Int.up),
            HasNeighbor(position + Vector3Int.down),
            HasNeighbor(position + Vector3Int.left),
            HasNeighbor(position + Vector3Int.right)
        );
    }

    private bool HasNeighbor(Vector3Int position)
    {
        var neighborTile = _selectedObjectPreview.ObjectTilemap.GetTile(position);
        return neighborTile != null && neighborTile.GetType() == _selectedObjectPreview.Tile.GetType();
    }
}
