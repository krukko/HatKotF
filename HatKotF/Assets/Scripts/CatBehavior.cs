using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CATSTATES { SIT, WALK, RUN, SIT_DOWN}
public class CatBehavior : MonoBehaviour
{   
    private int nextWaypoint, currentWaypoint, previousWaypoint;
    public int wayPointAmount;

    public float waitTime;
    public float walkingSpeed, runningSpeed, speed;

    private bool isWaiting = false, isAtWaypoint = true;

    public Transform waypointMarker;
    public Transform startingPoint;

    private Animator catAnimator;
    private Vector3[] waypointPositions;
    public CATSTATES catState;

    private void Start()
    {
        catAnimator = GetComponent<Animator>();

        waypointPositions = new Vector3[wayPointAmount];
        waypointPositions[0] = startingPoint.position;
        SetWaypoints();

        currentWaypoint = 0;

        SetState(CATSTATES.SIT);
        SetAnimations();
        Debug.Log("cat sits starting: " + Time.time);
    }

    private void Update()
    {
        if(!isWaiting)
        {
            FindNextWaypoint();
        }

        if (currentWaypoint != previousWaypoint)
        {
            Move();
        }
    }

    private void SetWaypoints()
    {
        for (int i = 1; i < waypointPositions.Length; i++)
        {
            waypointPositions[i] = Random.insideUnitSphere * 50 + startingPoint.position;
            waypointPositions[i].y =5;
            print("Position " + i + " :" + waypointPositions[i]);

            Instantiate(waypointMarker, waypointPositions[i], Quaternion.identity);
        }
    }

    private void SetState(CATSTATES newState)
    {
        if (catState != newState)
            catState = newState;
        else
            return;
    }

    private void SetAnimations()
    {
        switch(catState)
        {
            case CATSTATES.SIT:
                catAnimator.SetBool("isWaiting", true);
                catAnimator.SetBool("isWalking", false);
                catAnimator.SetBool("isRunning", false);
                break;
            case CATSTATES.WALK:
                catAnimator.SetBool("isWaiting", false);
                catAnimator.SetBool("isWalking", true);
                catAnimator.SetBool("isRunning", false);
                break;
            case CATSTATES.RUN:
                catAnimator.SetBool("isWalking", true);
                catAnimator.SetBool("isRunning", true);
                break;
            case CATSTATES.SIT_DOWN:
                catAnimator.SetBool("isWaiting", true);
                catAnimator.SetBool("isWalking", false);
                catAnimator.SetBool("isRunning", false);
                break;
        }
    }

    private void FindNextWaypoint()
    {
        nextWaypoint = Random.Range(0, waypointPositions.Length-1);

        if(nextWaypoint != currentWaypoint)
        {
            previousWaypoint = currentWaypoint;
            currentWaypoint = nextWaypoint;
            Debug.Log("next waypoint to move to is: " + nextWaypoint.ToString());
        }
        else
        {
            Debug.Log("same waypoint as last one");
        }
    }

    private void Move()
    {
        SetState(CATSTATES.WALK);
        Debug.Log("move to currentWaypoint");

        if (Vector3.Distance(transform.position, waypointPositions[currentWaypoint]) > 1)
        {
            Vector3 directionNormalized = (waypointPositions[currentWaypoint] - transform.position).normalized;
            transform.position = transform.position + directionNormalized * speed * Time.deltaTime;
        }
        else
        {
            StartCoroutine(Wait());
        }
    }

    private IEnumerator Wait()
    {
        SetState(CATSTATES.SIT);
        isWaiting = true;
        Debug.Log("waiting before next waypoint");

        yield return new WaitForSeconds(waitTime);

        isWaiting = false;

        Debug.Log("waiting finished");
    }
}
