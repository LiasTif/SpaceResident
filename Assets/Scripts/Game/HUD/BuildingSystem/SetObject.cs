//using UnityEngine;

//public class SetObject : MonoBehaviour
//{
//    [SerializeField]
//    private GameObject tileRealization;

//    private void Update()
//    {
//        if (Input.GetMouseButtonDown(0))
//            PlaceTile();
//    }

//    private void PlaceTile()
//    {
//        if (tileRealization.TryGetComponent<ObjectStateTiles>(out var st))
//        {

//        }
//        else if (tileRealization.TryGetComponent<ObjectTile>(out var t))
//        {

//        }

//        SetTile();
//    }

//    private static void SetTile()
//    {
//        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//        Vector3Int tilePosition = objectTile.Tilemap.WorldToCell(mouseWorldPos);
//        objectTile.Tilemap.SetTile(tilePosition, objectTile.Tile);
//    }
//}