using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class FoxMovement : MonoBehaviour
{
#region PUBLIC_VARIABLES

    public float sneakAnimationSpeed,
                 walkAnimationSpeed,        //speed walk animation plays
                 evadeSpeed,                //speed fox uses when avading player
                 evadeDistance,             //evade target distance
                 followDistance,            //distance when fox starts following
                 waitingDistance;           //distance fox stays idle before starting to move

    public LayerMask layerMask;

    public Vector3 offset;

    public Transform targetToFollow,        //empty game object which fox follows; child of player
                     playerTransform;       //player's transform

    public GameObject objective;            //objective towards which player should move
#endregion


#region PRIVATE_VARIABLES

    private float speed,
                  distanceToPlayer,
                  distanceToTarget;

    private Vector3 objectivePosition,
                    followTargetPosition,
                    evadeTargetPosition;

    private Animator animator;

    private FOXSTATES foxState;

    PlayerMovement playerMovementScript;
#endregion


    private void Awake()
    {
        playerMovementScript = playerTransform.GetComponent<PlayerMovement>();
        playerMovementScript.SetFollower(transform);
        animator = GetComponentInChildren<Animator>();

        targetToFollow.position = playerTransform.position + offset;
        followTargetPosition = targetToFollow.position;
    }

    private void Update()
    {
        distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);
        distanceToTarget = Vector3.Distance(targetToFollow.position, transform.position);

        if (playerMovementScript.GetPlayerState() == PLAYERSTATE.IDLE)
        {
            if (distanceToPlayer > waitingDistance)
            {
                SetFoxState(FOXSTATES.MOVE_TO_IDLE);
            }
            else
            {
                SetFoxState(FOXSTATES.IDLE);
            }
        }

        if (distanceToTarget >= waitingDistance)
        {          
            if (playerMovementScript.GetPlayerState() == PLAYERSTATE.WALK)
            {
                SetFoxState(FOXSTATES.WALK);
            }
            else if (playerMovementScript.GetPlayerState() == PLAYERSTATE.MOVE_BACK)
            {
                print("player moving backwards");
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
        }
        else if(distanceToPlayer < waitingDistance)
        {
            SetFoxState(FOXSTATES.IDLE);
        }

        SetAnimations();

        if (foxState == FOXSTATES.WALK || foxState == FOXSTATES.RUN || foxState == FOXSTATES.SNEAK)
        {
            SetTargetToFollowPosition();
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
        if (foxState != FOXSTATES.EVADE)
        {
            speed = playerMovementScript.GetAcceleration();
        }
        else
        {
            speed = evadeSpeed + playerMovementScript.GetAcceleration();
        }

        Ray ray = new Ray(targetToFollow.position + Vector3.up * 50, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            targetToFollow.position = new Vector3(targetToFollow.position.x, hit.point.y, targetToFollow.position.z);
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

        if (distanceToPlayer > waitingDistance && distanceToPlayer < followDistance)
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

            evadeTargetPosition = evadePosition;
            targetToFollow.position = evadeTargetPosition;
        }
        Follow();
    }

    private void SetTargetToFollowPosition()
    {
        print("setting targetposition");

        if (evadeTargetPosition != Vector3.zero)
        {
            if (playerMovementScript.GetRotationDirection())
            {
                evadeTargetPosition = Vector3.zero;
                Vector3 newOffset = playerTransform.rotation * -offset;
                followTargetPosition = playerTransform.position + newOffset;
                targetToFollow.position = followTargetPosition;
            }
            else
            {
                evadeTargetPosition = Vector3.zero;
                Vector3 newOffset = playerTransform.rotation * offset;
                followTargetPosition = playerTransform.position + newOffset;
                targetToFollow.position = followTargetPosition;
            }
        }
    }

    private void SetFoxState(FOXSTATES newFoxState)
    {
        if (foxState != newFoxState)
        {
            foxState = newFoxState;
        }
    }
}
