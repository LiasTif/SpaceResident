using UnityEngine.Tilemaps;

public static class ClearPreview
{
    public static Tilemap Tilemap { private get; set; }

    public static void Clear(HighlightPreview highlightPreviewComponent)
    {
        Clear();
        highlightPreviewComponent.Tilemap.ClearAllTiles();
    }

    public static void Clear() => Tilemap?.ClearAllTiles();
}
