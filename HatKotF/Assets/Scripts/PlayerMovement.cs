using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float acceleration;
    public float walkingSpeed = 10;
    public float runningSpeed = 15;
    public float slowWalkSpeed = 5;
    public float jumpSpeed = 10;
    public float maxVelocity = 10;
    public float turningSpeed = 30;

    private float distToGround;
    private Vector2 input;

    private Rigidbody rb;
    private CapsuleCollider col;
    private Animator animator;
    public LayerMask collisionMask;

    private void Awake()
    {
        col = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    //TODO: add animations

    private void FixedUpdate()
    {
        float inputHorizontal = Input.GetAxis("Horizontal"); ;
        float inputVertical = Input.GetAxis("Vertical");

        Move(inputHorizontal, inputVertical);

        if (IsGrounded())
        {
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }
    }

    private void Move(float _inputHorizontal, float _inputVertical)
    {
        if (_inputHorizontal == 0 && _inputVertical == 0) {
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
            animator.SetBool("isSneaking", false);
            acceleration = 0;
        } 
        else {
            animator.SetBool("isWalking", true);
            animator.SetFloat("speedMultiplier", _inputVertical);
            acceleration = walkingSpeed;
        }

        if (Input.GetKey(KeyCode.LeftShift)) {
            animator.SetBool("isRunning", true);
            acceleration = runningSpeed;
        } 
        else if (Input.GetKey(KeyCode.LeftControl)) {
            animator.SetBool("isSneaking", true);
            acceleration = slowWalkSpeed;
        } 
        else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.LeftControl)) {
            animator.SetBool("isSneaking", false);
            animator.SetBool("isRunning", true);
            acceleration = runningSpeed;
        } 
        else{
            acceleration = walkingSpeed;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift)) {
            animator.SetBool("isRunning", false);
        }
        if (Input.GetKeyUp(KeyCode.LeftControl)) {
            animator.SetBool("isSneaking", false);
        }

        if (rb.velocity.x >= maxVelocity || rb.velocity.x <= -maxVelocity) {
            rb.velocity = new Vector3(Mathf.Sign(rb.velocity.x) * maxVelocity, rb.velocity.y, rb.velocity.z);
        }
        if (rb.velocity.z >= maxVelocity || rb.velocity.z <= -maxVelocity) {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, Mathf.Sign(rb.velocity.z) * maxVelocity);
        }

        Vector3 movement = new Vector3(_inputHorizontal, 0.0f, _inputVertical);

        rb.AddRelativeForce(movement * acceleration, ForceMode.Impulse);

        if (!Input.GetKey(KeyCode.Mouse1)) {
            Quaternion deltaRotation = Quaternion.Euler(new Vector3(0f, turningSpeed * Input.GetAxis("Mouse X"), 0f) * Time.deltaTime);
            rb.MoveRotation(rb.rotation * deltaRotation);
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
    }

    private bool IsGrounded()
    {
        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x, col.bounds.min.y - 0.1f, col.bounds.center.z), 0.18f, collisionMask);
    }

    //Use to get Player's acceleration from another script
    public float GetAcceleration()
    {
        return acceleration;
    }
}
