using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CATSTATES { SIT, WALK, RUN }
public class CatBehavior : MonoBehaviour
{
    public float waitTime, walkingSpeed, runningSpeed;
    private float speed;
    private float[] catSpeeds = new float[2];

    public int wayPointAmount,
               waypointRange; // how far from each other waypoints are
    private int nextWaypoint, currentWaypoint;


    private bool isMoving = false;

    public Transform waypointMarker;
    public Transform startingPoint;

    private Animator catAnimator;
    private Vector3[] waypointPositions;

    private CATSTATES catState;


    private void Awake()
    {
        catAnimator = GetComponent<Animator>();

        catSpeeds[0] = walkingSpeed;
        catSpeeds[1] = runningSpeed;
        waypointPositions = new Vector3[wayPointAmount];
        waypointPositions[0] = startingPoint.position;
        transform.position = new Vector3(waypointPositions[0].x, 0, waypointPositions[0].z);

        SetWaypoints();
        SetState(CATSTATES.SIT);
        SetAnimations();
        FindNextWaypoint();
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, waypointPositions[currentWaypoint]) >= 2)
        {
            Move();
        }
        else
        {
            SetState(CATSTATES.SIT);
            isMoving = false;
            Invoke("FindNextWaypoint", waitTime);
        }
    }

    private void SetSpeed()
    {
        int choice = Random.Range(0, catSpeeds.Length);

        speed = catSpeeds[choice];

        switch (choice)
        {
            case 0:
                SetState(CATSTATES.WALK);
                break;
            case 1:
                SetState(CATSTATES.RUN);
                break;
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypointPositions[currentWaypoint], Time.deltaTime * speed);
        transform.LookAt(waypointPositions[currentWaypoint]);
    }
    private void FindNextWaypoint()
    {
        if (!isMoving)
        {
            isMoving = true;
            nextWaypoint = Random.Range(0, waypointPositions.Length - 1);

            if (nextWaypoint != currentWaypoint)
            {
                currentWaypoint = nextWaypoint;
            }
            else
            {
                return;
            }

            SetSpeed();
        }
    }

    private void SetState(CATSTATES newState)
    {
        if (catState != newState)
        {
            catState = newState;
            SetAnimations();
        }
        else
            return;
    }

    private void SetAnimations()
    {
        switch (catState)
        {
            case CATSTATES.SIT:
                catAnimator.SetBool("isWalking", false);
                catAnimator.SetBool("isRunning", false);
                catAnimator.SetBool("isWaiting", true);
                break;
            case CATSTATES.WALK:
                catAnimator.SetBool("isWaiting", false);
                catAnimator.SetBool("isWalking", true);
                catAnimator.SetBool("isRunning", false);
                break;
            case CATSTATES.RUN:
                catAnimator.SetBool("isWalking", true);
                catAnimator.SetBool("isRunning", true);
                catAnimator.SetBool("isWaiting", false);
                break;
        }
    }

    private void SetWaypoints()
    {
        for (int i = 1; i < waypointPositions.Length; i++)
        {
            waypointPositions[i] = Random.insideUnitSphere * waypointRange + startingPoint.position;
            waypointPositions[i].y = 0;
        }

        currentWaypoint = 0;
    }
}
