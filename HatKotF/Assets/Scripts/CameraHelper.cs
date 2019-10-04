using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHelper : MonoBehaviour
{
    private float xMovement, yMovement, zMovement;
    public LayerMask layerMask;

    //TODO: check if there are colliding objects inside given area
    //TODO: rotate object to face away from occlusion

    //private void Update()
    //{
    //    Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2, layerMask);

    //    int i = 0;

    //    while(i<hitColliders.Length)
    //    {
    //        Debug.Log("hitcollider within area" + (i+1));

    //        i++;
    //    }
    //}
}
  