using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FoxMovement : MonoBehaviour
{
    public float followSpeed;
    public float fallSpeed;
    public float targetDistance;
    public float followDistance;
    public float waitingDistance;

    public Vector3 desiredOffset;
    public Vector3 oldTargetPosition;

    public LayerMask layerMask;

    public Transform targetToFollow;


    private void Update()
    {
        if(!IsGrounded())
        {
           //fall down
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
        // get circle area around player 

        // check direction of destination

        // move towards destination
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 0.5f, layerMask);
    }
}
