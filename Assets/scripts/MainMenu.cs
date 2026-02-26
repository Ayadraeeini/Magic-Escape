using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadSceneAsync("Text");
    }
    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }


}
