/*
Brennan Miller-Klugman
Last Edited: 10/2/22
based off of code samples from:
https://github.com/Unity-Technologies/Unity-Robotics-Hub/blob/main/tutorials/ros_unity_integration/publisher.md
https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/Actions.html
https://www.youtube.com/watch?v=zd75Jq37R60
*/

using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes;
using twistMsg = RosMessageTypes.Geometry.TwistMsg;
using vector = RosMessageTypes.Geometry.Vector3Msg;

public class one_dof_twist_publisher : MonoBehaviour
    {
    ROSConnection ros;
    public data robot;

    public string topicName;
    
    // The game object
    public InputAction moveAction;
    // Publish the cube's position and rotation every N seconds
    public float publishMessageFrequency = 0.5f;

    // Used to determine how much time has elapsed since the last message was published
    private float timeElapsed;

    void Start()
    {
        // start the ROS connection
        topicName = robot.twist_topic;

        ros = robot.ros_connection;
        ros.RegisterPublisher<twistMsg>(topicName);
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed > publishMessageFrequency)
        {
            Vector2 input = moveAction.ReadValue<Vector2>();

            vector linear = new vector(
                input.x * robot.twist_speed,
                0.0,
                0.0
            );

            vector angular = new vector(
                0.0,
                0.0,
                input.y * robot.twist_speed
            );

            twistMsg twist = new  twistMsg(
                linear,
                angular
            );

            // Finally send the message to server_endpoint.py running in ROS
            ros.Publish(topicName, twist);

            timeElapsed = 0;
        }
    }
}

