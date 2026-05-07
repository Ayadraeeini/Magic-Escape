//Eyad Al Raeeini - 05/06/2026
//player health system with game over
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public HeartUIController healthUI;
    public string gameOverSceneName = "GameOver";

    private bool dead;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (dead) return;

        currentHealth = Mathf.Max(0, currentHealth - amount);

        if (healthUI != null)
            healthUI.OnTakeDamage();

        if (currentHealth <= 0)
        {
            dead = true;
            SceneManager.LoadScene(gameOverSceneName);
        }
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
    }
}