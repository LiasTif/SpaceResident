using TMPro;
using UnityEngine;

public class BuildPreviewSize : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _text;

    private void Update()
    {
        Vector2 offset = MouseWorldPositionProvider.GetMouseWorldPosition();
        _text.transform.position = new Vector3(offset.x, offset.y + 0.2f, -1f);
    }

    public void UpdateSize(int x, int y) => _text.text = $"{x}w // {y}l";
    public void UpdateSize(int length, bool vertical) => _text.text = vertical ? $"{length}l" : $"{length}w";
}