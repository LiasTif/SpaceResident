using UnityEngine;
using UnityEngine.EventSystems;

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
        var preview = _selectedObjectPreview.GetComponent<SelectedObjectPreview>();
        var previewTilemap = preview.Tilemap;
        Vector3Int endPosition = previewTilemap.WorldToCell(GetMouseWorldPosition());

        previewTilemap.ClearAllTiles();

        SetPlacementStrategy();
        PlacePreview placePreview = new(preview.ObjectTilemap, previewTilemap, preview.Tile, _startPosition, endPosition, _placementStrategy, _reservationManager);
        placePreview.Place();
    }

    private void DisablePreview()
    {
        var preview = _selectedObjectPreview.GetComponent<SelectedObjectPreview>();
        preview.Tilemap.ClearAllTiles();
    }

    private void FinishBuilding()
    {
        _isBuilding = false;

        var preview = _selectedObjectPreview.GetComponent<SelectedObjectPreview>();
        var tilemap = preview.ObjectTilemap;
        Vector3Int endPosition = tilemap.WorldToCell(GetMouseWorldPosition());

        SetPlacementStrategy();

        PlaceTile placeTile = new(tilemap, preview.Tile, _startPosition, endPosition, _placementStrategy, _reservationManager);
        placeTile.Place();

        DisablePreview();
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

    private Vector3 GetMouseWorldPosition() =>
        Camera.main.ScreenToWorldPoint(Input.mousePosition);
}