﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Transform cameraHelper;
    public LayerMask collisionMask;

    private Camera SpringCamera;

    public float Stiffness = 1800.0f;
    public float Damping = 600.0f;
    public float Mass = 50.0f;
    public float wallPush = 0.5f;
    public float rotationSpeed = 5f;
    private float desiredDistance;

    public Vector3 DesiredOffset = new Vector3(7.5f, 4.5f, 0f);
    public Vector3 defaultOffset;
    public Vector3 LookAtOffset = new Vector3(0.0f, 2f, 0.0f);
    private Vector3 desiredPosition = Vector3.zero;
    private Vector3 cameraVelocity = Vector3.zero;

    private bool isColliding = false;
    public bool rotateAroundPlayer = true;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        SpringCamera = Camera.main;

        cameraHelper.rotation = target.rotation;

        defaultOffset = DesiredOffset;
    }

    private void LateUpdate()
    {
        if (rotateAroundPlayer && Input.GetKey(KeyCode.Mouse1))
        {
            Quaternion cameraTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed, Vector3.up);

            DesiredOffset = cameraTurnAngle * DesiredOffset;
        }
        else DesiredOffset = defaultOffset;

        SpringFollow();

    }

    private void SpringFollow()
    {
        Vector3 stretch = SpringCamera.transform.position - desiredPosition; // how much camera "streches" when moving/stopping
        Vector3 force = -Stiffness * stretch - Damping * cameraVelocity;
        Vector3 acceleration = force / Mass;

        cameraVelocity += acceleration * Time.deltaTime;

        CheckCollision(SpringCamera.transform.position += cameraVelocity * Time.deltaTime);

        //Set cameraHelper's position to matrix
        Matrix4x4 CamMat = new Matrix4x4();
        CamMat.SetRow(0, new Vector4(-cameraHelper.forward.x, -cameraHelper.forward.y, -cameraHelper.forward.z));
        CamMat.SetRow(1, new Vector4(cameraHelper.up.x, cameraHelper.up.y, cameraHelper.up.z));
        Vector3 modifyRight = Vector3.Cross(CamMat.GetRow(1), CamMat.GetRow(0));                    //get cross product of cameraHelper's -forward and y-axis = right vector3
        CamMat.SetRow(2, new Vector4(modifyRight.x, modifyRight.y, modifyRight.z));

        //Set desired position and desired lookAt position
        desiredPosition = cameraHelper.position + TransformNormal(DesiredOffset, CamMat);
        Vector3 lookAt = cameraHelper.position + TransformNormal(LookAtOffset, CamMat);

        SpringCamera.transform.LookAt(lookAt, cameraHelper.up);

        desiredDistance = Vector3.Distance(target.position, DesiredOffset);
    }

    Vector3 TransformNormal(Vector3 normal, Matrix4x4 matrix)
    {
        //Get positions from matrix and change them into vector3 points to get new x, y and z axis
        Vector3 transformNormal = new Vector3();
        Vector3 axisX = new Vector3(matrix.m00, matrix.m01, matrix.m02);
        Vector3 axisY = new Vector3(matrix.m10, matrix.m11, matrix.m12);
        Vector3 axisZ = new Vector3(matrix.m20, matrix.m21, matrix.m22);

        //Get dot product of given normal and and axis to get desired position
        transformNormal.x = Vector3.Dot(normal, axisX);
        transformNormal.y = Vector3.Dot(normal, axisY);
        transformNormal.z = Vector3.Dot(normal, axisZ);

        return transformNormal;
    }

    //check camera collisions and push away from possible collision
    private void CheckCollision(Vector3 _returnPosition)
    {
        float desiredDistance = Vector3.Distance(cameraHelper.position, SpringCamera.transform.position); // what is distance between target and desired camera position

        //int stepCount = 4;                                                          // how many raycast "crosses" there will be - one more added later
        //float stepIncremental = desiredDistance / stepCount / 10;                   // distance between steps


        RaycastHit hit;
        Vector3 rayDir = SpringCamera.transform.position - cameraHelper.position;

        //Check if anything occluding player
        if (Physics.SphereCast(cameraHelper.position, 0.5f, rayDir, out hit, desiredDistance, collisionMask))
        {
            isColliding = true;

            Vector3 wallNormal = hit.normal * wallPush;
            Vector3 newPos = hit.point + wallNormal;

            Vector3 colDirection = cameraHelper.position - hit.point;
            Vector3 wallNormalDirection = cameraHelper.position - newPos;

            //transform.position = (hit.point - cameraHelper.position) * 0.8f + cameraHelper.position;
            // get positive or negative angle between colDirection vector and wallnormaldirection vector
            float angleToRotate = Vector3.SignedAngle(colDirection, wallNormalDirection, Vector3.up);
            angleToRotate = Mathf.Clamp(angleToRotate, -85, 85);

            if (angleToRotate < 0) angleToRotate -= 5;

            else if (angleToRotate > 0) angleToRotate += 5;

            else angleToRotate = 45;

            cameraHelper.Rotate(0, angleToRotate * Time.deltaTime * 20, 0);
        }
        else
        {
            WallCheck();

            cameraHelper.rotation = target.rotation;
            //for (int i = 0; i < stepCount + 1; i++)
            //{
            //    for (int j = 0; j < 4; j++)
            //    {
            //        Vector3 dir = Vector3.zero;
            //        Vector3 secondOrigin = target.position + rayDir * i * stepIncremental;

            //        switch (j)
            //        {
            //            case 0:
            //                dir = transform.up;
            //                break;
            //            case 1:
            //                dir = -transform.up;
            //                break;
            //            case 2:
            //                dir = transform.right;
            //                break;
            //            case 3:
            //                dir = -transform.right;
            //                break;
            //            default:
            //                break;
            //        }

            //        Debug.DrawRay(secondOrigin, dir * 1, Color.blue);

            //        if (Physics.Raycast(target.position, rayDir, out hit, desiredDistance, collisionMask))
            //        {
            //            transform.position = hit.point * wallPush;
            //        }
            //    }

            //}
        }
    }

    private void WallCheck()
    {
        Ray ray = new Ray(target.position, -target.forward);

        if (Physics.SphereCast(ray, 0.7f, desiredDistance, collisionMask))
        {
            isColliding = true;
        }
        else
        {
            isColliding = false;
        }
    }
}