using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class SetObject : MonoBehaviour
{
    [SerializeField]
    private GameObject _selectedObjectPreview;
    [SerializeField]
    private GameObject _highlightPreview;
    [SerializeField]
    private GameObject _buildPreviewSize;

    private Vector3Int _startPosition;

    private ITilePlacementStrategy _placementStrategy;
    private TileReservationManager _reservationManager;
    private HUDInputActions _hudInputActions;

    private SelectedObjectPreview _objectPreviewComponent;
    private HighlightPreview _highlightPreviewComponent;
    private BuildPreviewSize _buildPreviewSizeInstance;

    private void OnEnable() => _hudInputActions.HUD.Enable();
    private void OnDisable() => _hudInputActions.HUD.Disable();

    private void Awake()
    {
        _hudInputActions = new();
        _reservationManager = new();

        InitializePreviews();
    }

    private void InitializePreviews()
    {
        _objectPreviewComponent = _selectedObjectPreview.GetComponent<SelectedObjectPreview>();
        _highlightPreviewComponent = _highlightPreview.GetComponent<HighlightPreview>();
        _buildPreviewSizeInstance =  _buildPreviewSize.GetComponent<BuildPreviewSize>();
    }

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            ClearPreviewTilemaps();
            return;
        }

        if (Input.GetMouseButtonDown(0)) StartBuilding();
        if (Input.GetMouseButton(0)) UpdatePreview();
        if (Input.GetMouseButtonUp(0)) FinishBuilding();
    }

    private void StartBuilding()
    {
        _startPosition = GetObjectPreviewWorldPosition();
        _highlightPreview.SetActive(false);
    }

    private void UpdatePreview()
    {
        Vector3Int endPosition = GetPreviewWorldPosition();

        ClearPreviewTilemaps();
        SetPlacementStrategy();

        PlacePreview placePreview = new(_objectPreviewComponent.ObjectTilemap, _objectPreviewComponent.Tilemap, _objectPreviewComponent.Tile,
            _startPosition, endPosition, _placementStrategy, _reservationManager, _highlightPreview, _buildPreviewSizeInstance);
        placePreview.Place();
    }

    private void ClearPreviewTilemaps()
    {
        _objectPreviewComponent.Tilemap.ClearAllTiles();
        _highlightPreviewComponent.Tilemap.ClearAllTiles();
    }

    private void FinishBuilding()
    {
        Vector3Int endPosition = GetObjectPreviewWorldPosition();

        PlaceTile placeTile = new(_objectPreviewComponent, _startPosition, endPosition, _placementStrategy, _reservationManager,
            _buildPreviewSizeInstance);
        placeTile.Place();
        
        DisablePreview();
    }

    private void DisablePreview()
    {
        ClearPreviewTilemaps();
        _highlightPreview.SetActive(true);
    }

    private void SetPlacementStrategy()
    {
        if (SetDefaultStrategy()) return;

        if (_hudInputActions.HUD.FirstActionButton.IsPressed() && _hudInputActions.HUD.SecondActionButton.IsPressed())
            _placementStrategy = new FilledSquarePlacement();
        else if (_hudInputActions.HUD.FirstActionButton.IsPressed())
            _placementStrategy = new HollowSquarePlacement();
        else
            _placementStrategy = new LinePlacement();
    }

    private bool SetDefaultStrategy()
    {
        var tile = _objectPreviewComponent.Tile;
        Vector2 spriteSize = tile.sprite.bounds.size * tile.sprite.pixelsPerUnit;

        if (spriteSize.x != 32 || spriteSize.y != 32)
        {
            _placementStrategy = new LinePlacement();
            return true;
        }

        return false;
    }

    private Vector3Int GetObjectPreviewWorldPosition() =>
        _objectPreviewComponent.ObjectTilemap.WorldToCell(MouseWorldPositionProvider.GetMouseWorldPosition());

    private Vector3Int GetPreviewWorldPosition() =>
        _objectPreviewComponent.Tilemap.WorldToCell(MouseWorldPositionProvider.GetMouseWorldPosition());
}