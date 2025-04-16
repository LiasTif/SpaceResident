using UnityEngine;
using UnityEngine.Tilemaps;

public class HighlightPreview : TileUnderCoursor
{
    public Tile BuildTile;
    public Tile RemoveTile;
    public Tile ReservedTile;

    private TileReservationManager _reservationManager;

    private void Awake()
    {
        _reservationManager = new();
        SetBuildTile();
    }

    public void SetBuildTile() => Tile = BuildTile;
    public void SetRemoveTile() => Tile = RemoveTile;
    public void SetReservedTile() => Tile = ReservedTile;

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
            Tilemap.SetTile(tilePosition, base.Tile);
        else
            Tilemap.SetTile(tilePosition, base.Tile);

        previousTilePosition = tilePosition;
    }
}