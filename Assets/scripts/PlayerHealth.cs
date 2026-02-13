//Eyad Al Raeeini - 02/10/2026
//player health

using System.Collections.Generic;
using UnityEngine;
public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0) currentHealth = 0;

        Debug.Log("Player HP: " + currentHealth);
    }
}
