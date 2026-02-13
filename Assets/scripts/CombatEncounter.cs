//Eyad Al Raeeini - 02/10/2026
//combat encounter trigger

using UnityEngine;
public class CombatEncounter : MonoBehaviour
{
    public int comboLength = 5;
    public int damageOnFail = 15;
    private bool triggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (triggered) return;
        if (!other.CompareTag("Player")) return;

        triggered = true;
        CombatManager.Instance.StartEncounter(gameObject, comboLength, damageOnFail, other.gameObject);
    }
}