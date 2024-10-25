using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigation : MonoBehaviour
{
    public void NavigateToScene(string sceneName) => SceneManager.LoadScene(sceneName);
}
