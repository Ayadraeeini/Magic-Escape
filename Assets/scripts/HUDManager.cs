//Eyad Al Raeeini - 02/17/2026
//HUD manager

using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public TMP_Text hpText;
    public GameObject hpObject;
    public PlayerHealth playerHealth;

    void Start()
    {
        if (hpObject != null)
            hpObject.SetActive(false);

        UpdateHPText();
    }

    void Update()
    {
        UpdateHPText();
    }

    void UpdateHPText()
    {
        if (playerHealth == null || hpText == null) return;

        hpText.text = "HP: " + playerHealth.currentHealth + "/" + playerHealth.maxHealth;
    }

    public void ShowHP()
    {
        if (hpObject != null)
            hpObject.SetActive(true);

        UpdateHPText();
    }

    public void HideHP()
    {
        if (hpObject != null)
            hpObject.SetActive(false);
    }
}