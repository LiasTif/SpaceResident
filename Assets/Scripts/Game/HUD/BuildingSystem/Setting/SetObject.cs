using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class SetObject : MonoBehaviour
{
    [SerializeField]
    private GameObject _selectedObjectPreview;
    [SerializeField]
    private HUDInputActions _hudInputActions;

    private Vector3Int _startPosition;
    private bool _isBuilding = false;

    private ITilePlacementStrategy _placementStrategy;
    private TileReservationManager _reservationManager;

    private void Awake()
    {
        _hudInputActions = new();
        _reservationManager = new TileReservationManager();
    }

    private void OnEnable() => _hudInputActions.HUD.Enable();
    private void OnDisable() => _hudInputActions.HUD.Disable();

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (Input.GetMouseButtonDown(0)) StartBuilding();
        if (Input.GetMouseButton(0) && _isBuilding) UpdatePreview();
        if (Input.GetMouseButtonUp(0)) FinishBuilding();
    }

    private void StartBuilding()
    {
        var preview = _selectedObjectPreview.GetComponent<SelectedObjectPreview>();
        _startPosition = preview.ObjectTilemap.WorldToCell(GetMouseWorldPosition());
        _isBuilding = true;
    }

    private void UpdatePreview()
    {
        // Not implemented yet
    }

    private void FinishBuilding()
    {
        _isBuilding = false;

        var preview = _selectedObjectPreview.GetComponent<SelectedObjectPreview>();
        var tilemap = preview.ObjectTilemap;
        Vector3Int endPosition = tilemap.WorldToCell(GetMouseWorldPosition());

        SetPlacementStrategy();
        _placementStrategy?.Place(tilemap, _startPosition, endPosition, preview.Tile, _reservationManager);
    }

    private void SetPlacementStrategy()
    {
        if (_hudInputActions.HUD.FirstActionButton.IsPressed() && _hudInputActions.HUD.SecondActionButton.IsPressed())
            _placementStrategy = new FilledSquarePlacement();
        else if (_hudInputActions.HUD.FirstActionButton.IsPressed())
            _placementStrategy = new HollowSquarePlacement();
        else
            _placementStrategy = new LinePlacement();
    }

    //private void PlaceTile(Vector3Int position, Tile tile, Tilemap tilemap)
    //{
    //    Vector2 spriteSize = tile.sprite.bounds.size * tile.sprite.pixelsPerUnit;
    //    int tileWidth = Mathf.CeilToInt(spriteSize.x / 32f);
    //    int tileHeight = Mathf.CeilToInt(spriteSize.y / 32f);

    //    if (AreCellsReserved(tilemap, position, tileWidth, tileHeight))
    //    {
    //        Debug.Log("Cannot place tile: Area is reserved.");
    //        return;
    //    }

    //    tilemap.SetTile(position, tile);

    //    ReserveTiles(tilemap, position, tileWidth, tileHeight);
    //}

    //private bool AreCellsReserved(Tilemap tilemap, Vector3Int anchorPosition, int width, int height)
    //{
    //    for (int x = 0; x < width; x++)
    //    {
    //        for (int y = 0; y < height; y++)
    //        {
    //            Vector3Int offsetPosition = anchorPosition + new Vector3Int(x, y, 0);

    //            if (IsCellReserved(tilemap, offsetPosition))
    //                return true;
    //        }
    //    }

    //    return false;
    //}

    //private bool IsCellReserved(Tilemap tilemap, Vector3Int position)
    //{
    //    var key = (tilemap, position);
    //    return reservedCells.ContainsKey(key);
    //}

    //private Dictionary<(Tilemap, Vector3Int), bool> reservedCells = new Dictionary<(Tilemap, Vector3Int), bool>();

    //private void ReserveTiles(Tilemap tilemap, Vector3Int anchorPosition, int width, int height)
    //{
    //    for (int x = 0; x < width; x++)
    //    {
    //        for (int y = 0; y < height; y++)
    //        {
    //            Vector3Int offsetPosition = anchorPosition + new Vector3Int(x, y, 0);

    //            var key = (tilemap, offsetPosition);
    //            if (!reservedCells.ContainsKey(key))
    //                reservedCells[key] = true;
    //        }
    //    }
    //}

    private Vector3 GetMouseWorldPosition() =>
        Camera.main.ScreenToWorldPoint(Input.mousePosition);
}