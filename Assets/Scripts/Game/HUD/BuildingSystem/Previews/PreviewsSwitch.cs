using UnityEngine;

public class PreviewsSwitch : MonoBehaviour
{
    [SerializeField]
    private GameObject _highlightPreview;
    [SerializeField]
    private GameObject _selectedObjectPreview;
    [SerializeField]
    private GameObject _objectPreviewInitializer;

    private void Awake() => Switch(false);

    public void Switch()
    {
        PrimaryInitObjectPreview();

        if (_highlightPreview.activeSelf && _selectedObjectPreview.activeSelf)
            Switch(false);
        else
            Switch(true);
    }

    private void Switch(bool state)
    {
        _highlightPreview.SetActive(state);
        _selectedObjectPreview.SetActive(state);
    }

    private void PrimaryInitObjectPreview()
    {
        var s = _selectedObjectPreview.GetComponent<SelectedObjectPreview>();
        if (s.Tile == null || s.Tilemap == null)
        {
            var p = _objectPreviewInitializer.GetComponent<ObjectPreviewInitializer>();
            p.PrimaryInit(s);
        }
    }
}