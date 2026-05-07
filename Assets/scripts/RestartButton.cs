//Eyad Al Raeeini - 05/02/2026
//restart button

using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public string sceneToLoad = "SampleScene";

    public void RestartGame()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}