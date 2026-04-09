//Eyad Al Raeeini - 02/17/2026
//enemy minigame trigger
using UnityEngine;
public class EnemyMiniGameTrigger : MonoBehaviour
{
    public int comboLength = 4;
    public int damageOnFail = 10;
    private bool triggered;

    void OnTriggerEnter(Collider other)
    {
        if (triggered) return;
        if (!other.CompareTag("Player")) return;

        triggered = true;
        SwipeMiniGameManager.Instance.StartMiniGame(gameObject, other.gameObject, comboLength, damageOnFail);
    }
}