using System.Collections.Generic;
using UnityEngine;

public class LinePlacement : ITilePlacementStrategy
{
    public IEnumerable<Vector3Int> GetPositions(Vector3Int start, Vector3Int end)
    {
        List<Vector3Int> linePositions = new();
        int deltaX = Mathf.Abs(end.x - start.x);
        int deltaY = Mathf.Abs(end.y - start.y);

        if (deltaX > deltaY) end.y = start.y;
        else end.x = start.x;

        if (start.x == end.x)
        {
            for (int y = Mathf.Min(start.y, end.y); y <= Mathf.Max(start.y, end.y); y++)
                linePositions.Add(new Vector3Int(start.x, y, start.z));
        }
        else
        {
            for (int x = Mathf.Min(start.x, end.x); x <= Mathf.Max(start.x, end.x); x++)
                linePositions.Add(new Vector3Int(x, start.y, start.z));
        }

        return linePositions;
    }
}