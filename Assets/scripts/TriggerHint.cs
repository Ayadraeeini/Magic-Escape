//Eyad Al Raeeini - 02/17/2026
//trigger hint message

using UnityEngine;

public class TriggerHint : MonoBehaviour
{
    public SimplePopup popup;
    public string message = "Find a key, unlock the chest, and collect the gold.";

    public bool triggerOnce = true;

    private bool triggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (triggered && triggerOnce) return;
        if (!other.CompareTag("Player")) return;

        triggered = true;

        if (popup != null)
            popup.Show(message);
    }
}