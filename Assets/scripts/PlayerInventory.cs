// Eyad Al Raeeini - 02/17/2026
// Player inventory

using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool hasChestKey = false;

    public void CollectChestKey()
    {
        hasChestKey = true;
    }
}