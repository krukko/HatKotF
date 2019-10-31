using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FoxMovement : MonoBehaviour
{
    private float speed;
    public float baseSpeed;

    public float followDistance;
    public float waitingDistance;
    private float targetDistance;

    public LayerMask collisionMask;
    PlayerMovement playerMovementScript;

    public Transform targetToFollow;
    public GameObject objective; // objective of the player

    private Vector3 objectivePosition;
    private bool isFollowing = false;

    private void Awake()
    {
        playerMovementScript = targetToFollow.GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (targetToFollow)
        {
            targetDistance = Vector3.Distance(transform.position, targetToFollow.position);

            if (targetDistance > followDistance)
            {
                isFollowing = true;
            }
        
            if(targetDistance < waitingDistance)
            {
                isFollowing = false;
            }

            if (isFollowing)
            {
                Follow();
            }
            else
            {
                Idle();
            }
        }
    }

    private void Follow()
    {
        speed = playerMovementScript.GetAcceleration() + baseSpeed;

        Vector3 direction = targetToFollow.position - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.1f);

        if(targetDistance > waitingDistance)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetToFollow.position, step);
        }
    }

    private void Idle()
    {
        objectivePosition = objective.transform.position - targetToFollow.position;
        objectivePosition = Vector3.Normalize(objectivePosition);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(objectivePosition), 0.1f);

        if (targetDistance < waitingDistance)
        {
            Vector3 waitingPosition = transform.position + (4f * new Vector3(objectivePosition.x, 0, objectivePosition.x));

            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, waitingPosition, step);
        }
    }
}
