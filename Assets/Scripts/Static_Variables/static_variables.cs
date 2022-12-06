/*
Brennan Miller-Klugman
12/1/22

Holds static variables across scenes

Referances:
https://www.w3schools.com/cs/cs_properties.php
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySqlConnector;
using Unity.Robotics.ROSTCPConnector;
using Vuforia;

public class data 
{
    public ROSConnection ros_connection {get; set;}

    public int id { get; set; } = 0;
    public string name { get; set; } = "";
    public string twist_topic { get; set; } = "";
    public int base_dof { get; set; } = 0;
    public float twist_speed { get; set; } = 0;
    public string multi_target_dataset { get; set; } = "";
    public string multi_target_name { get; set; } = "";

    public MultiTargetBehaviour multi_target_behavior { get; set; }

    public data(int id, ROSConnection con, string ip_addr, int port)
    {
        this.id = id;
        this.ros_connection = con;
        this.ros_connection.RosIPAddress = ip_addr;
        this.ros_connection.RosPort = port;
        this.ros_connection.ShowHud = false;
    }
}

public static class static_variables
{
    public static List<data> robot = new List<data>();
}
