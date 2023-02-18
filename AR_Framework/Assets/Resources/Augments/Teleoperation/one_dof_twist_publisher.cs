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

using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes;
using twistMsg = RosMessageTypes.Geometry.TwistMsg;
using vector = RosMessageTypes.Geometry.Vector3Msg;

public class one_dof_twist_publisher : MonoBehaviour
    {
    ROSConnection ros;
    public data robot;
    public string topicName;
    public float twist_speed;

    // The game object
    // Publish the cube's position and rotation every N seconds
    public float publishMessageFrequency = 0.1f;


    void Start()
    {   
        // get component from parent
        var setup_config = GetComponentInParent<setup_augment>();
        
        StartCoroutine(setup_config.read_config((JToken config_json) => {
            // start the ROS connection
            robot = setup_config.robot;
            topicName = (string)config_json["twist_topic"];
            twist_speed = (float)config_json["twist_speed"];

            ros = robot.ros_connection;
            ros.RegisterPublisher<twistMsg>(topicName);

            InvokeRepeating("publish_twist", 0.0f, publishMessageFrequency);
        }
        ));
    }

    private void publish_twist()
    {
        Gamepad gamepad = Gamepad.current;

        Vector2 input = gamepad.leftStick.ReadValue();

        vector linear = new vector(
            input.x * twist_speed,
            0.0,
            0.0
        );

        vector angular = new vector(
            0.0,
            0.0,
            input.y * twist_speed
        );

        twistMsg twist = new  twistMsg(
            linear,
            angular
        );

        // Finally send the message to server_endpoint.py running in ROS
        ros.Publish(topicName, twist);

    }
}

