//Eyad Al Raeeini - 02/17/2026
//shop ui system
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShopUI : MonoBehaviour
{
    public GameObject panel;
    public TMP_Text goldText;
    public TMP_Text healthPriceText;
    public TMP_Text shieldPriceText;
    public Button closeButton;
    public int healthPrice = 25;
    public int shieldPrice = 40;
    public int healthAmount = 25;
    public int shieldAmount = 25;
    private PlayerGold playerGold;
    private PlayerHealth playerHealth;
    private PlayerShield playerShield;
    public Button healthButton;
    public Button shieldButton;


    void Start()
    {
        panel.SetActive(false);
        closeButton.onClick.AddListener(CloseShop);
        healthButton.onClick.AddListener(BuyHealth);
        shieldButton.onClick.AddListener(BuyShield);

        healthPriceText.text = "Health: " + healthPrice + " Gold";
        shieldPriceText.text = "Shield: " + shieldPrice + " Gold";
    }

    public void OpenShop(GameObject player)
    {
        playerGold = player.GetComponent<PlayerGold>();
        playerHealth = player.GetComponent<PlayerHealth>();
         playerShield = player.GetComponent<PlayerShield>();

        panel.SetActive(true);
        UpdateGoldText();
    }

    public void CloseShop()
    {
       panel.SetActive(false);
    }

    void BuyHealth()
    {
        if (playerGold == null || playerHealth == null) return;

        if (playerGold.SpendGold(healthPrice))
        {
            playerHealth.Heal(healthAmount);
            UpdateGoldText();
        }
    }

    void BuyShield()
    {
        if (playerGold == null || playerShield == null) return;

        if (playerGold.SpendGold(shieldPrice))
        {
            playerShield.AddShield(shieldAmount);
             UpdateGoldText();
        }
    }

    void UpdateGoldText()
    {
        if (playerGold != null)
          goldText.text = "Gold: " + playerGold.CurrentGold;
    }
}