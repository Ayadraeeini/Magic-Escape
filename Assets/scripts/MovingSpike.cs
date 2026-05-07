//Eyad Al Raeeini - 05/02/2026
//moving spike trap with damage

using UnityEngine;

public class MovingSpike : MonoBehaviour
{
    public float moveDistance = 3f;
    public float moveSpeed = 2f;

    public int damage = 20;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float z = Mathf.Sin(Time.time * moveSpeed) * moveDistance;

        transform.position = new Vector3
        (
            startPos.x,
            startPos.y,
            startPos.z + z
        );
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        PlayerHealth health =
            other.GetComponent<PlayerHealth>();

        if (health != null)
        {
            health.TakeDamage(damage);

            Debug.Log("Spike damaged player");
        }
    }
}