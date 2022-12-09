/*
Brennan Miller-Klugman
12/9/22

Subscribes to affect message and updates the multitarget visual

References:
https://github.com/Unity-Technologies/Unity-Robotics-Hub/blob/main/tutorials/ros_unity_integration/subscriber.md
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Robotics.ROSTCPConnector;
using affect = RosMessageTypes.Std.StringMsg;

public class visual_affects : MonoBehaviour
{
    public data robot;
    public RawImage img;
    void Start()
    {
        ROSConnection.GetOrCreateInstance().Subscribe<affect>(robot.affect_topic, updateAffect);
        img = robot.multi_target_behavior.gameObject.GetComponent<RawImage>();
    }

    void updateAffect(affect affectMessage)
    {
        if (affectMessage.data == "happy")
        {
            img.texture = Resources.Load<Texture2D>("Resources/Emojis/happy");
        }
        else if (affectMessage.data == "sad")
        {
            img.texture = Resources.Load<Texture2D>("Resources/Emojis/sad");
        }
        else if (affectMessage.data == "angry")
        {
            img.texture = Resources.Load<Texture2D>("Resources/Emojis/angry");
        }
        else if (affectMessage.data == "neutral")
        {
            img.texture = Resources.Load<Texture2D>("Resources/Emojis/neutral");
        }
        else if (affectMessage.data == "confused")
        {
            img.texture = Resources.Load<Texture2D>("Resources/Emojis/confused");
        } 
        else if (affectMessage.data == "tired")
        {
            img.texture = Resources.Load<Texture2D>("Resources/Emojis/tired");
        }
    }
}
