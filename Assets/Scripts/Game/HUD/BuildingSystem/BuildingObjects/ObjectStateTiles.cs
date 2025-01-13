using UnityEngine.Tilemaps;

public class ObjectStateTiles : ObjectTileBase
{
    public Tile Straight;
    public Tile DeadEnd;
    public Tile XShaped;
    public Tile TShaped;
    public Tile Corner;

    public Tile GetTile(string name)
    {
        return name switch
        {
            "straight" => Straight,
            "deadend" => DeadEnd,
            "tshaped" => TShaped,
            "xshaped" => XShaped,
            "corner" => Corner,
            _ => Single,
        };
    }
}