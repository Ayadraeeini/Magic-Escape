//Eyad Al Raeeini - 02/17/2026
//player inventory

using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool hasKey = false;

    public void CollectKey()
    {
        hasKey = true;
    }
}