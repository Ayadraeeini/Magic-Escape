//Eyad Al Raeeini - 02/17/2026
//win screen ui
using UnityEngine;
using UnityEngine.SceneManagement;
public class WinScreenUI : MonoBehaviour
{
    public string levelSceneName = "Level1";
    public string mainMenuSceneName = "MainMenu";

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(levelSceneName);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }
}