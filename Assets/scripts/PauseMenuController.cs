//Eyad Al Raeeini - 05/02/2026
//pause menu controller
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseBox;

    void Start()
    {
        pauseBox.SetActive(false);
    }

    public void OnPauseClicked()
    {
        pauseBox.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OnResume()
    {
        pauseBox.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OnRestart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}