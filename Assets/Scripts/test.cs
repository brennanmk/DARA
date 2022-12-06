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

public class test : MonoBehaviour
    {

    public InputAction moveAction;


    void Start()
    {

    }

    private void Update()
    {
        Debug.Log(moveAction.ReadValue<Vector2>().x);

    }
}

