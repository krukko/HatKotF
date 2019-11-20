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

        PrintRow();
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
                string sqlQuery = "SELECT " + column + " FROM EmotionListNew WHERE field2 = " + row;

                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //Gives column's name/number
                        string meToo = reader.GetName(0);

                        //Gives cells content. Use GetInt32 if you need an integer
                        string toPrint = reader.GetString(0);
                        Debug.Log(toPrint + " I am result");
                    }
                    //Close connection to Database
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
    }

    public void PrintRow()
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                int row = 3;

                //string sqlQuery = "SELECT " + 4 + " FROM EmotionListNew WHERE field1 = " + 23;
                string sqlQuery = "SELECT field4 FROM EmotionListNew WHERE field2 = " + row;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string returnedRow = reader.GetString(0);
                        Debug.Log(returnedRow);
                    }

                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
    }

}
