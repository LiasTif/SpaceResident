using UnityEngine;

public class SelectBuildingElement : MonoBehaviour
{
    public static string SelectedElement { get; private set; }

    public void SelectElement(string name) => SelectedElement = name;
}