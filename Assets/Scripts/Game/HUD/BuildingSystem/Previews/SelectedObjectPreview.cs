using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class SelectedObjectPreview : TileUnderCoursor
{
    private Dictionary<TileBase, ObjectStateTiles> _tileToStateTiles = new();
    private ObjectStateTiles _selectedObjectStateTiles;

    public Tilemap ObjectTilemap;
    public ObjectTileBase ObjectTileBase;

    public SelectedObjectPreview()
    {
        InitializeTileMappings();
    }

    private void InitializeTileMappings()
    {
        _tileToStateTiles.Add(myGlassTile, myGlassStateTiles);
        _tileToStateTiles.Add(myWallTile, myWallStateTiles);
    }

    public bool TryGetStateTiles(TileBase tileBase, out ObjectStateTiles stateTiles)
    {
        stateTiles = GetStateTilesFor(tileBase);
        return stateTiles != null;
    }

    public ObjectStateTiles GetStateTilesFor(TileBase tileBase)
    {
        if (tileBase == null) return null;

        if (_tileToStateTiles.TryGetValue(tileBase, out var stateTiles))
            return stateTiles;

        return null;
    }

    private void SetSelectedTile(TileBase tileBase)
    {
        if (_tileToStateTiles.TryGetValue(tileBase, out var stateTiles))
            _selectedObjectStateTiles = stateTiles;
    }
}