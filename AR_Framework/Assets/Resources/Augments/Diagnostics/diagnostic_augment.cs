/*
Brennan Miller-Klugman
Last Edited: 10/2/22
based off of code samples from:
https://github.com/Unity-Technologies/Unity-Robotics-Hub/blob/main/tutorials/ros_unity_integration/publisher.md
https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/Actions.html
https://www.youtube.com/watch?v=zd75Jq37R60
https://answers.unity.com/questions/1859009/new-input-system-how-to-get-left-stick-axis.html
*/

using UnityEngine;
using UnityEngine.InputSystem;
using Newtonsoft.Json.Linq;
using System.Collections;
using UnityEngine.Networking;
using Vuforia;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes;
using integer = RosMessageTypes.Std.Int16Msg;

public class diagnostic_augment : MonoBehaviour
    {
    ROSConnection ros;
    public data robot;
    public string battery_topic, cpu_usage_topic, cpu_temp_topic;
    public augment_cube battery, cpu_usage, cpu_temp;

    // The game object
    // Publish the cube's position and rotation every N seconds

    void Start()
    {   
        // get component from parent
        var setup_config = GetComponentInParent<setup_augment>();
        
        StartCoroutine(setup_config.read_config((JToken config_json) => {
            // start the ROS connection
            robot = setup_config.robot;
            ros = robot.ros_connection;

            battery_topic = (string)config_json["battery_topic"];
            cpu_usage_topic = (string)config_json["cpu_usage_topic"];
            cpu_temp_topic = (string)config_json["cpu_temp_topic"];

            ros.Subscribe<integer>(cpu_usage_topic, update_cpu_usage);
            ros.Subscribe<integer>(cpu_temp_topic, update_cpu_temp);
            ros.Subscribe<integer>(battery_topic, update_battery);

        }
        ));
    }



    void update_cpu_usage(integer data) //on subscribe, update the emoji based on message contents
    {

        if (data.data <= 20){
            cpu_usage.update_render(Resources.Load<Material>("Augments/Diagnostics/ux/Materials/1_bar"));
        } else if(data.data <= 40){
            cpu_usage.update_render(Resources.Load<Material>("Augments/Diagnostics/ux/Materials/2_bar"));
        } else if(data.data <= 60){
            cpu_usage.update_render(Resources.Load<Material>("Augments/Diagnostics/ux/Materials/3_bar"));
        } else if(data.data <= 80){
            cpu_usage.update_render(Resources.Load<Material>("Augments/Diagnostics/ux/Materials/4_bar"));
        } else if(data.data <= 100){
            cpu_usage.update_render(Resources.Load<Material>("Augments/Diagnostics/ux/Materials/5_bar"));
        }
    }

    void update_battery(integer data) //on subscribe, update the emoji based on message contents
    {

        if (data.data <= 20){
            battery.update_render(Resources.Load<Material>("Augments/Diagnostics/ux/Materials/1_bar"));
        } else if(data.data <= 40){
            battery.update_render(Resources.Load<Material>("Augments/Diagnostics/ux/Materials/2_bar"));
        } else if(data.data <= 60){
            battery.update_render(Resources.Load<Material>("Augments/Diagnostics/ux/Materials/3_bar"));
        } else if(data.data <= 80){
            battery.update_render(Resources.Load<Material>("Augments/Diagnostics/ux/Materials/4_bar"));
        } else if(data.data <= 100){
            battery.update_render(Resources.Load<Material>("Augments/Diagnostics/ux/Materials/5_bar"));
        }
    }

    void update_cpu_temp(integer data) //on subscribe, update the emoji based on message contents
    {

        if (data.data <= 20){
            cpu_temp.update_render(Resources.Load<Material>("Augments/Diagnostics/ux/Materials/1_bar"));
        } else if(data.data <= 40){
            cpu_temp.update_render(Resources.Load<Material>("Augments/Diagnostics/ux/Materials/2_bar"));
        } else if(data.data <= 60){
            cpu_temp.update_render(Resources.Load<Material>("Augments/Diagnostics/ux/Materials/3_bar"));
        } else if(data.data <= 80){
            cpu_temp.update_render(Resources.Load<Material>("Augments/Diagnostics/ux/Materials/4_bar"));
        } else if(data.data <= 100){
            cpu_temp.update_render(Resources.Load<Material>("Augments/Diagnostics/ux/Materials/5_bar"));
        }
    }
}
