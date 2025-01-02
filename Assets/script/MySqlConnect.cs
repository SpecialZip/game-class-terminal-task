using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;

public class MySqlConnect
{
    private static MySqlConnection connection;
    private static string server="127.0.0.1";
    private static string port="3306";
    private static string database="game_class";
    private static string password="yunzhcjust4fun";
    private static string uid="root";
    
    private static string connectionString = $"server={server};port={port};database={database};user={uid};password={password}";

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

    public static bool CheckAccountInDatabase(string account, string password)
    {
        using (connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT COUNT(*) FROM players WHERE name = @account AND password = @password";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@account", account);
                command.Parameters.AddWithValue("@password", password);
                object result = command.ExecuteScalar();
                int count = result!=null? Convert.ToInt32(result) :0;
                return count > 0;
            }
        }
    }
}
