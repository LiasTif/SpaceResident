using UnityEngine;
using UnityEngine.Tilemaps;

public class HighlightPreview : TileUnderCoursor
{
    private TileReservationManager _reservationManager;

    private void Awake() => _reservationManager = new();

    private void Update()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int tilePosition = Tilemap.WorldToCell(mouseWorldPos);

        if (tilePosition != previousTilePosition)
        {
            base.ClearPreviousTile();
            SetTileToNewPosition(tilePosition);
        }
    }

    private void SetTileToNewPosition(Vector3Int tilePosition)
    {
        if (_reservationManager.IsCellAvailable(Tilemap, tilePosition))
            Tilemap.SetTile(tilePosition, Tile);
        else
            Tilemap.SetTile(tilePosition, Tile);

        previousTilePosition = tilePosition;
    }
}