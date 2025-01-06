using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class SetObject : MonoBehaviour
{
    [SerializeField]
    private GameObject _selectedObjectPreview;
    [SerializeField]
    private GameObject _highlightPreview;
    [SerializeField]
    private HUDInputActions _hudInputActions;

    private Vector3Int _startPosition;
    private Vector3Int previousTilePosition;
    private bool _isBuilding = false;

    private ITilePlacementStrategy _placementStrategy;
    private TileReservationManager _reservationManager;

    private void Awake()
    {
        _hudInputActions = new();
        _reservationManager = new();
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
        var tilemap = preview.ObjectTilemap;

        Vector3Int mousePosition = tilemap.WorldToCell(GetMouseWorldPosition());
        Vector3Int endPosition = mousePosition;

        SetPlacementStrategy();

        IEnumerable<Vector3Int> positionsToHighlight = _placementStrategy?.GetPositions(_startPosition, endPosition);

        var highlight = _highlightPreview.GetComponent<HighlightPreview>();

        if (positionsToHighlight != null)
        {
            foreach (var position in positionsToHighlight)
            {
                if (_reservationManager.IsCellAvailable(tilemap, position))
                    HighlightTile(position, highlight.Tile);
                else
                    HighlightTile(position, highlight.DenyTile);
            }
        }
    }

    private void ClearTilemap(Tilemap tilemap) => tilemap.ClearAllTiles();

    private void HighlightTile(Vector3Int position, Tile tile)
    {
        var highlightPreview = _highlightPreview.GetComponent<HighlightPreview>();
        highlightPreview.Tilemap.SetTile(position, tile);
    }

    private void FinishBuilding()
    {
        _isBuilding = false;

        var preview = _selectedObjectPreview.GetComponent<SelectedObjectPreview>();
        var tilemap = preview.ObjectTilemap;
        Vector3Int endPosition = tilemap.WorldToCell(GetMouseWorldPosition());

        SetPlacementStrategy();
        _placementStrategy?.Place(tilemap, _startPosition, endPosition, preview.Tile, _reservationManager);

        var highlight = _highlightPreview.GetComponent<HighlightPreview>();
        ClearTilemap(highlight.Tilemap);
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