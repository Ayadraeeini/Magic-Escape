using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public SimplePopup popup;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerInventory inv = other.GetComponent<PlayerInventory>();
        if (inv != null)
        {
            inv.CollectKey();

            if (popup != null)
                popup.Show("Use the Key to Unlock chests and collect gold");
        }

        Destroy(gameObject);
    }
}