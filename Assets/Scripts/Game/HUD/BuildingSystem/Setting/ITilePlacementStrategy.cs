using UnityEngine.Tilemaps;
using UnityEngine;
using System.Collections.Generic;

public interface ITilePlacementStrategy
{
    IEnumerable<Vector3Int> GetPositions(Vector3Int start, Vector3Int end);
}