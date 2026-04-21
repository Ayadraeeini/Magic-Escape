//Eyad Al Raeeini - 02/17/2026
//lever sequence interactable
using UnityEngine;
public class LeverSequenceInteractable : MonoBehaviour
{
    public int leverID;
    public LeverPuzzleManager puzzleManager;
    public float targetZRotation = -40f;
    public float rotateSpeed = 8f;

    private bool activated = false;
    private bool resetting = false;
    private float startZ;

    void Start()
    {
        startZ = transform.localEulerAngles.z;
    }

    public void Interact()
    {
        if (activated) return;
        activated = true;
        if (puzzleManager != null)
            puzzleManager.LeverPulled(leverID);
    }

    void Update()
    {
        Vector3 current = transform.localEulerAngles;
        if (activated && !resetting)
        {
            float z = Mathf.LerpAngle(current.z, targetZRotation, Time.deltaTime * rotateSpeed);
            transform.localEulerAngles = new Vector3(current.x, current.y, z);
        }

        if (resetting)
        {
            float z = Mathf.LerpAngle(current.z, startZ, Time.deltaTime * rotateSpeed);
            transform.localEulerAngles = new Vector3(current.x, current.y, z);

            if (Mathf.Abs(Mathf.DeltaAngle(z, startZ)) < 1f)
            {
                resetting = false;
                activated = false;
            }
        }
    }

    public void ResetLever()
    {
        resetting = true;
    }
}