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

    private Vector3 GetMouseWorldPosition() =>
        Camera.main.ScreenToWorldPoint(Input.mousePosition);
}