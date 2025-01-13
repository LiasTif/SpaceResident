using UnityEngine.Tilemaps;

public class SelectedObjectPreview : TileUnderCoursor
{
    public Tilemap ObjectTilemap;
    public ObjectTileBase SelectedTileBase { get; set; }
}