//Eyad Al Raeeini - 02/17/2026
//door controller
using UnityEngine;
public class DoorController : MonoBehaviour
{
    public float openYRotation = 111f;
    public float rotateSpeed = 5f;
    private bool open = false;

    public void OpenDoor()
    {
        open = true;
    }

    void Update()
    {
        if (!open) return;

        Vector3 current = transform.localEulerAngles;
        float y = Mathf.LerpAngle(current.y, openYRotation, Time.deltaTime * rotateSpeed);
        transform.localEulerAngles = new Vector3(current.x, y, current.z);
    }
}