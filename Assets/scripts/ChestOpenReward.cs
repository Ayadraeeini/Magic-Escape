//Eyad Al Raeeini - 02/17/2026
//chest open reward
using System.Collections;
using UnityEngine;
public class ChestOpenReward : MonoBehaviour
{
    public Transform chestTop;
    public Vector3 targetOpenRotation = new Vector3(-90f, 0f, 0f);
    public float openSpeed = 4f;

    public GameObject coinModel;
    public Transform coinSpawnPoint;
    public int goldAmount = 25;
    public float coinRiseHeight = 1.5f;
    public float coinRiseTime = 0.5f;
    public float coinScale = 2f;

    public PopupMessage popupMessage;
    public float messageDuration = 1.2f;

    private bool opening = false;
    private bool opened = false;

    void Update()
    {
        if (!opening || chestTop == null) return;

        Vector3 current = chestTop.localEulerAngles;
        float x = Mathf.LerpAngle(current.x, targetOpenRotation.x, Time.deltaTime * openSpeed);
        float y = Mathf.LerpAngle(current.y, targetOpenRotation.y, Time.deltaTime * openSpeed);
        float z = Mathf.LerpAngle(current.z, targetOpenRotation.z, Time.deltaTime * openSpeed);
        chestTop.localEulerAngles = new Vector3(x, y, z);
    }

    void OnTriggerEnter(Collider other)
    {
        if (opened) return;
        if (!other.CompareTag("Player")) return;

        PlayerInventory inventory = other.GetComponent<PlayerInventory>();
        if (inventory == null) return;
        if (!inventory.hasChestKey) return;
        if (chestTop == null) return;

        opened = true;
        opening = true;
        StartCoroutine(OpenChestRoutine(other.gameObject));
    }

    IEnumerator OpenChestRoutine(GameObject player)
    {
        GameObject spawnedCoin = null;
        if (coinModel != null && coinSpawnPoint != null)
        {
            spawnedCoin = Instantiate(coinModel, coinSpawnPoint.position, coinSpawnPoint.rotation);
            spawnedCoin.transform.localScale = Vector3.one * coinScale;

            Vector3 startPos = spawnedCoin.transform.position;
            Vector3 endPos = startPos + Vector3.up * coinRiseHeight;

            float t = 0f;
            while (t < coinRiseTime)
            {
                t += Time.deltaTime;
                float p = t / coinRiseTime;
                spawnedCoin.transform.position = Vector3.Lerp(startPos, endPos, p);
                yield return null;
            }
        }

        PlayerGold gold = player.GetComponent<PlayerGold>();
        if (gold != null)
            gold.AddGold(goldAmount);

        if (popupMessage != null)
            popupMessage.ShowMessage("Coin collected.", messageDuration);

        if (spawnedCoin != null)
            Destroy(spawnedCoin, 1f);
    }
}