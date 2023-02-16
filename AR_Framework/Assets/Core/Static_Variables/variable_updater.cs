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
    private database_query_handler db_handler;

    void Awake()
    {
        this.db_handler = new database_query_handler();
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
        for(int val = 0; val < static_variables.robot.Count; val++)
        {
            string sql = $"SELECT ip_addr, port, name FROM robots WHERE id = {static_variables.robot[val].id}";
            MySqlDataReader rdr = this.db_handler.query(sql);

            while (rdr.Read())
                { //update static variables
                    static_variables.robot[val].ros_connection.RosIPAddress = rdr[0].ToString(); 
                    static_variables.robot[val].ros_connection.RosPort = (int)rdr[1];                    
                    static_variables.robot[val].name = rdr[2].ToString();

                }
            
            rdr.Close();

        }
    }

}
