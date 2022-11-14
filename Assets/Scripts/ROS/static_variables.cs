using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySqlConnector;
using Unity.Robotics.ROSTCPConnector;

public class static_variables : MonoBehaviour
{
    public static List<int> id = new List<int>();

    public static List<ROSConnection> ros_connection = new List<ROSConnection>();

    public static List<string> ip = new List<string>();
    public static List<string> twist_topic = new List<string>();

    public static List<int> port = new List<int>();
    public static List<int> base_dof = new List<int>();
    public static List<int> twist_speed = new List<int>();

    void Awake() //from https://docs.unity3d.com/ScriptReference/MonoBehaviour.InvokeRepeating.html
    {
        InvokeRepeating("refresh", 1.0f, 30f); //repeat every 30 seconds
    }
    
    public void refresh()
    {
        string connStr = @"server=98.229.202.174;port=3307;user=root;password=NveQlG8bKp89hPWMdhdC6jBnd;database=ar;";
        MySqlConnection conn = new MySqlConnection(connStr);
        conn.Open();

        foreach(int val in id)
        {
            string sql = $"SELECT ip_addr, port, twist_topic, base_dof, twist_speed FROM robots WHERE id = {val}";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
                { //update vars
                    ros_connection[val].m_RosIPAddress = rdr[0].ToString(); //update ROS Connection
                    ros_connection[val].m_RosPort = (int)rdr[1];

                    twist_topic[val] = rdr[2].ToString(); //update database info
                    base_dof[val] = (int)rdr[3];
                    twist_speed[val] = (int)rdr[4]; 
                }
            
            rdr.Close();
        }
    }

}
