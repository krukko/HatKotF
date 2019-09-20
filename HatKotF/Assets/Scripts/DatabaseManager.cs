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
                string sqlQuery = "SELECT * FROM EmotionListFull";
                //string sqlQueryTwo = "SELECT field5 FROM EmotionListFull WHERE field1 = '93'";

                dbCmd.CommandText = sqlQuery;
                //dbCmd.CommandText = sqlQueryTwo;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //Debug.Log(reader.GetString(0) + " " + reader.GetString(4));
                        ReadSingleRow((IDataRecord)reader);
                        //Debug.Log("I'm working, I just don't understand.");
                    }

                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
    }

    private static void ReadSingleRow(IDataRecord record)
    {
        //Make a list that contains all of these List(string ID, string dialogue)
        Debug.Log(String.Format("{0}, {1}", record[0], record[4]));
    }
}
