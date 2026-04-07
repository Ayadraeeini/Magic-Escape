//Eyad Al Raeeini - 02/17/2026
//player health system
using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public event Action<int, int> OnHealthChanged;

    void Awake()
    {
        currentHealth = maxHealth;
        NotifyHealthChanged();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth < 0)
            currentHealth = 0;

        NotifyHealthChanged();
    }

    public void Heal(int amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        NotifyHealthChanged();
    }

    void NotifyHealthChanged()
    {
        if (OnHealthChanged != null)
            OnHealthChanged(currentHealth, maxHealth);
    }
}