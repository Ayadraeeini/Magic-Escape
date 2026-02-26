//Eyad Al Raeeini - 02/17/2026
//lever interactor for mobile button
using UnityEngine;
using UnityEngine.UI;
public class LeverInteractor : MonoBehaviour
{
    public GameObject interactButtonObject;
    public Button interactButton;

    private LeverInteractable lever;
    private bool playerInside;

    private static LeverInteractor activeLever;

    void Awake()
    {
        lever = GetComponent<LeverInteractable>();
    }

    void Start()
    {
        if (interactButtonObject != null)
            interactButtonObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerInside = true;
        activeLever = this;

        interactButtonObject.SetActive(true);
        interactButton.onClick.RemoveAllListeners();
        interactButton.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        if (activeLever == this && playerInside && lever != null)
        {
            lever.Interact();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerInside = false;

        if (activeLever == this)
        {
            activeLever = null;
            interactButton.onClick.RemoveAllListeners();
            interactButtonObject.SetActive(false);
        }
    }
}