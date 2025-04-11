//using UnityEngine;
//using UnityEngine.EventSystems;
//using UnityEngine.Tilemaps;

//public class DeleteObject : MonoBehaviour
//{
//    [SerializeField]
//    private Tilemap _walls;
//    [SerializeField]
//    private Tilemap _objects;
//    [SerializeField]
//    private Tilemap _floor;

//    private bool _isDeleting = false;

//    private void Update()
//    {
//        if (EventSystem.current.IsPointerOverGameObject())
//        {
//            ClearPreview.Clear();
//            return;
//        }

//        if (_isDeleting)
//        {
//            if (Input.GetMouseButtonDown(0)) StartDeleting();
//            if (Input.GetMouseButton(0)) UpdatePreview();
//            if (Input.GetMouseButtonUp(0)) FinishDeleting();
//        }
//    }

//    public void ToggleDeleteMode()
//    {
//        _isDeleting = !_isDeleting;
//    }

//    private void StartDeleting()
//    {
//        Delete();
//    }

//    private void UpdatePreview()
//    {
//        // Implement preview update logic if needed
//    }

//    private void FinishDeleting()
//    {
//        // Implement any cleanup logic if needed
//    }

//    public void Delete()
//    {
//        Vector3Int cellPosition = GetMouseCellPosition();

//        if (_objects.HasTile(cellPosition))
//        {
//            _objects.SetTile(cellPosition, null);
//        }
//        else if (_walls.HasTile(cellPosition))
//        {
//            _walls.SetTile(cellPosition, null);
//        }
//        else if (_floor.HasTile(cellPosition))
//        {
//            _floor.SetTile(cellPosition, null);
//        }
//    }

//    private Vector3Int GetMouseCellPosition()
//    {
//        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//        return _objects.WorldToCell(mouseWorldPos);
//    }
//}
