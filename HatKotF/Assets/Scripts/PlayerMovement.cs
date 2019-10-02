using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float acceleration = 10;
    public float jumpSpeed = 10;
    public float maxVelocity = 10;
    public float turningSpeed = 60;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void Move()
    {
        float inputHorizontal = Input.GetAxis("Horizontal");
        float inputVertical = Input.GetAxis("Vertical");

        if (rb.velocity.x >= maxVelocity || rb.velocity.x <= -maxVelocity)
        {
            rb.velocity = new Vector3(Mathf.Sign(rb.velocity.x) * maxVelocity, rb.velocity.y, rb.velocity.z);
        }

        Vector3 movement = new Vector3(inputHorizontal, 0.0f, inputVertical);

        rb.AddForce(movement * acceleration, ForceMode.Acceleration);
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
    }
}
