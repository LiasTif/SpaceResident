using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField]
    private GameObject _pauseMenu;

    private static bool _paused;

    public void Start() => ResumeGame();

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (_paused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    private void ResumeGame() => TogglePause(false);

    private void PauseGame() => TogglePause(true);

    private void TogglePause(bool pause)
    {
        _pauseMenu.SetActive(pause);
        _paused = pause;
        Time.timeScale = pause ? 0.0f : 1.0f;
    }
}
