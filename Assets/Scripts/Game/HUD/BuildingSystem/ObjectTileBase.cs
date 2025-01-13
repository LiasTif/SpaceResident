using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class ObjectTileBase : MonoBehaviour
{
    public string TileId;
    public Tile Single;
    public Tilemap Tilemap;
}