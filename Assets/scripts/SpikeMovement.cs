//Eyad Al Raeeini - 02/17/2026
//spike movement back and forth

using UnityEngine;

public class SpikeMovement : MonoBehaviour
{
    public float startZ = 0f;
    public float endZ = -3f;
    public float moveDuration = 2f;

    float timer = 0f;
    bool movingForward = true;

    void Update()
    {
        timer += Time.deltaTime;

        float t = timer / moveDuration;

        Vector3 pos = transform.position;

        if (movingForward)
            pos.z = Mathf.Lerp(startZ, endZ, t);
        else
            pos.z = Mathf.Lerp(endZ, startZ, t);

        transform.position = pos;

        if (timer >= moveDuration)
        {
            timer = 0f;
            movingForward = !movingForward;
        }
    }
}