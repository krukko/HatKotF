using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;

public class DatabaseManager : MonoBehaviour
{
    //Connects the program to the SQLite database
    private string connectionString;

    void Start()
    {
        //Tells the connection string which file is the database
        connectionString = "URI=file:" + Application.dataPath + "/EmotionDataFull.db";
        GetID();
    }

    //Function name irrelevant here
    private void GetID()
    {
        //For opening/closing the connection to the database
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                int column = UnityEngine.Random.Range(5, 7);
                int row = UnityEngine.Random.Range(2, 31);

                string sqlQuery = "SELECT " + column + " FROM EmotionListFull WHERE field1 = " + row;

                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Debug.Log("aaaaaaaaaaaaaaa");
                        string toPrint = reader.GetString(0);
                        Debug.Log(toPrint);
                        //Debug.Log(reader.GetString(0) + " " + reader.GetString(4));
                        //Debug.Log("I'm working, I just don't understand.");
                    }

                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
    }

}
