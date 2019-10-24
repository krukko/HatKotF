using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float acceleration = 10;
    public float jumpSpeed = 10;
    public float maxVelocity = 10;
    public float turningSpeed = 30;

    private float distToGround;
    private Vector2 input;

    private Rigidbody rb;
    private CapsuleCollider col;
    public LayerMask collisionMask;

    private void Start()
    {
        col = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float inputHorizontal = Input.GetAxis("Horizontal"); ;
        float inputVertical = Input.GetAxis("Vertical");

        Move(inputHorizontal, inputVertical);

        if(IsGrounded())
        {
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }      
    }

    private void Move(float _inputHorizontal, float _inputVertical)
    {
        if (rb.velocity.x >= maxVelocity || rb.velocity.x <= -maxVelocity)
        {
            rb.velocity = new Vector3(Mathf.Sign(rb.velocity.x) * maxVelocity, rb.velocity.y, rb.velocity.z);
        }

        if (rb.velocity.z >= maxVelocity || rb.velocity.z <= -maxVelocity)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, Mathf.Sign(rb.velocity.z) * maxVelocity);
        }

        Vector3 movement = new Vector3(_inputHorizontal, 0.0f, _inputVertical);

        rb.AddRelativeForce(movement * acceleration, ForceMode.Impulse);

        Quaternion deltaRotation = Quaternion.Euler(new Vector3(0f, turningSpeed * Input.GetAxis("Mouse X"), 0f) * Time.deltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
    }

    private bool IsGrounded()
    {
        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x, col.bounds.min.y - 0.1f, col.bounds.center.z), 0.18f, collisionMask);
    }
}
