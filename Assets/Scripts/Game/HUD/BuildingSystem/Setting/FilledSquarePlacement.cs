using System.Collections.Generic;
using UnityEngine;

public class FilledSquarePlacement : ITilePlacementStrategy
{
    public IEnumerable<Vector3Int> GetPositions(Vector3Int start, Vector3Int end)
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
                positions.Add(new Vector3Int(x, y, start.z));
            }
        }

        return positions;
    }
}
