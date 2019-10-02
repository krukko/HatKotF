using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public LayerMask playerMask;
    public float smoothStep = 10f;
    public Vector3 offset;

    private Camera SpringCamera;

    public float Stiffness = 1800.0f;
    public float Damping = 600.0f;
    public float Mass = 50.0f;
    public Vector3 DesiredOffset = new Vector3(0.0f, 3.5f, -4.0f);
    public Vector3 LookAtOffset = new Vector3(0.0f, 3.1f, 0.0f);

    private Vector3 desiredPosition = Vector3.zero;
    private Vector3 cameraVelocity = Vector3.zero;

    private void Start()
    {
        SpringCamera = Camera.main;
    }


    private void LateUpdate()
    {
        SpringFollow();

        //float currentAngle = transform.eulerAngles.y;
        //float desiredAngle = target.transform.eulerAngles.y;
        //Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);

        //Vector3 desiredPosition = target.position + offset;
        //Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothStep * Time.deltaTime);

        //transform.position = smoothedPosition;

        //transform.LookAt(target.transform);
    }

    //TODO: Clamp flipping when going backwards
    //TODO: Clamp side rotation

    private void SpringFollow()
    {
        Vector3 stretch = SpringCamera.transform.position - desiredPosition;
        Vector3 force = -Stiffness * stretch - Damping * cameraVelocity;
        Vector3 acceleration = force / Mass;

        cameraVelocity += acceleration * Time.deltaTime;

        SpringCamera.transform.position += cameraVelocity * Time.deltaTime;

        //Set target's position to matrix
        Matrix4x4 CamMat = new Matrix4x4();
        CamMat.SetRow(0, new Vector4(-target.forward.x, -target.forward.y, -target.forward.z));     
        CamMat.SetRow(1, new Vector4(target.up.x, target.up.y, target.up.z));                       
        Vector3 modifyRight = Vector3.Cross(CamMat.GetRow(1), CamMat.GetRow(0));                    //get cross product of player's -forward and y-axis = players right
        CamMat.SetRow(2, new Vector4(modifyRight.x, modifyRight.y, modifyRight.z));                 


        //Set desired position and desired lookAt position
        desiredPosition = target.position + TransformNormal(DesiredOffset, CamMat);
        Vector3 lookAt = target.position + TransformNormal(LookAtOffset, CamMat);

        SpringCamera.transform.LookAt(lookAt, target.up);

        print("camera position: " + SpringCamera.transform.position.ToString());
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
    private void CheckCollision()
    {
        //behind camera

        //left

        //right

        //up

        //down
    }
}
