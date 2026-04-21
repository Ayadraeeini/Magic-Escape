//Eyad Al Raeeini - 02/17/2026
//simple popup text FIXED

using System.Collections;
using UnityEngine;
using TMPro;

public class SimplePopup : MonoBehaviour
{
    public TMP_Text text;
    public float showTime = 1.2f;

    private Coroutine currentRoutine;

    void Awake()
    {
        if (text != null)
        {
            Color c = text.color;
            c.a = 0f;
            text.color = c;
        }
    }

    public void Show(string message)
    {
        if (text == null) return;

        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(ShowRoutine(message));
    }

    IEnumerator ShowRoutine(string message)
    {
        text.text = message;

        // SHOW
        Color c = text.color;
        c.a = 1f;
        text.color = c;

        yield return new WaitForSeconds(showTime);

        // HIDE
        c.a = 0f;
        text.color = c;
    }
}