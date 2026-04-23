//Eyad Al Raeeini - 02/17/2026
//test lever reset (auto trigger)

using System.Collections;
using UnityEngine;

public class LeverTestReset : MonoBehaviour
{
    public float targetZRotation = -40f;
    public float rotateSpeed = 8f;
    public float resetDelay = 1.5f;

    private float startZ;
    private bool activated = false;
    private bool resetting = false;

    void Start()
    {
        startZ = transform.localEulerAngles.z;
    }

    void OnTriggerEnter(Collider other)
    {
        if (activated) return;
        if (!other.CompareTag("Player")) return;

        activated = true;
        StartCoroutine(ResetRoutine());
    }

    void Update()
    {
        Vector3 current = transform.localEulerAngles;

        // rotate down
        if (activated && !resetting)
        {
            float z = Mathf.LerpAngle(current.z, targetZRotation, Time.deltaTime * rotateSpeed);
            transform.localEulerAngles = new Vector3(current.x, current.y, z);
        }

        // rotate back up
        if (resetting)
        {
            float z = Mathf.LerpAngle(current.z, startZ, Time.deltaTime * rotateSpeed);
            transform.localEulerAngles = new Vector3(current.x, current.y, z);

            if (Mathf.Abs(Mathf.DeltaAngle(z, startZ)) < 0.5f)
            {
                transform.localEulerAngles = new Vector3(current.x, current.y, startZ);
                resetting = false;
                activated = false;
            }
        }
    }

    IEnumerator ResetRoutine()
    {
        yield return new WaitForSeconds(resetDelay);
        resetting = true;
    }
}