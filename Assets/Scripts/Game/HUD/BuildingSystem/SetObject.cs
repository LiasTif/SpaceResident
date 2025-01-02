using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class SetObject : MonoBehaviour
{
    [SerializeField]
    private GameObject _selectedObjectPreview;

    private Dictionary<(Tilemap, Vector3Int), bool> reservedCells = new Dictionary<(Tilemap, Vector3Int), bool>();

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.GetMouseButtonDown(0))
            PlaceTile();
    }

    private void PlaceTile()
    {
        if (_selectedObjectPreview == null)
        {
            Debug.LogError("ObjectTile not bind!");
            return;
        }

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var s = _selectedObjectPreview.GetComponent<SelectedObjectPreview>();
        var tilemap = s.ObjectTilemap;

        Vector3Int tilePosition = tilemap.WorldToCell(mouseWorldPos);

        Tile tileToPlace = s.Tile;

        Vector2 spriteSize = tileToPlace.sprite.bounds.size * tileToPlace.sprite.pixelsPerUnit;
        int tileWidth = Mathf.CeilToInt(spriteSize.x / 32f);
        int tileHeight = Mathf.CeilToInt(spriteSize.y / 32f);

        if (AreCellsReserved(tilemap, tilePosition, tileWidth, tileHeight))
        {
            Debug.Log("Cannot place tile: Area is reserved.");
            return;
        }

        tilemap.SetTile(tilePosition, tileToPlace);

        ReserveTiles(tilemap, tilePosition, tileWidth, tileHeight);
    }

    private bool AreCellsReserved(Tilemap tilemap, Vector3Int anchorPosition, int width, int height)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3Int offsetPosition = anchorPosition + new Vector3Int(x, y, 0);

                if (IsCellReserved(tilemap, offsetPosition))
                    return true;
            }
        }

        return false;
    }


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

    private bool IsCellReserved(Tilemap tilemap, Vector3Int position)
    {
        var key = (tilemap, position);
        return reservedCells.ContainsKey(key);
    }
}
