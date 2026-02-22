//Eyad Al Raeeini - 02/17/2026
//lever interaction opens door
using UnityEngine;
public class LeverInteractable : MonoBehaviour
{
    public float targetZRotation = -40f;
    public float rotateSpeed = 8f;
    public DoorController door;
    private bool activated = false;

    public void Interact()
    {
        if (activated) return;
        activated = true;

        if (door != null)
            door.OpenDoor();
    }

    void Update()
    {
        if (!activated) return;

        Vector3 current = transform.localEulerAngles;
        float z = Mathf.LerpAngle(current.z, targetZRotation, Time.deltaTime * rotateSpeed);
        transform.localEulerAngles = new Vector3(current.x, current.y, z);
    }
}