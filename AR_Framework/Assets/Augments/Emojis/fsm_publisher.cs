/*
Brennan Miller-Klugman
Last Edited: 10/2/22
based off of code samples from:
https://github.com/Unity-Technologies/Unity-Robotics-Hub/blob/main/tutorials/ros_unity_integration/publisher.md
https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/Actions.html
*/

using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes;
using twistMsg = RosMessageTypes.Geometry.TwistMsg;
using boolmsg = RosMessageTypes.Std.BoolMsg;

public class fsm_publisher : MonoBehaviour
    {
    ROSConnection ros;
    public string topicName = "start_fsm";

    private float timeElapsed;

    void Start()
    {
        // start the ROS connection
        ros = ROSConnection.GetOrCreateInstance();
        ros.RegisterPublisher<boolmsg>(topicName);
    }

    public void onPress()
    {
            boolmsg msg = new boolmsg(
                true
            );

            // Finally send the message to server_endpoint.py running in ROS
            ros.Publish(topicName, msg);

    }
}

