using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameSaveManager : MonoBehaviour
{
    public static GameSaveManager instance;

    public PlayerPosition playerPosition;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != null)
        {
            Destroy(this);
        }

        DontDestroyOnLoad(this);
    }

    public void SaveGame()
    {
        if(!IsSaveFile())
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save");
        }

        if(!Directory.Exists(Application.persistentDataPath + "/game_save/character_data"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save/character_data");
        }

        BinaryFormatter bf = new BinaryFormatter();
        //FileStream
    }

    public bool IsSaveFile()
    {
        return Directory.Exists(Application.persistentDataPath + "/game_save");
    }
}
