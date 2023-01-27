/*
Brennan Miller-Klugman
12/1/22

Updates static variables (from database) on a timer

References:
https://docs.unity3d.com/ScriptReference/MonoBehaviour.InvokeRepeating.html
https://dev.mysql.com/doc/connector-net/en/connector-net-tutorials-sql-command.html
https://github.com/freseco/Unity3d_MySQL_Client
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySqlConnector;
using Unity.Robotics.ROSTCPConnector;
using UnityEngine.UIElements;
using UnityEngine.Networking;

public class variable_updater : MonoBehaviour
{
    public GameObject scripts;

    void Awake()
    {
        InvokeRepeating("refresh", 5f, 30f); //repeat every 30 seconds
    }

    void Start(){
        for(int val = 0; val < static_variables.robot.Count; val++){ //create new ros connections for each robot
            ROSConnection ros_con = scripts.AddComponent<ROSConnection>();
            ros_con.ShowHud = false;
            static_variables.robot[val].ros_connection = ros_con;
        }
        refresh();
        scripts.GetComponent<dynamic_menu>().create();
    }
    
    public void refresh()
    {
        string connStr = @"server=98.229.202.174;port=3307;user=root;password=NveQlG8bKp89hPWMdhdC6jBnd;database=ar;";
        MySqlConnection conn = new MySqlConnection(connStr);
        conn.Open();

        for(int val = 0; val < static_variables.robot.Count; val++)
        {
            string sql = $"SELECT ip_addr, port, twist_topic, base_dof, twist_speed, name, multi_target_dataset, multi_target_name, affect_topic FROM robots WHERE id = {static_variables.robot[val].id}";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
                { //update static variables
                    static_variables.robot[val].ros_connection.RosIPAddress = rdr[0].ToString(); 
                    static_variables.robot[val].ros_connection.RosPort = (int)rdr[1];                    
                    static_variables.robot[val].name = rdr[5].ToString();

                }
            
            rdr.Close();

        }
    }

}
