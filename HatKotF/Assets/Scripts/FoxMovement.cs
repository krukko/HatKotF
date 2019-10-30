using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FoxMovement : MonoBehaviour
{
    public float followSpeed;
    public float targetDistance;
    public float followDistance;
    public float waitingDistance;

    public LayerMask collisionMask;
    public Transform targetToFollow;
    private Rigidbody rBod;
    private CapsuleCollider col;

    private void Start()
    {
        col = GetComponent<CapsuleCollider>();
        rBod = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(!IsGrounded())
        {

        }

        if (targetToFollow)
        {
            targetDistance = Vector3.Distance(transform.position, targetToFollow.position);

            if (targetDistance > followDistance)
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
        Vector3 difference = transform.position - targetToFollow.position;
        float mult = followDistance / difference.magnitude;

        transform.position -= difference;
        transform.position += difference * mult;

        Vector3 direction = targetToFollow.position - transform.position;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.1f);
    }

    private void Idle()
    {
        // check direction of destination

        // calculate line from player to destination

        // move fox to line at given distance from player

        // sit down and idle
    }

    private bool IsGrounded()
    {
        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x, col.bounds.min.y - 0.1f, col.bounds.center.z), 0.2f, collisionMask);
    }
}
