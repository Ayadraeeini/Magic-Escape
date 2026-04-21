//Eyad Al Raeeini - 02/17/2026
//lever interaction opens door with camera cutaway
using System.Collections;
using UnityEngine;
public class LeverInteractable : MonoBehaviour
{
    public float targetZRotation = -40f;
    public float rotateSpeed = 8f;
    public DoorController door;
    public Transform objectToRotate;
    public Vector3 objectTargetRotation = new Vector3(0f, 90f, 0f);
    public float objectRotateSpeed = 5f;
    public Camera doorCam;
    public float doorViewSeconds = 2f;
    public MonoBehaviour playerMovementScript;
    private bool activated = false;
    private bool rotatingObject = false;

    public void Interact()
    {
        if (activated) return;
        activated = true;

        if (door == null) return;

        if (objectToRotate != null)
            rotatingObject = true;

        if (doorCam == null)
        {
            door.OpenDoor();
            return;
        }

        StartCoroutine(DoorCutawayRoutine());
    }

    IEnumerator DoorCutawayRoutine()
    {
        Camera playerCam = Camera.main;
        if (playerCam == null)
        {
            door.OpenDoor();
            yield break;
        }

        if (playerMovementScript != null)
            playerMovementScript.enabled = false;

        playerCam.gameObject.SetActive(false);
        doorCam.gameObject.SetActive(true);

        door.OpenDoor();

        yield return new WaitForSeconds(doorViewSeconds);

        doorCam.gameObject.SetActive(false);
        playerCam.gameObject.SetActive(true);

        if (playerMovementScript != null)
            playerMovementScript.enabled = true;
    }

    void Update()
    {
        if (!activated) return;

        Vector3 current = transform.localEulerAngles;
        float z = Mathf.LerpAngle(current.z, targetZRotation, Time.deltaTime * rotateSpeed);
        transform.localEulerAngles = new Vector3(current.x, current.y, z);

        if (rotatingObject && objectToRotate != null)
        {
            Vector3 objCurrent = objectToRotate.localEulerAngles;
            float x = Mathf.LerpAngle(objCurrent.x, objectTargetRotation.x, Time.deltaTime * objectRotateSpeed);
            float y = Mathf.LerpAngle(objCurrent.y, objectTargetRotation.y, Time.deltaTime * objectRotateSpeed);
            float objZ = Mathf.LerpAngle(objCurrent.z, objectTargetRotation.z, Time.deltaTime * objectRotateSpeed);
            objectToRotate.localEulerAngles = new Vector3(x, y, objZ);
        }
    }
}