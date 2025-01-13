using UnityEngine;
using UnityEngine.Tilemaps;

public class PlacePreview
{
    private readonly Tilemap _origin;
    private readonly Tilemap _preview;

    private Vector3Int _startPosition;
    private Vector3Int _endPosition;

    private readonly Tile _tile;

    private readonly ITilePlacementStrategy _placementStrategy;

    private readonly TileReservationManager _reservationManager;

    private readonly HighlightPreview _highlightPreview;

    public PlacePreview(Tilemap origin, Tilemap preview, Tile tile, Vector3Int start, Vector3Int end,
        ITilePlacementStrategy placementStrategy, TileReservationManager reservationManager, GameObject highlightPreview)
    {
        _origin = origin;
        _preview = preview;
        _startPosition = start;
        _endPosition = end;
        _tile = tile;
        _placementStrategy = placementStrategy;
        _reservationManager = reservationManager;
        _highlightPreview = highlightPreview.GetComponent<HighlightPreview>();
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
                if (_reservationManager.AreCellsAvailable(_origin, position, tileWidth, tileHeight))
                {
                    _preview.SetTile(position, _tile);
                    _highlightPreview.Tilemap.SetTile(position, _highlightPreview.Tile);
                }
            }
        }
    }
}