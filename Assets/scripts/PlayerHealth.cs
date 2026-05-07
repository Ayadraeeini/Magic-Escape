//Eyad Al Raeeini - 05/02/2026
//player health system with spike damage

using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HeartUIController healthUI;

    public int spikeDamage = 20;

    private float damageCooldown = 1f;
    private float nextDamageTime;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!hit.gameObject.CompareTag("Spike"))
            return;

        if (Time.time < nextDamageTime)
            return;

        nextDamageTime = Time.time + damageCooldown;

        TakeDamage(spikeDamage);

        Debug.Log("Player hit spike");
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth < 0)
            currentHealth = 0;

        if (healthUI != null)
            healthUI.OnTakeDamage();
    }

    public void Heal(int amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }
}