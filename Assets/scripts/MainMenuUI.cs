using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [Header("Scene name to load (must match exactly)")]
    public string levelSceneName = "Level1";

    public void StartGame()
    {
        SceneManager.LoadScene(levelSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();

        // Editor won't close play mode, so this is just for testing
        Debug.Log("Quit pressed (won't quit in Editor).");
    }
}