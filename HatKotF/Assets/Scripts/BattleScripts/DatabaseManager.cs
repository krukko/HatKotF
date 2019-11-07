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
                int column = UnityEngine.Random.Range(6, 8);
                int row = UnityEngine.Random.Range(2, 31);

                //Use field2 because that has the IDAuto!!!
                string sqlQuery = "SELECT " + column + " FROM EmotionListFull WHERE field2 = " + row;

                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //Gives column's name/number
                        string meToo = reader.GetName(0);

                        //Give's cells content. Use GetInt32 if you need an integer
                        string toPrint = reader.GetString(0);
                        Debug.Log(toPrint + " I am result");
                    }

                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
    }

}
