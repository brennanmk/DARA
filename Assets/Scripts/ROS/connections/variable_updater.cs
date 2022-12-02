/*
Brennan Miller-Klugman
12/1/22

Updates static variables (from database) on a timer

References:
https://docs.unity3d.com/ScriptReference/MonoBehaviour.InvokeRepeating.html
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySqlConnector;
using Unity.Robotics.ROSTCPConnector;
using UnityEngine.UIElements;

public class variable_updater : MonoBehaviour
{
    public GameObject scripts;

    void Awake()
    {
        InvokeRepeating("refresh", 5f, 30f); //repeat every 30 seconds
    }

    void Start(){
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
            string sql = $"SELECT ip_addr, port, twist_topic, base_dof, twist_speed, name FROM robots WHERE id = {val}";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
                { //update vars
                    static_variables.robot[val].ros_connection.RosIPAddress = rdr[0].ToString(); //update ROS Connection
                    static_variables.robot[val].ros_connection.RosPort = (int)rdr[1];

                    static_variables.robot[val].twist_topic = rdr[2].ToString();
                    static_variables.robot[val].base_dof = (int)rdr[3];
                    static_variables.robot[val].twist_speed = (int)rdr[4]; 
                    
                    static_variables.robot[val].name = rdr[5].ToString();

                }
            
            rdr.Close();
        }
    }

}
