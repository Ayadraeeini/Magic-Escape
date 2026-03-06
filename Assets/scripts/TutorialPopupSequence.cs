using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class TutorialPopupSequence : MonoBehaviour
{
    public GameObject tutorialPanel;
    public TMP_Text continueText;
    public MonoBehaviour playerMovementScript;
    public float delayBeforeContinueText = 4f;

    bool triggered;
    bool canTap;

    void Start()
    {
        if (tutorialPanel != null)
            tutorialPanel.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (triggered) return;
        if (!other.CompareTag("Player")) return;

        triggered = true;
        Debug.Log("Tutorial trigger hit by Player. Showing tutorial...");
        ShowTutorial();
    }

    void ShowTutorial()
    {
        // Freeze time
        Time.timeScale = 0f;

        // Freeze movement script
        if (playerMovementScript != null)
            playerMovementScript.enabled = false;

        // Show UI
        if (tutorialPanel == null)
        {
            Debug.LogError("TutorialPanel is NOT assigned in the inspector!");
            return;
        }

        tutorialPanel.SetActive(true);

        if (continueText == null)
        {
            Debug.LogError("ContinueText is NOT assigned in the inspector!");
            return;
        }

        // Hide continue text initially
        SetContinueAlpha(0f);

        canTap = false;
        StartCoroutine(ShowContinueAfterDelay());
    }

    System.Collections.IEnumerator ShowContinueAfterDelay()
    {
        yield return new WaitForSecondsRealtime(delayBeforeContinueText);

        SetContinueAlpha(1f);
        canTap = true;

        Debug.Log("Continue text shown. Tap anywhere to close.");
    }

    void Update()
    {
        if (!canTap) return;

        if (Pointer.current == null) return;

        // Tap/click anywhere
        if (Pointer.current.press.wasPressedThisFrame)
        {
            Debug.Log("Tap detected. Closing tutorial.");
            CloseTutorial();
        }
    }

    void CloseTutorial()
    {
        // Unfreeze
        Time.timeScale = 1f;

        if (playerMovementScript != null)
            playerMovementScript.enabled = true;

        if (tutorialPanel != null)
            tutorialPanel.SetActive(false);

        canTap = false;
    }

    void SetContinueAlpha(float a)
    {
        Color c = continueText.color;
        c.a = a;
        continueText.color = c;
    }
}