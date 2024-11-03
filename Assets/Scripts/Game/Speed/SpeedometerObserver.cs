using TMPro;
using UnityEngine;

public class SpeedometerObserver : MonoBehaviour, ISpeedObserver
{
    [SerializeField]
    private TMP_Text _speedometerText;

    public void UpdateObserver((int value, string measurementUnits) s) => _speedometerText.text = $"{s.value} {s.measurementUnits}/h";
}