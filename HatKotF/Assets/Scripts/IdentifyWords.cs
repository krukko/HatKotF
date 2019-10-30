using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IdentifyWords : MonoBehaviour
{
    //public int ID;
    //public string name;
    //public EmotionList emotionList;
    //public GameManager gameManager;

    //public GameObject kylla;
    //public GameObject tunnen;
    //public GameObject syntymasi;

    public GameObject wordPrefab;
    public GameObject[] words;

    // Start is called before the first frame update
    void Start()
    {
        SpawnWords();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnWords()
    {
        int spawnpointX = Random.Range(-300, 300);
        int spawnpointY = Random.Range(-250, 250);
        Vector3 spawnposition = new Vector3(spawnpointX, spawnpointY, 0);

        //if (words == null)
        //{
        //    words = GameObject.FindGameObjectsWithTag("IdentifyText");
        //}
            GameObject.Instantiate(wordPrefab, spawnposition, Quaternion.identity);
    }
}
