using UnityEngine;

public class SceneTransition
{
    public static void Transit(int sceneIndex)
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == sceneIndex)
        {
            Debug.LogWarning("Attempted to load the current scene again. No action taken.");
            return;
        }
        else
        {
            IndexTransit(sceneIndex);
        }
    }

    public static void Transit(string sceneName)
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == sceneName)
        {
            Debug.LogWarning("Attempted to load the current scene again. No action taken.");
            return;
        }
        else
        {
            NameTransit(sceneName);
        }
    }

    public static void ForceTransit(int sceneIndex)
    {
        IndexTransit(sceneIndex);
    }

    public static void ForceTransit(string sceneName)
    {
        NameTransit(sceneName);
    }

    private static void IndexTransit(int sceneIndex)
    {
        try
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to load scene with index: " + sceneIndex + ". Error: " + e.Message);
            return;
        }
    }

    private static void NameTransit(string sceneName)
    {
        try
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to load scene: " + sceneName + ". Error: " + e.Message);
        }
    }
}