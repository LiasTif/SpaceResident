using UnityEngine;

public static class MouseWorldPositionProvider
{
    public static Vector3 GetMouseWorldPosition() =>
         Camera.main.ScreenToWorldPoint(Input.mousePosition);
}