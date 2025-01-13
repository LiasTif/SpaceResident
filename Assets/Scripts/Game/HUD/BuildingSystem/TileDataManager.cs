using System.Collections.Generic;
using UnityEngine;

public class TileDataManager
{
    private readonly Dictionary<Vector3Int, ObjectTileBase> _tileData = new();

    public void AddTile(Vector3Int position, ObjectTileBase tile) => _tileData[position] = tile;

    public ObjectTileBase GetTile(Vector3Int position)
    {
        _tileData.TryGetValue(position, out var tile);
        return tile;
    }

    public bool HasTile(Vector3Int position) => _tileData.ContainsKey(position);
}
