//Eyad Al Raeeini - 02/17/2026
//player shield system
using UnityEngine;
public class PlayerShield : MonoBehaviour
{
    public int currentShield = 0;
    public int maxShield = 100;

    public void AddShield(int amount)
    {
         currentShield += amount;
        if (currentShield > maxShield)
            currentShield = maxShield;
    }
}