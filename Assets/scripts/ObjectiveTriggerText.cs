//Eyad Al Raeeini - 02/17/2026
//objective trigger text
using UnityEngine;
using System.Collections;
using TMPro;
public class ObjectiveTriggerText : MonoBehaviour
{
    public GameObject objectiveTextObject;
    public TMP_Text objectiveText;
    public string message = "OBJECTIVE: Find the lever to unlock the door";
    public float showSeconds = 15f;

    private bool triggered;

    void OnTriggerEnter(Collider other)
    {
        if (triggered) return;
        if (!other.CompareTag("Player")) return;

        triggered = true;

        if (objectiveText != null)
            objectiveText.text = message;

        objectiveTextObject.SetActive(true);
        StartCoroutine(HideAfter());
    }

    IEnumerator HideAfter()
    {
        yield return new WaitForSeconds(showSeconds);
        objectiveTextObject.SetActive(false);
    }
}