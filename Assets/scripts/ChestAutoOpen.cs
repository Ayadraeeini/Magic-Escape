using System.Collections;
using UnityEngine;

public class ChestAutoOpen : MonoBehaviour
{
    public Transform chestTop;

    public Vector3 openRotation = new Vector3(-90f, 0f, 0f);
    public float openSpeed = 4f;

    public GameObject coinPrefab;
    public Transform coinSpawnPoint;
    public int goldAmount = 25;

    public SimplePopup popup;

    private bool opening = false;
    private bool opened = false;

    void Update()
    {
        if (!opening || chestTop == null) return;

        Vector3 current = chestTop.localEulerAngles;

        float x = Mathf.LerpAngle(current.x, openRotation.x, Time.deltaTime * openSpeed);
        float y = Mathf.LerpAngle(current.y, openRotation.y, Time.deltaTime * openSpeed);
        float z = Mathf.LerpAngle(current.z, openRotation.z, Time.deltaTime * openSpeed);

        chestTop.localEulerAngles = new Vector3(x, y, z);
    }

    void OnTriggerEnter(Collider other)
    {
        if (opened) return;
        if (!other.CompareTag("Player")) return;

        PlayerInventory inv = other.GetComponent<PlayerInventory>();
        if (inv == null) return;
        if (!inv.hasKey) return;

        opened = true;
        opening = true;

        StartCoroutine(SpawnCoin(other.gameObject));
    }

    IEnumerator SpawnCoin(GameObject player)
    {
        yield return new WaitForSeconds(0.3f);

        GameObject coin = null;

        if (coinPrefab != null && coinSpawnPoint != null)
        {
            coin = Instantiate(coinPrefab, coinSpawnPoint.position, coinSpawnPoint.rotation);
        }

        PlayerGold gold = player.GetComponent<PlayerGold>();
        if (gold != null)
            gold.AddGold(goldAmount);

        if (popup != null)
            popup.Show("Gold collected");

        if (coin != null)
            Destroy(coin, 1.5f);
    }
}