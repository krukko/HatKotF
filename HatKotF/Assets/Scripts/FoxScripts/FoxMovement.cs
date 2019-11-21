using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class FoxMovement : MonoBehaviour
{
    private float speed;
    public float sneakAnimationSpeed, walkAnimationSpeed;
    public float baseSpeed;
    public float followDistance;
    public float waitingDistance;

    Vector3 direction;
    private Vector3 objectivePosition;

    public Transform targetToFollow, playerTransform;
    public GameObject objective; // objective of the player
    private Animator animator;
    public FOXSTATES foxState;
    PlayerMovement playerMovementScript;

    private void Awake()
    {
        playerMovementScript = targetToFollow.GetComponentInParent<PlayerMovement>();
        animator = GetComponentInChildren<Animator>();      
    }

    private void Update()
    {
        direction = targetToFollow.position - transform.position;

        if (direction.sqrMagnitude > followDistance * followDistance) {
            if (playerMovementScript.playerState == PLAYERSTATE.IDLE) {
                SetFoxState(FOXSTATES.IDLE);
            }
            else if (playerMovementScript.playerState == PLAYERSTATE.WALK) {
                SetFoxState(FOXSTATES.WALK);
            }
            else if (playerMovementScript.playerState == PLAYERSTATE.RUN) {
                SetFoxState(FOXSTATES.RUN);
            }
            else if (playerMovementScript.playerState == PLAYERSTATE.SNEAK) {
                SetFoxState(FOXSTATES.WALK);
            }
        }
        else if (direction.sqrMagnitude > waitingDistance * waitingDistance && direction.sqrMagnitude < followDistance * followDistance) {
            if (playerMovementScript.playerState == PLAYERSTATE.IDLE) {
                SetFoxState(FOXSTATES.IDLE);
            }
            else if (playerMovementScript.playerState == PLAYERSTATE.WALK) {
                SetFoxState(FOXSTATES.WALK);
            }
            else if (playerMovementScript.playerState == PLAYERSTATE.RUN) {
                SetFoxState(FOXSTATES.RUN);
            }
            else if (playerMovementScript.playerState == PLAYERSTATE.SNEAK) {
                SetFoxState(FOXSTATES.SNEAK);
            }
        }
        else if (direction.sqrMagnitude < waitingDistance * waitingDistance) {
            if (playerMovementScript.playerState == PLAYERSTATE.IDLE) {
                SetFoxState(FOXSTATES.MOVE_TO_IDLE);
            }
            else if (playerMovementScript.playerState == PLAYERSTATE.WALK) {
                SetFoxState(FOXSTATES.WALK);
            }
            else if (playerMovementScript.playerState == PLAYERSTATE.RUN) {
                SetFoxState(FOXSTATES.RUN);
            }
        }
    }

    private void Follow()
    {
        speed = playerMovementScript.GetAcceleration() + baseSpeed;

        Quaternion rotation = Quaternion.LookRotation(direction, transform.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.1f);

        if (direction.sqrMagnitude > waitingDistance * waitingDistance) {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetToFollow.position, step);
        }
    }

    private void MoveToIdle()
    {
        objectivePosition = objective.transform.position - targetToFollow.position;
        objectivePosition = Vector3.Normalize(objectivePosition);

        if (direction.sqrMagnitude < waitingDistance * waitingDistance) {
            Vector3 waitingPosition = transform.position + (4f * new Vector3(objectivePosition.x, 0, objectivePosition.x));

            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, waitingPosition, step);
        }
        else {
            Quaternion newRotation = Quaternion.LookRotation(objectivePosition, transform.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, 0.1f);
            animator.SetBool("isWalking", false);
            animator.SetBool("isWaiting", true);
        }
    }

    private void SetFoxState(FOXSTATES newFoxState)
    {
        if(foxState != newFoxState) {
            foxState = newFoxState;
        }

        switch (foxState) {
            case FOXSTATES.IDLE:
                animator.SetBool("isWalking", false);
                animator.SetBool("isWaiting", true);
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
        }

        print(foxState);
    }
}
