using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class FoxMovement : MonoBehaviour
{
    public float sneakAnimationSpeed, walkAnimationSpeed;
    public float baseSpeed, evadeSpeed, evadeDistance;
    public float followDistance;
    public float waitingDistance;
    private float speed;
    private float distanceToTarget;

    public LayerMask layerMask;
    public Vector3 offset;
    private Vector3 objectivePosition, followTargetPosition, evadeTargetPosition;

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
        followTargetPosition = targetToFollow.position;
    }

    private void Update()
    {
        distanceToTarget = Vector3.Distance(playerTransform.position, transform.position);

        if (playerMovementScript.GetPlayerState() == PLAYERSTATE.IDLE)
        {
            if (distanceToTarget > waitingDistance)
            {
                SetFoxState(FOXSTATES.MOVE_TO_IDLE);
            }
            else
            {
                SetFoxState(FOXSTATES.IDLE);
            }
        }
        else if (playerMovementScript.GetPlayerState() == PLAYERSTATE.WALK)
        {
            SetFoxState(FOXSTATES.WALK);
        }
        else if (playerMovementScript.GetPlayerState() == PLAYERSTATE.RUN)
        {
            SetFoxState(FOXSTATES.RUN);
        }
        else if (playerMovementScript.GetPlayerState() == PLAYERSTATE.SNEAK)
        {
            SetFoxState(FOXSTATES.SNEAK);
        }
        else if (playerMovementScript.GetPlayerState() == PLAYERSTATE.FOLLOW)
        {
            if (playerMovementScript.GetPlayerState() != PLAYERSTATE.IDLE)
            {
                SetFoxState(FOXSTATES.EVADE);
            }
            else
            {
                SetFoxState(FOXSTATES.IDLE);
            }
        }

        SetAnimations();

        if (foxState == FOXSTATES.WALK || foxState == FOXSTATES.RUN || foxState == FOXSTATES.SNEAK)
        {
            if(evadeTargetPosition != Vector3.zero)
            {
                evadeTargetPosition = Vector3.zero;
                Vector3 newOffset = playerTransform.rotation * offset;
                followTargetPosition = playerTransform.position + newOffset;
                targetToFollow.position = followTargetPosition;
            }

            Follow();
        }
        else if (foxState == FOXSTATES.MOVE_TO_IDLE)
        {
            MoveToIdle();
        }
        else if (foxState == FOXSTATES.EVADE)
        {
            Evade();
        }
    }

    private void SetAnimations()
    {
        switch (foxState)
        {
            case FOXSTATES.IDLE:
                animator.SetBool("isWalking", false);
                animator.SetBool("isRunning", false);
                animator.SetBool("isWaiting", true);
                break;
            case FOXSTATES.WALK:
                animator.SetBool("isWalking", true);
                animator.SetBool("isRunning", false);
                animator.SetFloat("speedMultiplier", walkAnimationSpeed);
                animator.SetBool("isWaiting", false);
                break;
            case FOXSTATES.RUN:
                animator.SetBool("isWalking", true);
                animator.SetBool("isRunning", true);
                animator.SetBool("isWaiting", false);
                break;
            case FOXSTATES.SNEAK:
                animator.SetBool("isWaiting", false);
                animator.SetBool("isWalking", true);
                animator.SetBool("isRunning", false);
                animator.SetFloat("speedMultiplier", sneakAnimationSpeed);
                break;
            case FOXSTATES.MOVE_TO_IDLE:
                animator.SetBool("isWaiting", true);
                animator.SetBool("isWalking", true);
                animator.SetBool("isRunning", false);
                break;
            case FOXSTATES.EVADE:
                animator.SetBool("isWalking", true);
                animator.SetBool("isRunning", true);
                animator.SetBool("isWaiting", false);
                animator.SetFloat("speedMultiplier", walkAnimationSpeed);
                break;
        }
    }

    private void Follow()
    {
        if(foxState != FOXSTATES.EVADE)
        {
            speed = playerMovementScript.GetAcceleration() + baseSpeed;
        }
        else
        {
            speed = evadeSpeed;
        }

        transform.LookAt(targetToFollow);

        if (Vector3.Distance(transform.position, targetToFollow.position) > waitingDistance)
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

        if (distanceToTarget > waitingDistance && distanceToTarget < followDistance)
        {
            transform.position = waitingPosition;
            transform.LookAt(waitingPosition);
        }
        else
        {
            SetFoxState(FOXSTATES.IDLE);
            SetAnimations();
        }
    }

    private void Evade()
    {
        if (evadeTargetPosition == Vector3.zero)
        {
            Vector3 objectiveDirection = (transform.position - playerTransform.position).normalized;
            Vector3 evadePosition = transform.position + (objectiveDirection * evadeDistance);

            Ray ray = new Ray(targetToFollow.position + Vector3.up * 50, Vector3.down);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                evadePosition.y = hit.point.y + 2;
            }

            evadeTargetPosition = evadePosition;
            targetToFollow.position = evadeTargetPosition;
        }

        Follow();
    }

    private void SetFoxState(FOXSTATES newFoxState)
    {
        if (foxState != newFoxState)
        {
            foxState = newFoxState;
        }
    }
}
