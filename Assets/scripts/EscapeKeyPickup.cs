//Eyad Al Raeeini - 05/02/2026
//escape key pickup with proper message hide

using System.Collections;
using UnityEngine;
using TMPro;

public class EscapeKeyPickup : MonoBehaviour
{
    public EscapeDoorManager manager;

    public TMP_Text middleText;

    private bool collected;

    void Start()
    {
        if (middleText != null)
        {
            middleText.text = "";
            middleText.enabled = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (collected)
            return;

        if (!other.CompareTag("Player"))
            return;

        collected = true;

        StartCoroutine(CollectRoutine());
    }

    IEnumerator CollectRoutine()
    {
        if (manager != null)
            manager.CollectKey();

        if (manager != null && manager.currentKeys == 1)
        {
            if (middleText != null)
            {
                middleText.enabled = true;

                middleText.text =
                    "Find 2 more white keys to open your way out";
            }

            yield return new WaitForSeconds(4f);

            if (middleText != null)
            {
                middleText.text = "";
                middleText.enabled = false;
            }
        }

        Destroy(gameObject);
    }
}