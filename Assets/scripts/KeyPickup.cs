// Eyad Al Raeeini - 02/17/2026
// Key pickup

using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public PopupMessage popupMessage;
    public float messageDuration = 1.2f;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerInventory inventory = other.GetComponent<PlayerInventory>();

        if (inventory != null)
        {
            inventory.CollectChestKey();

            if (popupMessage != null)
                popupMessage.ShowMessage("Chest key collected.", messageDuration);
        }

        Destroy(gameObject);
    }
}