using UnityEngine;

public class SelectObject : MonoBehaviour
{
    [SerializeField]
    private GameObject _selectedObjectPreview;
    [SerializeField]
    private GameObject _elements;

    public void Select(string name)
    {
        var selectedObjectPreview = _selectedObjectPreview.GetComponent<SelectedObjectPreview>();

        selectedObjectPreview.Tile = name switch
        {
            "wall" => _elements.GetComponent<Wall>().Single,
            "glass" => _elements.GetComponent<Glass>().Single,
            "floor" => _elements.GetComponent<Floor>().Single,
            "door" => _elements.GetComponent<Door>().Single,
            _ => throw new System.NotImplementedException(),
        };
    }
}