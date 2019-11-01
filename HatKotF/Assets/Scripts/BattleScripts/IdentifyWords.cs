﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IdentifyWords : MonoBehaviour
{
    public GameObject wordPrefab;
    public GameObject[] words;

    public void SpawnWords()
    {
        int spawnpointX = Random.Range(-300, 300);
        int spawnpointY = Random.Range(-250, 250);
        Vector3 spawnposition = new Vector3(spawnpointX, spawnpointY, 0);

        if (words == null)
        {
            words = GameObject.FindGameObjectsWithTag("IdentifyText");
        }
        GameObject.Instantiate(wordPrefab, spawnposition, Quaternion.identity);
    }
}
