using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class ObjectStateTiles : MonoBehaviour
{
    public Tile Single;
    public Tile Straight;
    public Tile DeadEnd;
    public Tile XShaped;
    public Tile TShaped;
    public Tile Corner;

    public Tile GetElement(string name)
    {
        return name switch
        {
            "straight" => Straight,
            "deadend" => DeadEnd,
            "tshaped" => Single,
            "xshaped" => XShaped,
            "corner" => Corner,
            _ => Single,
        };
    }
}