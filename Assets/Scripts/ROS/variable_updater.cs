using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySqlConnector;
using Unity.Robotics.ROSTCPConnector;
using UnityEngine.UIElements;

public class variable_updater : MonoBehaviour
{
    void Awake() //from https://docs.unity3d.com/ScriptReference/MonoBehaviour.InvokeRepeating.html
    {
        InvokeRepeating("refresh", 1.0f, 30f); //repeat every 30 seconds
    }
    
    public void refresh()
    {
        string connStr = @"server=98.229.202.174;port=3307;user=root;password=NveQlG8bKp89hPWMdhdC6jBnd;database=ar;";
        MySqlConnection conn = new MySqlConnection(connStr);
        conn.Open();

        foreach(int val in static_variables.id)
        {
            string sql = $"SELECT ip_addr, port, twist_topic, base_dof, twist_speed FROM robots WHERE id = {val}";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
                { //update vars
                    static_variables.ros_connection[val].RosIPAddress = rdr[0].ToString(); //update ROS Connection
                    static_variables.ros_connection[val].RosPort = (int)rdr[1];

                    static_variables.twist_topic[val] = rdr[2].ToString(); //update database info
                    static_variables.base_dof[val] = (int)rdr[3];
                    static_variables.twist_speed[val] = (int)rdr[4]; 
                }
            
            rdr.Close();
        }
    }

}
