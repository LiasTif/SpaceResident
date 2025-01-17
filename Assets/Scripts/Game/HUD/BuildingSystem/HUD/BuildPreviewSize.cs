using TMPro;
using UnityEngine;

public class BuildPreviewSize : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _text;

    private TMP_Text _textComp;

    //private void Start() => GetTextComponent();

    private void Update()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 offset = GetMouseWorldPosition(-20);

        _text.transform.position = offset;
    }

    private Vector3 GetMouseWorldPosition(float offsetY)
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.y += offsetY;
        return Camera.main.ScreenToWorldPoint(mouseScreenPosition);
    }

    public void UpdateSize(int x, int y) => _textComp.text = $"{x}w // {y}l";
    public void UpdateSize(int length, bool vertical) => _textComp.text = vertical ? $"{length}l" : $"{length}w";

    //private TMP_Text GetTextComponent() => _text.GetComponent<TMP_Text>();
}