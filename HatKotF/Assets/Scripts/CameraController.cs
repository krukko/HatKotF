using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public LayerMask layerMask;

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

    private void Update()
    {
        CheckCollision();
    }

    private void LateUpdate()
    {
        SpringFollow();
    }

    //TODO: if moving backwards rise camera higher to see more ground (expand FoV if possible)
    //TODO: when looking up expand FoV (can player look up?)
    //TODO: swing camera away from possibly occluding object (tag objects to not swing away from)
    //TODO: look down when approaching cliffs?

    //TODO: change desired position according to players direction
    //
    // change x/z offset when moving left/right
    // expand desired offset when moving backwards
    // 

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
        float desiredDistance = Vector3.Distance(target.position, desiredPosition); // what is distance between target and desired camera position
        int stepCount = 4;                                                          // how many raycast "crosses" there will be - one more added later
        float stepIncremental = desiredDistance / stepCount / 10;                   // distance between steps

        RaycastHit hit;
        Vector3 rayDir = transform.position - target.position;

        Debug.DrawRay(target.position, rayDir, Color.red);

        //Check if anything occluding player
        if (Physics.Raycast(target.position, rayDir, out hit, desiredDistance, layerMask))
        {
            Debug.Log("player occluded");

            transform.position = hit.point;
        }
        else
        {
            // for each step draw raycast cross to check up, down and side occlusion
            for (int i = 0; i < stepCount + 1; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Vector3 dir = Vector3.zero;
                    Vector3 secondOrigin = target.position + rayDir * i * stepIncremental;

                    switch (j)
                    {
                        case 0:
                            dir = transform.up;
                            break;
                        case 1:
                            dir = -transform.up;
                            break;
                        case 2:
                            dir = transform.right;
                            break;
                        case 3:
                            dir = -transform.right;
                            break;
                        default:
                            break;
                    }

                    Debug.DrawRay(secondOrigin, dir * 1, Color.blue);

                    if (Physics.Raycast(target.position, rayDir, out hit, desiredDistance, layerMask))
                    {
                        transform.position = hit.point;
                    }
                }
            }

            // TODO: instead of transform.position change desiredOffset 

            // move camera to 1 step closer to player than hit point
            // move camera to other side if side ray hits something
            // move camera up or down if occlusion hit (tilt camera more?)
        }

    }
}
