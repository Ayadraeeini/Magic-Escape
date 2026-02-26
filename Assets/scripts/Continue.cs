using UnityEngine;
using UnityEngine.SceneManagement;

public class Continue : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Continues()
    {
        SceneManager.LoadSceneAsync("jesse");
    }

}
