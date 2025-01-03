using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class SetObject : MonoBehaviour
{
    [SerializeField]
    private GameObject _selectedObjectPreview;

    private Vector3Int _startPosition;
    private bool _isBuilding = false;

    [SerializeField]
    private HUDInputActions _hudInputActions;

    private void Awake()
    {
        _hudInputActions = new HUDInputActions();
        _hudInputActions.Enable();
    }

    private void OnEnable() => _hudInputActions.HUD.Enable();

    private void OnDisable() => _hudInputActions.HUD.Disable();

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.GetMouseButtonDown(0))
            StartBuilding();

        if (Input.GetMouseButton(0) && _isBuilding)
            UpdatePreview();

        if (Input.GetMouseButtonUp(0))
            FinishBuilding();
    }

    private void StartBuilding()
    {
        if (_selectedObjectPreview == null)
        {
            Debug.LogError("ObjectTile not bind!");
            return;
        }

        var s = _selectedObjectPreview.GetComponent<SelectedObjectPreview>();
        _startPosition = s.ObjectTilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        _isBuilding = true;
    }

    private void UpdatePreview()
    {
        var s = _selectedObjectPreview.GetComponent<SelectedObjectPreview>();
        var tilemap = s.ObjectTilemap;

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int endPosition = tilemap.WorldToCell(mouseWorldPos);
    }

    private void FinishBuilding()
    {
        _isBuilding = false;

        if (_selectedObjectPreview == null)
        {
            Debug.LogError("ObjectTile not bind!");
            return;
        }

        var s = _selectedObjectPreview.GetComponent<SelectedObjectPreview>();
        var tilemap = s.ObjectTilemap;

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int anchorPosition = tilemap.WorldToCell(mouseWorldPos);

        Vector2 spriteSize = s.Tile.sprite.bounds.size * s.Tile.sprite.pixelsPerUnit;
        int tileWidth = Mathf.CeilToInt(spriteSize.x / tilemap.cellSize.x);
        int tileHeight = Mathf.CeilToInt(spriteSize.y / tilemap.cellSize.y);

        List<Vector3Int> occupiedPositions = GetOccupiedPositions(anchorPosition, tileWidth, tileHeight);

        if (!CanPlaceArea(tilemap, occupiedPositions))
        {
            Debug.Log("Cannot place tile: Area is already occupied.");
            return;
        }

        foreach (var position in occupiedPositions)
        {
            tilemap.SetTile(position, s.Tile);
        }
    }


    private void PlaceTile(Tilemap tilemap, Vector3Int anchorPosition, Tile tile)
    {
        Vector2 spriteSize = tile.sprite.bounds.size * tile.sprite.pixelsPerUnit;
        int tileWidth = Mathf.CeilToInt(spriteSize.x / tilemap.cellSize.x);
        int tileHeight = Mathf.CeilToInt(spriteSize.y / tilemap.cellSize.y);

        List<Vector3Int> positions = GetOccupiedPositions(anchorPosition, tileWidth, tileHeight);

        if (!CanPlaceArea(tilemap, positions))
        {
            Debug.Log("Cannot place tile: area is reserved.");
            return;
        }

        foreach (var position in positions)
        {
            tilemap.SetTile(position, tile);
        }
    }

    private List<Vector3Int> GetOccupiedPositions(Vector3Int anchorPosition, int width, int height)
    {
        List<Vector3Int> positions = new List<Vector3Int>();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                positions.Add(new Vector3Int(anchorPosition.x + x, anchorPosition.y + y, anchorPosition.z));
            }
        }

        return positions;
    }

    private bool CanPlaceArea(Tilemap tilemap, List<Vector3Int> positions)
    {
        foreach (var position in positions)
        {
            if (tilemap.HasTile(position))
            {
                return false;
            }
        }
        return true;
    }

    private void BuildLine(Tilemap tilemap, Vector3Int startPosition, Vector3Int endPosition, Tile tile)
    {
        List<Vector3Int> linePositions = GetLinePositions(startPosition, endPosition);

        if (!CanPlaceArea(tilemap, linePositions))
        {
            Debug.Log("Cannot place line: area is occupied.");
            return;
        }

        foreach (var position in linePositions)
        {
            tilemap.SetTile(position, tile);
        }
    }

    private void BuildHollowSquare(Tilemap tilemap, Vector3Int startPosition, Vector3Int endPosition, Tile tile)
    {
        List<Vector3Int> squarePositions = GetHollowSquarePositions(startPosition, endPosition);

        if (!CanPlaceArea(tilemap, squarePositions))
        {
            Debug.Log("Cannot place hollow square: area is occupied.");
            return;
        }

        foreach (var position in squarePositions)
        {
            tilemap.SetTile(position, tile);
        }
    }

    private void BuildFilledSquare(Tilemap tilemap, Vector3Int startPosition, Vector3Int endPosition, Tile tile)
    {
        List<Vector3Int> squarePositions = GetFilledSquarePositions(startPosition, endPosition);

        if (!CanPlaceArea(tilemap, squarePositions))
        {
            Debug.Log("Cannot place filled square: area is occupied.");
            return;
        }

        foreach (var position in squarePositions)
        {
            tilemap.SetTile(position, tile);
        }
    }

    //private bool CanPlaceArea(Tilemap tilemap, List<Vector3Int> positions)
    //{
    //    foreach (var position in positions)
    //    {
    //        if (tilemap.HasTile(position))
    //        {
    //            return false;
    //        }
    //    }
    //    return true;
    //}

    private List<Vector3Int> GetLinePositions(Vector3Int start, Vector3Int current)
    {
        List<Vector3Int> linePositions = new List<Vector3Int>();

        int deltaX = Mathf.Abs(current.x - start.x);
        int deltaY = Mathf.Abs(current.y - start.y);

        if (deltaX > deltaY)
        {
            current.y = start.y;
        }
        else
        {
            current.x = start.x;
        }

        if (current.x == start.x)
        {
            int minY = Mathf.Min(start.y, current.y);
            int maxY = Mathf.Max(start.y, current.y);

            for (int y = minY; y <= maxY; y++)
            {
                linePositions.Add(new Vector3Int(start.x, y, start.z));
            }
        }
        else if (current.y == start.y)
        {
            int minX = Mathf.Min(start.x, current.x);
            int maxX = Mathf.Max(start.x, current.x);

            for (int x = minX; x <= maxX; x++)
            {
                linePositions.Add(new Vector3Int(x, start.y, start.z));
            }
        }

        return linePositions;
    }

    private IEnumerable<Vector3Int> GetSquarePositions(Vector3Int start, Vector3Int end, bool hollow)
    {
        List<Vector3Int> positions = new List<Vector3Int>();

        int minX = Mathf.Min(start.x, end.x);
        int maxX = Mathf.Max(start.x, end.x);
        int minY = Mathf.Min(start.y, end.y);
        int maxY = Mathf.Max(start.y, end.y);

        for (int x = minX; x <= maxX; x++)
        {
            for (int y = minY; y <= maxY; y++)
            {
                if (hollow && x > minX && x < maxX && y > minY && y < maxY)
                    continue;

                positions.Add(new Vector3Int(x, y, start.z));
            }
        }

        return positions;
    }

    private List<Vector3Int> GetHollowSquarePositions(Vector3Int start, Vector3Int end)
    {
        return new List<Vector3Int>(GetSquarePositions(start, end, true));
    }

    private List<Vector3Int> GetFilledSquarePositions(Vector3Int start, Vector3Int end)
    {
        return new List<Vector3Int>(GetSquarePositions(start, end, false));
    }
}
