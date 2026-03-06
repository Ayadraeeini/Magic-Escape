//Eyad Al Raeeini - 02/17/2026
//scene teleport trigger
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneTeleport : MonoBehaviour
{
    public string sceneName = "WinScreen";
    private bool triggered;

    void OnTriggerEnter(Collider other)
    {
        if (triggered) return;
        if (!other.CompareTag("Player")) return;

        triggered = true;
        SceneManager.LoadScene(sceneName);
    }
}