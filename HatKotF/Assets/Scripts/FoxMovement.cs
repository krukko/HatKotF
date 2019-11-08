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
    private float targetDistance;

    public Vector3 offset;
    private Vector3 objectivePosition;
    private bool isFollowing = false;

    public LayerMask collisionMask;
    public Transform targetToFollow;
    public GameObject objective; // objective of the player
    private Animator animator;

    PlayerMovement playerMovementScript;

    private void Awake()
    {
        playerMovementScript = targetToFollow.GetComponentInParent<PlayerMovement>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        CheckCollision();

        targetDistance = Vector3.Distance(transform.position, targetToFollow.position);

        if (targetDistance > followDistance)
        {
            isFollowing = true;
        }

        if (targetDistance < waitingDistance)
        {
            isFollowing = false;
        }

        if (isFollowing)
        {
            animator.SetBool("isWaiting", false);
            Follow();
        }
        else
        {
            Idle();
        }
    }

    private void Follow()
    {
        speed = playerMovementScript.GetAcceleration() + baseSpeed;

        if (speed == (playerMovementScript.walkingSpeed + baseSpeed) || speed == (playerMovementScript.slowWalkSpeed + baseSpeed)){
            animator.SetBool("isWalking", true);
            animator.SetBool("isRunning", false);
        }

        if (Input.GetKey(KeyCode.LeftShift)){
            animator.SetBool("isWalking", true);
            animator.SetBool("isRunning", true);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift)){
            animator.SetBool("isRunning", false);
        }

        if (Input.GetKey(KeyCode.LeftControl)){
            animator.SetFloat("speedMultiplier", sneakAnimationSpeed);
        }
        else{
            animator.SetFloat("speedMultiplier", walkAnimationSpeed);
        }

        Vector3 direction = targetToFollow.position - (transform.position - offset);
        if(direction != Vector3.zero){

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.1f);

            if (targetDistance > waitingDistance) {
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, targetToFollow.position + offset, step);

                Debug.DrawLine(transform.position, targetToFollow.position + offset, Color.green);
            }
        }     
    }

    private void Idle()
    {
        objectivePosition = objective.transform.position - targetToFollow.position;
        objectivePosition = Vector3.Normalize(objectivePosition);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(objectivePosition), 0.1f);

        if (targetDistance < waitingDistance){
            Vector3 waitingPosition = transform.position + (4f * new Vector3(objectivePosition.x, 0, objectivePosition.x));

            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, waitingPosition, step);
        }
        else{
            animator.SetBool("isWalking", false);
            animator.SetBool("isWaiting", true);
        }
    }

    private void CheckCollision()
    {


       
    }
}
