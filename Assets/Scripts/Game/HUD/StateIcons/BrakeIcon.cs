using TMPro;
using UnityEngine;

public class BrakeIcon : MonoBehaviour, IStateIcon
{
    [SerializeField]
    private TMP_Text _brakeIconText;

    public void ChangeStatus(bool status) => _brakeIconText.color = status ? Color.white : new Color(1f, 1f, 1f, 0.2f);
}