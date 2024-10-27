using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField]
    private GameObject _pauseMenu;

    private static bool _paused;
    private static HUDInputActions _hudInputActions;

    private void OnEnable() => _hudInputActions.HUD.Pause.Enable();

    private void OnDisable() => _hudInputActions.HUD.Pause.Disable();

    private void Awake() => InitHUDInputActions();

    private void InitHUDInputActions()
    {
        _hudInputActions = new();
        _hudInputActions.HUD.Pause.performed += context => TogglePause(!_paused);
    }

    public void ResumeGame() => TogglePause(false);

    private void TogglePause(bool pause)
    {
        _pauseMenu.SetActive(pause);
        _paused = pause;
        Time.timeScale = pause ? 0.0f : 1.0f;
    }
}
