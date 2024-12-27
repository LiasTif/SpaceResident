//using UnityEngine;
//using UnityEngine.Tilemaps;
//using System.Collections.Generic;

//public class BuildingElementPreView : TileUnderCoursor
//{
//    private Vector3Int previousTilePosition;
//    private Dictionary<Vector3Int, TileBase> tileBuffer = new();

//    private void Update()
//    {
//        if (Tilemap == null || Tile == null)
//            return;

//        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//        Vector3Int tilePosition = Tilemap.WorldToCell(mouseWorldPos);

//        if (tilePosition != previousTilePosition)
//        {
//            ClearPreviousTile();
//            SetTile(tilePosition);
//            previousTilePosition = tilePosition;
//        }
//    }

//    private void SetTile(Vector3Int tilePosition)
//    {
//        if (Tilemap == null || Tile == null)
//            return;

//        if (!tileBuffer.ContainsKey(tilePosition))
//            tileBuffer[tilePosition] = Tilemap.GetTile(tilePosition);

//        Tilemap.SetTile(tilePosition, Tile);
//    }

//    public void Clear()
//    {
//        ClearPreviousTile();
//        Tilemap = null;
//        Tile = null;
//    }

//    public void ClearPreviousTile()
//    {
//        if (Tilemap == null)
//            return;

//        if (tileBuffer.TryGetValue(previousTilePosition, out var originalTile))
//        {
//            Tilemap.SetTile(previousTilePosition, originalTile);
//            tileBuffer.Remove(previousTilePosition);
//        }
//    }
//}