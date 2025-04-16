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
    private readonly BuildPreviewSize _buildPreviewSize;

    public PlacePreview(Tilemap origin, Tilemap preview, Tile tile, Vector3Int start, Vector3Int end,
        ITilePlacementStrategy placementStrategy, TileReservationManager reservationManager, GameObject highlightPreview,
        BuildPreviewSize buildPreviewSize)
    {
        _origin = origin;
        _preview = preview;
        _startPosition = start;
        _endPosition = end;
        _tile = tile;
        _placementStrategy = placementStrategy;
        _reservationManager = reservationManager;
        _highlightPreview = highlightPreview.GetComponent<HighlightPreview>();
        _buildPreviewSize = buildPreviewSize;
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
                    UpdateBuildPreviewSize();
                }
            }
        }
    }

    private void UpdateBuildPreviewSize()
    {
        int sizeX = Mathf.Abs(_endPosition.x - _startPosition.x) + 1;
        int sizeY = Mathf.Abs(_endPosition.y - _startPosition.y) + 1;

        if (_placementStrategy is LinePlacement)
            if (sizeX > sizeY)
                _buildPreviewSize.UpdateSize(sizeX, false);
            else
                _buildPreviewSize.UpdateSize(sizeY, true);
        else
            _buildPreviewSize.UpdateSize(sizeX, sizeY);
    }
}