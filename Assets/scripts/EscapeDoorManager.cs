//Eyad Al Raeeini - 05/02/2026
//escape door manager with automatic unlock and opening

using System.Collections;
using UnityEngine;
using TMPro;

public class EscapeDoorManager : MonoBehaviour
{
    public int keysNeeded = 3;

    public int currentKeys;

    public Transform escapeDoor;

    public Vector3 openRotation =
        new Vector3(0f, 90f, 0f);

    public float rotateSpeed = 3f;

    public Camera playerCamera;
    public Camera doorCamera;

    public float cameraShowTime = 3f;

    public TMP_Text topMessage;

    private bool unlocked;
    private bool opening;

    void Update()
    {
        if (opening && escapeDoor != null)
        {
            Vector3 current =
                escapeDoor.localEulerAngles;

            float x =
                Mathf.LerpAngle
                (
                    current.x,
                    openRotation.x,
                    Time.deltaTime * rotateSpeed
                );

            float y =
                Mathf.LerpAngle
                (
                    current.y,
                    openRotation.y,
                    Time.deltaTime * rotateSpeed
                );

            float z =
                Mathf.LerpAngle
                (
                    current.z,
                    openRotation.z,
                    Time.deltaTime * rotateSpeed
                );

            escapeDoor.localEulerAngles =
                new Vector3(x, y, z);
        }
    }
    public void CollectKey()
    {
        currentKeys++;

        if (currentKeys >= keysNeeded && !unlocked)
        {
            unlocked = true;

            opening = true;

            StartCoroutine(UnlockSequence());
        }
    }

    IEnumerator UnlockSequence()
    {
        if (topMessage != null)
        {
            topMessage.gameObject.SetActive(true);

            topMessage.text =
                "ESCAPE DOOR UNLOCKED";
        }

        if (playerCamera != null)
             playerCamera.gameObject.SetActive(false);

        if (doorCamera != null)
                doorCamera.gameObject.SetActive(true);

        yield return new WaitForSeconds(cameraShowTime);

        if (doorCamera != null)
                     doorCamera.gameObject.SetActive(false);

        if (playerCamera != null)
            playerCamera.gameObject.SetActive(true);

        if (topMessage != null)
            topMessage.gameObject.SetActive(false);
    }
}