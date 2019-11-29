using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CATSTATES { SIT, WALK, RUN, SIT_DOWN}
public class CatBehavior : MonoBehaviour
{
    private Vector3[] waypointPositions;
    private int nextWaypoint, currentWaypoint, previousWaypoint;

    public int wayPointAmount;

    public float walkingSpeed, runningSpeed;

    public Transform waypointMarker;
    public Transform startingPoint;

    private void Start()
    {
        waypointPositions = new Vector3[wayPointAmount];
        waypointPositions[0] = startingPoint.position;
        SetWaypoints();

        currentWaypoint = 0;

        Debug.Log("cat sits");
    }

    private void Update()
    {
        FindNextWaypoint();

        if(currentWaypoint != previousWaypoint)
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

    private void SetState()
    {

    }

    private void SetAnimations()
    {

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
        Debug.Log("move to currentWaypoint");
    }

    private IEnumerator Wait()
    {
        return null;
    }
}
