using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FoxMovement : MonoBehaviour
{
    public float speed = 1.0f;
    public float followDistance;
    public float waitingDistance;

    private float targetDistance;
    private float oldTargetDistance;

    public LayerMask collisionMask;
    public Transform targetToFollow;
    private Rigidbody rBod;

    public GameObject destination;

    private Vector3 destinationPosition;
    private bool isWaiting = false;

    private void Start()
    {
        rBod = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (targetToFollow)
        {
            targetDistance = Vector3.Distance(transform.position, targetToFollow.position);


            if (targetDistance > followDistance)
            {
                oldTargetDistance = targetDistance;
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
        targetDistance = Vector3.Distance(transform.position, targetToFollow.position);

        Vector3 direction = targetToFollow.position - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.1f);

        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetToFollow.position, step);
    }

    private void Idle()
    {
        destinationPosition = destinationPosition - targetToFollow.position;

        destinationPosition = Vector3.Normalize(destinationPosition);

        Vector3 waitingPosition = targetToFollow.position + (5 * new Vector3(destinationPosition.x, 0, destinationPosition.x));

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(destinationPosition), 0.1f);

        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, waitingPosition, step);
    }
}
