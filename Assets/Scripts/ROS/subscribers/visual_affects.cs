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
    public Renderer img;
    void Start()
    {
        ROSConnection.GetOrCreateInstance().Subscribe<affect>(robot.affect_topic, updateAffect);
        img = robot.multi_target_behavior.gameObject.GetComponent<Renderer>();
    }

    void updateAffect(affect affectMessage)
    {
        if (affectMessage.data == "happy")
        {
            img.material = Resources.Load<Material>("Resources/Emojis/Materials/happy");
        }
        else if (affectMessage.data == "sad")
        {
            img.material = Resources.Load<Material>("Resources/Emojis/Materials/sad");
        }
        else if (affectMessage.data == "angry")
        {
            img.material = Resources.Load<Material>("Resources/Emojis/Materials/angry");
        }
        else if (affectMessage.data == "neutral")
        {
            img.material = Resources.Load<Material>("Resources/Emojis/Materials/neutral");
        }
        else if (affectMessage.data == "confused")
        {
            img.material = Resources.Load<Material>("Resources/Emojis/Materials/confused");
        } 
        else if (affectMessage.data == "tired")
        {
            img.material = Resources.Load<Material>("Resources/Emojis/Materials/tired");
        }
    }
}
