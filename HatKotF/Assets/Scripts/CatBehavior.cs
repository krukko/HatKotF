using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CATSTATES { SIT, WALK, RUN, SIT_DOWN}
public class CatBehavior : MonoBehaviour
{
    private Vector3[] randomMovementPositions;

    public int wayPointAmount;
    public Transform waypointMarker;
    public Transform startingPoint;

    private void Start()
    {
        randomMovementPositions = new Vector3[wayPointAmount];
        randomMovementPositions[0] = startingPoint.position;
        SetWaypoints();
    }

    private void SetWaypoints()
    {
        for (int i = 1; i < randomMovementPositions.Length; i++)
        {
            randomMovementPositions[i] = Random.insideUnitSphere * 50 + startingPoint.position;
            randomMovementPositions[i].y =5;
            print("Position " + i + " :" + randomMovementPositions[i]);

            Instantiate(waypointMarker, randomMovementPositions[i], Quaternion.identity);
        }
    }
}
