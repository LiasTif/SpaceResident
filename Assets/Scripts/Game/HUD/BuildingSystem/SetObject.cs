using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class SetObject : MonoBehaviour
{
    [SerializeField]
    private GameObject _selectedObjectPreview;
    [SerializeField]
    private HUDInputActions _hudInputActions;

    private Vector3Int _startPosition;
    private bool _isBuilding = false;

    private void Awake() => _hudInputActions = new();

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

        var s = _selectedObjectPreview.GetComponent<SelectedObjectPreview>();
        var tilemap = s.ObjectTilemap;

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int endPosition = tilemap.WorldToCell(mouseWorldPos);

        bool isFilled = _hudInputActions.HUD.FirstActionButton.IsPressed() && _hudInputActions.HUD.SecondActionButton.IsPressed();
        bool isHollow = _hudInputActions.HUD.FirstActionButton.IsPressed() && !_hudInputActions.HUD.SecondActionButton.IsPressed();

        if (isHollow)
            BuildHollowSquare(tilemap, _startPosition, endPosition, s.Tile);
        else if (isFilled)
            BuildFilledSquare(tilemap, _startPosition, endPosition, s.Tile);
        else
            BuildLine(tilemap, _startPosition, endPosition, s.Tile);
    }

    private void BuildLine(Tilemap tilemap, Vector3Int start, Vector3Int end, Tile tile)
    {
        foreach (var position in GetLinePositions(start, end))
        {
            if (tilemap.HasTile(position))
            {
                Debug.Log("Tile already exists at " + position);
                return;
            }
        }

        foreach (var position in GetLinePositions(start, end))
        {
            PlaceTile(position, tile, tilemap);
        }
    }

    private void BuildHollowSquare(Tilemap tilemap, Vector3Int start, Vector3Int end, Tile tile)
    {
        foreach (var position in GetSquarePositions(start, end, true))
        {
            if (tilemap.HasTile(position))
            {
                Debug.Log("Tile already exists at " + position);
                return;
            }
        }

        foreach (var position in GetSquarePositions(start, end, true))
        {
            PlaceTile(position, tile, tilemap);
        }
    }

    private void BuildFilledSquare(Tilemap tilemap, Vector3Int start, Vector3Int end, Tile tile)
    {
        foreach (var position in GetSquarePositions(start, end, false))
        {
            if (tilemap.HasTile(position))
            {
                Debug.Log("Tile already exists at " + position);
                return;
            }
        }

        foreach (var position in GetSquarePositions(start, end, false))
        {
            PlaceTile(position, tile, tilemap);
        }
    }

    private void PlaceTile(Vector3Int position, Tile tile, Tilemap tilemap)
    {
        Vector2 spriteSize = tile.sprite.bounds.size * tile.sprite.pixelsPerUnit;
        int tileWidth = Mathf.CeilToInt(spriteSize.x / 32f);
        int tileHeight = Mathf.CeilToInt(spriteSize.y / 32f);

        if (AreCellsReserved(tilemap, position, tileWidth, tileHeight))
        {
            Debug.Log("Cannot place tile: Area is reserved.");
            return;
        }

        tilemap.SetTile(position, tile);

        ReserveTiles(tilemap, position, tileWidth, tileHeight);
    }

    private bool AreCellsReserved(Tilemap tilemap, Vector3Int anchorPosition, int width, int height)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3Int offsetPosition = anchorPosition + new Vector3Int(x, y, 0);

                if (IsCellReserved(tilemap, offsetPosition))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private bool IsCellReserved(Tilemap tilemap, Vector3Int position)
    {
        var key = (tilemap, position);
        return reservedCells.ContainsKey(key);
    }

    private Dictionary<(Tilemap, Vector3Int), bool> reservedCells = new Dictionary<(Tilemap, Vector3Int), bool>();

    private void ReserveTiles(Tilemap tilemap, Vector3Int anchorPosition, int width, int height)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3Int offsetPosition = anchorPosition + new Vector3Int(x, y, 0);

                var key = (tilemap, offsetPosition);
                if (!reservedCells.ContainsKey(key))
                {
                    reservedCells[key] = true;
                }
            }
        }
    }

    private List<Vector3Int> GetLinePositions(Vector3Int start, Vector3Int current)
    {
        List<Vector3Int> linePositions = new();

        int deltaX = Mathf.Abs(current.x - start.x);
        int deltaY = Mathf.Abs(current.y - start.y);

        if (deltaX > deltaY)
            current.y = start.y;
        else
            current.x = start.x;

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
        List<Vector3Int> positions = new();

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
}