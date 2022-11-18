using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySqlConnector;
using Unity.Robotics.ROSTCPConnector;

public static class static_variables
{
    public static List<int> id = new List<int>();

    public static List<ROSConnection> ros_connection = new List<ROSConnection>();

    public static List<string> ip = new List<string>();
    public static List<string> twist_topic = new List<string>();

    public static List<int> port = new List<int>();
    public static List<int> base_dof = new List<int>();
    public static List<int> twist_speed = new List<int>();

}
