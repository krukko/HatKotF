using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PLAYERSTATE { IDLE, WALK, RUN, SNEAK, JUMP, FOLLOW }

public class PlayerMovement : MonoBehaviour
{
    public float walkingSpeed = 10;
    public float runningSpeed = 15;
    public float slowWalkSpeed = 5;
    public float jumpSpeed = 10;
    public float maxVelocity = 10;
    public float turningSpeed = 30;
    private float inputHorizontal;
    private float inputVertical;
    private float acceleration;

    public PLAYERSTATE playerState;

    private Animator animator;
    private CapsuleCollider col;
    public LayerMask collisionMask;
    private Rigidbody rb;
    private Transform follower;

    private void Awake()
    {
        col = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        inputHorizontal = Input.GetAxis("Horizontal"); ;
        inputVertical = Input.GetAxis("Vertical");

        Rotate();

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            inputHorizontal = 0;
        }

        if (inputHorizontal == 0 && inputVertical == 0)
        {
            acceleration = 0;
            SetPlayerState(PLAYERSTATE.IDLE);
        }
        else if(IsGrounded())
        {
            Move(inputHorizontal, inputVertical);
        }

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
       if (Input.GetKey(KeyCode.LeftShift))
        {
            acceleration = runningSpeed;
            SetPlayerState(PLAYERSTATE.RUN);
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            acceleration = slowWalkSpeed;
            SetPlayerState(PLAYERSTATE.SNEAK);
        }
        else
        {
            acceleration = walkingSpeed;
            SetPlayerState(PLAYERSTATE.WALK);
        }

        Vector3 directionToFollower = (follower.transform.position - transform.position).normalized;
        float dotproductOfDirectionToFollower = Vector3.Dot(directionToFollower, transform.forward);

        print(dotproductOfDirectionToFollower);

        if (dotproductOfDirectionToFollower > 0.9f)
        {
            SetPlayerState(PLAYERSTATE.FOLLOW);
        }

        rb.velocity = Vector3.zero;

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

    }

    private void Rotate()
    {
        if (!Input.GetKey(KeyCode.Mouse1))
        {
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

    public float GetAcceleration()
    {
        return acceleration;
    }

    private void SetPlayerState(PLAYERSTATE newState)
    {
        if (playerState != newState)
        {
            playerState = newState;
        }

        SetAnimations();
    }

    public PLAYERSTATE GetPlayerState()
    {
        return playerState;
    }

    private void SetAnimations()
    {
        switch (playerState)
        {
            case PLAYERSTATE.IDLE:
                animator.SetBool("isWalking", false);
                animator.SetBool("isRunning", false);
                animator.SetBool("isSneaking", false);
                break;
            case PLAYERSTATE.WALK:
                animator.SetBool("isWalking", true);
                animator.SetBool("isRunning", false);
                animator.SetBool("isSneaking", false);
                animator.SetFloat("speedMultiplier", inputVertical);
                break;
            case PLAYERSTATE.RUN:
                animator.SetBool("isWalking", true);
                animator.SetBool("isSneaking", false);
                animator.SetBool("isRunning", true);
                break;
            case PLAYERSTATE.SNEAK:
                animator.SetBool("isWalking", true);
                animator.SetBool("isRunning", false);
                animator.SetBool("isSneaking", true);
                break;
            case PLAYERSTATE.JUMP:
                break;
            case PLAYERSTATE.FOLLOW:
                break;
        }
    }

    public void SetFollower(Transform newFollower)
    {
        follower = newFollower;
    }
}
