//Eyad Al Raeeini - 02/17/2026
//shop trigger
using UnityEngine;
public class ShopTrigger : MonoBehaviour
{
    public ShopUI shopUI;
    private bool triggered;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

       triggered = true;
       shopUI.OpenShop(other.gameObject);
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        triggered = false;
    }
}