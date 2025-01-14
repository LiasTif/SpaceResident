using UnityEngine;

public class PreviewsSwitch : MonoBehaviour
{
    [SerializeField]
    private GameObject _highlightPreview;
    [SerializeField]
    private GameObject _selectedObjectPreview;
    [SerializeField]
    private GameObject _objectPreviewInitializer;

    private void Start()
    {
        PrimaryInitObjectPreview();
        Switch(false);
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

    public void Switch()
    {
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

    public void ClearPreview()
    {
        var h = _highlightPreview.GetComponent<HighlightPreview>();
        var s = _selectedObjectPreview.GetComponent<SelectedObjectPreview>();

        h.ClearPreviousTile();
        s.ClearPreviousTile();
    }
}