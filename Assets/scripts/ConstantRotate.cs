//Eyad Al Raeeini - 02/17/2026
//constant rotation

using UnityEngine;

public class ConstantRotate : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0f, 90f, 0f);

    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}