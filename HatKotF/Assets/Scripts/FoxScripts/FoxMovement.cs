using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class FoxMovement : MonoBehaviour
{
    private float speed;
    private float distanceToPlayer;
    public float sneakAnimationSpeed, walkAnimationSpeed;
    public float baseSpeed;
    public float followDistance;
    public float waitingDistance;

    public Vector3 offset;
    private Vector3 objectivePosition;

    public Transform targetToFollow, playerTransform;
    public GameObject objective;
    private Animator animator;
    public FOXSTATES foxState;
    PlayerMovement playerMovementScript;

    private void Awake()
    {
        playerMovementScript = targetToFollow.GetComponentInParent<PlayerMovement>();
        playerMovementScript.SetFollower(this.transform);
        animator = GetComponentInChildren<Animator>();

        targetToFollow.position = playerTransform.position + offset;
    }

    private void Update()
    {
        distanceToPlayer = Vector3.Distance(targetToFollow.position, transform.position);

        if (distanceToPlayer > followDistance)
        {
            if (playerMovementScript.playerState == PLAYERSTATE.IDLE)
            {
                SetFoxState(FOXSTATES.MOVE_TO_IDLE);
            }
            else if (playerMovementScript.playerState == PLAYERSTATE.WALK)
            {
                SetFoxState(FOXSTATES.WALK);
            }
            else if (playerMovementScript.playerState == PLAYERSTATE.RUN)
            {
                SetFoxState(FOXSTATES.RUN);
            }
            else if (playerMovementScript.playerState == PLAYERSTATE.SNEAK)
            {
                SetFoxState(FOXSTATES.WALK);
            }
        }
        else if (distanceToPlayer > waitingDistance && distanceToPlayer < followDistance)
        {
            if (playerMovementScript.playerState == PLAYERSTATE.IDLE)
            {
                SetFoxState(FOXSTATES.MOVE_TO_IDLE);
            }
            else if (playerMovementScript.playerState == PLAYERSTATE.WALK)
            {
                SetFoxState(FOXSTATES.WALK);
            }
            else if (playerMovementScript.playerState == PLAYERSTATE.RUN)
            {
                SetFoxState(FOXSTATES.RUN);
            }
            else if (playerMovementScript.playerState == PLAYERSTATE.SNEAK)
            {
                SetFoxState(FOXSTATES.SNEAK);
            }
            else if (playerMovementScript.playerState == PLAYERSTATE.FOLLOW)
            {
                SetFoxState(FOXSTATES.EVADE);
            }
        }
        else if (distanceToPlayer < waitingDistance)
        {
            if (playerMovementScript.playerState == PLAYERSTATE.IDLE)
            {
                SetFoxState(FOXSTATES.IDLE);
            }
            else if (playerMovementScript.playerState == PLAYERSTATE.WALK)
            {
                SetFoxState(FOXSTATES.WALK);
            }
            else if (playerMovementScript.playerState == PLAYERSTATE.RUN)
            {
                SetFoxState(FOXSTATES.RUN);
            }
            else if (playerMovementScript.playerState == PLAYERSTATE.SNEAK)
            {
                SetFoxState(FOXSTATES.SNEAK);
            }

        }

    }

    private void Follow()
    {
        speed = playerMovementScript.GetAcceleration() + baseSpeed;

        transform.LookAt(targetToFollow);

        if (distanceToPlayer > waitingDistance)
        {
            Vector3 directionNormalized = (targetToFollow.position - transform.position).normalized;
            transform.position = transform.position + directionNormalized * speed * Time.deltaTime;
        }
    }

    private void MoveToIdle()
    {
        objectivePosition = objective.transform.position;
        Vector3 objectiveDirection = (objectivePosition - transform.position).normalized;
        Vector3 waitingPosition = transform.position + (objectiveDirection * 0.2f);

        Vector3 newRotationDirection = Vector3.RotateTowards(transform.forward, waitingPosition - transform.position, 2 * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newRotationDirection);

        if (distanceToPlayer > waitingDistance && distanceToPlayer < followDistance)
        {
            transform.position = waitingPosition;
            transform.LookAt(waitingPosition);
        }
        else
        {
            SetFoxState(FOXSTATES.IDLE);
        }
    }

    private void Idle()
    {
        Vector3 newRotationDirection = Vector3.RotateTowards(transform.forward, objectivePosition - transform.position, 2 * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newRotationDirection);
    }

    private void Evade()
    {
        print("evading player");

    }

    private void SetFoxState(FOXSTATES newFoxState)
    {
        if (foxState != newFoxState)
        {
            foxState = newFoxState;
        }

        switch (foxState)
        {
            case FOXSTATES.IDLE:
                animator.SetBool("isWalking", false);
                animator.SetBool("isWaiting", true);
                Idle();
                break;
            case FOXSTATES.WALK:
                animator.SetBool("isWalking", true);
                animator.SetBool("isRunning", false);
                animator.SetFloat("speedMultiplier", walkAnimationSpeed);
                animator.SetBool("isWaiting", false);
                Follow();
                break;
            case FOXSTATES.RUN:
                animator.SetBool("isWalking", true);
                animator.SetBool("isRunning", true);
                animator.SetBool("isWaiting", false);
                Follow();
                break;
            case FOXSTATES.SNEAK:
                animator.SetBool("isWaiting", false);
                animator.SetBool("isWalking", true);
                animator.SetBool("isRunning", false);
                animator.SetFloat("speedMultiplier", sneakAnimationSpeed);
                Follow();
                break;
            case FOXSTATES.MOVE_TO_IDLE:
                animator.SetBool("isWaiting", true);
                animator.SetBool("isWalking", true);
                animator.SetBool("isRunning", false);
                MoveToIdle();
                break;
            case FOXSTATES.EVADE:
                animator.SetBool("isWalking", true);
                animator.SetBool("isRunning", false);
                animator.SetFloat("speedMultiplier", walkAnimationSpeed);
                animator.SetBool("isWaiting", false);
                Evade();
                break;
        }
    }
}
