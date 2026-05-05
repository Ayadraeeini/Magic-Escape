//Eyad Al Raeeini - 05/02/2026
//heart UI controller
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HeartUIController : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public float showTime = 5f;
    public int lowHealthThreshold = 40;

    private Coroutine hideRoutine;

    void Start()
    {
        SetVisible(false);
    }

    void Update()
    {
        if (playerHealth == null) return;

        UpdateHearts();

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
    }
}