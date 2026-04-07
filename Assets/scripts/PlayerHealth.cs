//Eyad Al Raeeini - 02/17/2026
//player health system
using UnityEngine;
<<<<<<< HEAD
using System;

public class PlayerHealth : MonoBehaviour
=======
public class PlayerHealth: MonoBehaviour
>>>>>>> 17fe806eb2a0836d47e5fcd17be94c9e70a3698b
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