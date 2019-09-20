using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ReadColumns : MonoBehaviour
{

    public TextAsset emotiondata;
    public Text printHere;

    private char lineSep = '\n';
    private char fieldSep = ';';
    private ArrayList rowList = new ArrayList();


    public void Start()
    {
        ReadData();
    }

    private void ReadData()
    {
        string[] rows = emotiondata.text.Split('\n');
        
        for (int i = 0; i < rows.Length; i++)
        {
            foreach (string row in rows)
            {
                char delimiter = ';';
                string[] columns = row.Split(delimiter);
                foreach (string column in columns)
                {
                    Debug.Log(column);
                }
                
            }
            //Debug.Log(rows[i]);
        }
    }

    //for (int i = 1; i <rows.Length; i++)
    //{
    //    foreach (string row in rows)
    //    {
    //        rowList.Add(row);
    //        foreach (string printrow in rowList)
    //        {
    //            Debug.Log(printrow);
    //        }
    //    }
    //}


}
