using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int goldAmount = 25;
    public PopupMessage popupMessage;
    public float messageDuration = 1.2f;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerGold gold = other.GetComponent<PlayerGold>();

        if (gold != null)
        {
            gold.AddGold(goldAmount);

            if (popupMessage != null)
                popupMessage.ShowMessage("Coin collected.", messageDuration);
        }

        Destroy(gameObject);
    }
}