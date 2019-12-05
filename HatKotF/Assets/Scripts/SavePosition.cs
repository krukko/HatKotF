using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePosition : MonoBehaviour
{
    public GameObject Player;

    public float positionX;
    public float positionY;
    public float positionZ;

    public float rotationX;
    public float rotationY;
    public float rotationZ;

    public void SavePos()
    {
        PlayerPrefs.SetFloat("PlayerX", Player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", Player.transform.position.y);
        PlayerPrefs.SetFloat("PlayerZ", Player.transform.position.z);
    }

    public void LoadPos()
    {
        transform.position = new Vector3(PlayerPrefs.GetFloat("PlayerX"), PlayerPrefs.GetFloat("PlayerY"), PlayerPrefs.GetFloat("PlayerZ"));
    }
        



    ////public void Awake()
    ////{
    ////    positionX = PlayerPrefs.GetFloat("MyPositionX");
    ////    positionY = PlayerPrefs.GetFloat("MyPositionY");
    ////    positionZ = PlayerPrefs.GetFloat("MyPositionZ");

    ////    rotationX = PlayerPrefs.GetFloat("MyRotationX");
    ////    rotationY = PlayerPrefs.GetFloat("MyRotationY");
    ////    rotationZ = PlayerPrefs.GetFloat("MyRotationZ");
    ////}

    //public void Start()
    //{   
    //    Vector3 savedPosition = new Vector3(PlayerPrefs.GetFloat("playerX"), PlayerPrefs.GetFloat("playerY"), PlayerPrefs.GetFloat("playerZ"));
    //    Player.transform.position = new Vector3(positionX, positionY, positionZ);
    //    //Player.transform.rotation = Quaternion.Euler(rotationX, rotationY, rotationZ);
    //}

    //public void Update()
    //{
    //    PlayerPrefs.SetFloat("playerX", Player.transform.position.x);
    //    PlayerPrefs.SetFloat("playerY", Player.transform.position.y);
    //    PlayerPrefs.SetFloat("playerZ", Player.transform.position.z);

    //    //PlayerPrefs.SetFloat("MyPositionX", transform.position.x);
    //    //PlayerPrefs.SetFloat("MyPositionY", transform.position.y);
    //    //PlayerPrefs.SetFloat("MyPositionZ", transform.position.z);

    //    //PlayerPrefs.SetFloat("MyRotationX", transform.eulerAngles.x);
    //    //PlayerPrefs.SetFloat("MyRotationY", transform.eulerAngles.y);
    //    //PlayerPrefs.SetFloat("MyRotationZ", transform.eulerAngles.z);
    //}

}

