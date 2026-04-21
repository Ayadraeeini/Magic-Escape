using UnityEngine;

public class MobileFirstPerson : MonoBehaviour
{
    public Transform body;
    public float moveSpeed = 4.5f;
    public float turnAngle = 90f;
    public float gravity = -25f;
    public float groundStick = -5f;

    private CharacterController cc;
    private float yVel;

    private bool holdingForward;
    private bool holdingBackward;

    void Awake()
    {
        cc = GetComponent<CharacterController>();

        if (body == null)
            body = transform;
    }

    void Update()
    {
        // Gravity
        if (cc.isGrounded)
        {
            if (yVel < 0)
                yVel = groundStick;
        }
        else
        {
            yVel += gravity * Time.deltaTime;
        }

        Vector3 move = Vector3.zero;

        Vector3 forward = body.forward;
        forward.y = 0;
        forward.Normalize();

        if (holdingForward)
        {
            move += forward * moveSpeed;
        }

        if (holdingBackward)
        {
            move -= forward * moveSpeed;
        }

        cc.Move((move + Vector3.up * yVel) * Time.deltaTime);
    }

    public void ForwardDown()
    {
        holdingForward = true;
    }

    public void ForwardUp()
    {
        holdingForward = false;
    }

  
    public void BackwardDown()
    {
        holdingBackward = true;

        body.Rotate(0f, 180f, 0f);
    }

    public void BackwardUp()
    {
        holdingBackward = false;
    }

    public void TurnLeft()
    {
        body.Rotate(0, -turnAngle, 0);
    }

    public void TurnRight()
    {
        body.Rotate(0, turnAngle, 0);
    }
}