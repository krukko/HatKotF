using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum FOXSTATES
{
    IDLE, WALK, RUN, SNEAK, MOVE_TO_IDLE, DODGE
}

public class FoxMovement : MonoBehaviour
{
    private float speed;
    public float sneakAnimationSpeed, walkAnimationSpeed;
    public float baseSpeed;
    public float followDistance;
    public float waitingDistance;
    public float evadeDistance;

    Vector3 direction;
    public Vector3 offset;
    private Vector3 objectivePosition;
    private bool isFollowing = false, isFleeing = false;

    public LayerMask collisionMask, fleeFromMask;
    public Transform targetToFollow;
    public GameObject objective; // objective of the player
    private Animator animator;

    public FOXSTATES foxStates;
   
    PlayerMovement playerMovementScript;

    private void Awake()
    {
        playerMovementScript = targetToFollow.GetComponentInParent<PlayerMovement>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        direction = targetToFollow.position - transform.position - offset;

        if (direction.sqrMagnitude > followDistance*followDistance) {
            isFollowing = true;
        }

        if (direction.sqrMagnitude < waitingDistance*waitingDistance) {
            isFollowing = false;
        }

        if (isFollowing) {
            animator.SetBool("isWaiting", false);
            Follow();
        }
        else
        {
            foxStates = FOXSTATES.IDLE;
            Idle();
        }
    }

    private void Follow()
    {
        speed = playerMovementScript.GetAcceleration() + baseSpeed;

        if (speed == (playerMovementScript.walkingSpeed + baseSpeed) || speed == (playerMovementScript.slowWalkSpeed + baseSpeed)) {
            animator.SetBool("isWalking", true);
            animator.SetBool("isRunning", false);
            animator.SetFloat("speedMultiplier", walkAnimationSpeed);
            foxStates = FOXSTATES.WALK;            
        }

        if (Input.GetKey(KeyCode.LeftShift)) {
            animator.SetBool("isWalking", true);
            animator.SetBool("isRunning", true);

            foxStates = FOXSTATES.RUN;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift)) {
            animator.SetBool("isRunning", false);
            foxStates = FOXSTATES.WALK;
        }

        if (Input.GetKey(KeyCode.LeftControl)) {
            animator.SetFloat("speedMultiplier", sneakAnimationSpeed);
            foxStates = FOXSTATES.SNEAK;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            foxStates = FOXSTATES.WALK;
        }

        Quaternion rotation = Quaternion.LookRotation(direction, transform.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.1f);

        if (direction.sqrMagnitude > waitingDistance*waitingDistance) {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetToFollow.position - offset, step);
        }
    }

    private void Idle()
    {
        objectivePosition = objective.transform.position - (targetToFollow.position - offset);
        objectivePosition = Vector3.Normalize(objectivePosition);  

        if (direction.sqrMagnitude < waitingDistance * waitingDistance) {
            Vector3 waitingPosition = transform.position + (4f * new Vector3(objectivePosition.x, 0, objectivePosition.x));

            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, waitingPosition, step);
        }
        else {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(objectivePosition), 0.1f);
            animator.SetBool("isWalking", false);
            animator.SetBool("isWaiting", true);
        }
    } 
}
