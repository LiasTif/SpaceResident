using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class RemoveObject : MonoBehaviour
{
    [SerializeField]
    private Tilemap _walls;
    [SerializeField]
    private Tilemap _objects;
    [SerializeField]
    private Tilemap _floor;
    [SerializeField]
    private GameObject _selectedObjectPreview;
    [SerializeField]
    private GameObject _highlightPreview;

    private SelectedObjectPreview _selectedObjectPreviewComponent;
    private bool _isRemoving = true;

    private void Start()
    {
        _selectedObjectPreviewComponent = _selectedObjectPreview.GetComponent<SelectedObjectPreview>();
        var highlightPreviewComponent = _highlightPreview.GetComponent<HighlightPreview>();

        _selectedObjectPreviewComponent.Tile = null;
        highlightPreviewComponent.SetRemoveTile();
    }

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (_isRemoving)
        {
            if (Input.GetMouseButtonDown(0)) StartRemoving();
            //if (Input.GetMouseButton(0)) UpdatePreview();
            //if (Input.GetMouseButtonUp(0)) FinishRemoving();
        }
    }

    public void ToggleDeleteMode()
    {
        _isRemoving = !_isRemoving;
    }

    private void StartRemoving()
    {
        Remove();
    }

    //private void UpdatePreview()
    //{
    //    // Implement preview update logic if needed
    //}

    //private void FinishRemoving()
    //{
    //    // Implement any cleanup logic if needed
    //}

    public void Remove()
    {
        Vector3Int cellPosition = GetMouseCellPosition();

        if (_objects.HasTile(cellPosition))
        {
            _objects.SetTile(cellPosition, null);
        }
        else if (_walls.HasTile(cellPosition))
        {
            _walls.SetTile(cellPosition, null);
        }
        else if (_floor.HasTile(cellPosition))
        {
            _floor.SetTile(cellPosition, null);
        }

        _selectedObjectPreviewComponent = _selectedObjectPreview.GetComponent<SelectedObjectPreview>();
        var placeTile = new PlaceTile(_selectedObjectPreviewComponent);
        placeTile.RecalculateNeighborsRotation(cellPosition);
    }

    private Vector3Int GetMouseCellPosition()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return _objects.WorldToCell(mouseWorldPos);
    }
}
