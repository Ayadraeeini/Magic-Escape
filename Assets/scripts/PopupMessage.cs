//Eyad AlR
//ui text
//4/2/2026



using System.Collections;
using TMPro;
using UnityEngine;

public class PopupMessage : MonoBehaviour
{
    public TMP_Text popupText;
    public float defaultShowTime = 1.5f;
    private Coroutine currentRoutine;

    public void ShowMessage(string message)
    {
        ShowMessage(message, defaultShowTime);
    }

    public void ShowMessage(string message, float duration)
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(ShowRoutine(message, duration));
    }

    IEnumerator ShowRoutine(string message, float duration)
    {
       popupText.text = message;
        popupText.gameObject.SetActive(true);

        yield return new WaitForSeconds(duration);

        popupText.text = "";
        popupText.gameObject.SetActive(false);
        currentRoutine = null;
    }
}