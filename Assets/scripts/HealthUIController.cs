//Eyad Al Raeeini - 05/02/2026
//heart UI controller
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeartUIController : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public TMP_Text healthText;
    public RectTransform warningHeart;
    public float pulseSpeed = 4f;
    public float pulseSize = 0.15f;
    public float showTime = 5f;
    public int lowHealthThreshold = 40;

    private Coroutine hideRoutine;

    void Start()
    {
        SetVisible(false);

        if (warningHeart != null)
            warningHeart.gameObject.SetActive(false);
    }

    void Update()
    {
        if (playerHealth == null) return;

        if (healthText != null)
            healthText.text = playerHealth.currentHealth + "/" + playerHealth.maxHealth;

        UpdateHearts();
        HandleLowHealthWarning();

        if (playerHealth.currentHealth <= lowHealthThreshold)
           ShowPermanent();
    }

    void UpdateHearts()
    {
        float healthPerHeart = (float)playerHealth.maxHealth / hearts.Length;

        for (int i = 0; i < hearts.Length; i++)
        {
           if (hearts[i] == null) continue;
            hearts[i].sprite = playerHealth.currentHealth >= (i + 1) * healthPerHeart ? fullHeart : emptyHeart;
        }
    }

    void HandleLowHealthWarning()
    {
        if (warningHeart == null) return;

        bool low = playerHealth.currentHealth < 50;
        warningHeart.gameObject.SetActive(low);
        if (low)
            warningHeart.localScale = Vector3.one * (1f + Mathf.Sin(Time.time * pulseSpeed) * pulseSize);
    }

    public void OnTakeDamage()
    {
        if (playerHealth.currentHealth <= lowHealthThreshold) return;

        SetVisible(true);
        if (hideRoutine != null) StopCoroutine(hideRoutine);
         hideRoutine = StartCoroutine(HideAfterDelay());
    }

    IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(showTime);
        if (playerHealth.currentHealth > lowHealthThreshold)
            SetVisible(false);
    }

    void ShowPermanent()
    {
        if (hideRoutine != null)
        {
            StopCoroutine(hideRoutine);
            hideRoutine = null;
        }

        SetVisible(true);
    }

    void SetVisible(bool state)
    {
        foreach (var heart in hearts)
        {
            if (heart != null)
                heart.gameObject.SetActive(state);
        }

        if (healthText != null)
            healthText.gameObject.SetActive(state);
    }
}