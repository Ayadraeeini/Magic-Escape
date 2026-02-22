//Eyad Al Raeeini - 02/17/2026
//lever interactor for mobile button
using UnityEngine;
using UnityEngine.UI;
public class LeverInteractor : MonoBehaviour
{
    public GameObject interactButtonObject;
    public Button interactButton;

    private LeverInteractable lever;
    private bool playerInside = false;

    void Start()
    {
        lever = GetComponent<LeverInteractable>();
        interactButtonObject.SetActive(false);
        interactButton.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        if (playerInside && lever != null)
            lever.Interact();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerInside = true;
        interactButtonObject.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerInside = false;
        interactButtonObject.SetActive(false);
    }
}