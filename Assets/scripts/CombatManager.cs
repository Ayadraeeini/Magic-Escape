using System.Collections.Generic;
using UnityEngine;



public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance;

    public GameObject combatUI;
    public CombatSequenceUI sequenceUI;

    private List<Direction> sequence;
    private int index;

    private GameObject currentEnemy;
    private PlayerHealth playerHealth;
    private MobileFirstPerson movement;

    private int damageOnFail;

    void Awake()
    {
        Instance = this;

        if (combatUI != null)
            combatUI.SetActive(false);
    }

    // 🔥 THIS is the method your enemy is calling
    public void StartEncounter(GameObject enemy, int comboLength, int damage, GameObject player)
    {
        currentEnemy = enemy;
        damageOnFail = damage;

        playerHealth = player.GetComponent<PlayerHealth>();
        movement = player.GetComponent<MobileFirstPerson>();

        if (movement != null)
            movement.enabled = false;

        sequence = GenerateSequence(comboLength);
        index = 0;

        if (combatUI != null)
            combatUI.SetActive(true);

        if (sequenceUI != null)
            sequenceUI.ShowSequence(sequence, index);

        SwipeInput.Instance.Begin(this);
    }

    public void ReceiveSwipe(Direction dir)
    {
        if (sequence == null) return;

        if (dir == sequence[index])
        {
            index++;
            sequenceUI.ShowSequence(sequence, index);

            if (index >= sequence.Count)
                Win();
        }
        else
        {
            Fail();
        }
    }

    void Win()
    {
        Destroy(currentEnemy);
        EndEncounter();
    }

    void Fail()
    {
        if (playerHealth != null)
            playerHealth.TakeDamage(damageOnFail);

        index = 0;
        sequenceUI.ShowSequence(sequence, index);
    }

    void EndEncounter()
    {
        if (combatUI != null)
            combatUI.SetActive(false);

        if (movement != null)
            movement.enabled = true;

        SwipeInput.Instance.End();
    }

    List<Direction> GenerateSequence(int length)
    {
        var list = new List<Direction>();

        for (int i = 0; i < length; i++)
            list.Add((Direction)Random.Range(0, 4));

        return list;
    }
}
