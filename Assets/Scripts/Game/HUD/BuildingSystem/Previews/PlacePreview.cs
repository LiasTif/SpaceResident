using UnityEngine;
using UnityEngine.Tilemaps;

public class PlacePreview
{
    private Tilemap _origin;
    private Tilemap _preview;

    private Vector3Int _startPosition;
    private Vector3Int _endPosition;

    private Tile _tile;

    private ITilePlacementStrategy _placementStrategy;

    private TileReservationManager _reservationManager;

    public PlacePreview(Tilemap origin, Tilemap preview, Vector3Int start, Vector3Int end, Tile tile, ITilePlacementStrategy placementStrategy, TileReservationManager reservationManager)
    {
        _origin = origin;
        _preview = preview;
        _startPosition = start;
        _endPosition = end;
        _tile = tile;
        _placementStrategy = placementStrategy;

        _reservationManager = reservationManager;
    }

    public void Place()
    {
        Vector2 spriteSize = _tile.sprite.bounds.size * _tile.sprite.pixelsPerUnit;
        int tileWidth = Mathf.CeilToInt(spriteSize.x / 32f);
        int tileHeight = Mathf.CeilToInt(spriteSize.y / 32f);

        foreach (var position in _placementStrategy.GetPositions(_startPosition, _endPosition))
        {
            if ((position.x - _startPosition.x) % (tileWidth) == 0 && (position.y - _startPosition.y) % (tileHeight) == 0)
            {
                if (_reservationManager.AreCellsAvailable(_preview, position, tileWidth, tileHeight))
                {
                    _preview.SetTile(position, _tile);
                    if (!_reservationManager.AreCellsAvailable(_origin, position, tileWidth, tileHeight))
                        _preview.SetTile(position, null);
                }
            }
        }
    }
}