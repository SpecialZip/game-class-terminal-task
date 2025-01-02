using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;

public class MySqlConnect : MonoBehaviour
{
    private MySqlConnection connection;
    private string server="127.0.0.1";
    private string port="3306";
    private string database="game_class";
    private string password="yunzhcjust4fun";
    private string uid="root";
    // Start is called before the first frame update
    void Start()
    {
        string connectionString = $"server={server};port={port};database={database};user={uid};password={password}";
        connection=new MySqlConnection(connectionString);

        try
        {
            connection.Open();
            Debug.Log("连接成功");
        }
        catch (MySqlException ex)
        {
            Debug.Log("连接失败："+ex.Message);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
