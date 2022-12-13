/*
Brennan Miller-Klugman
12/9/22

Subscribes to affect message and updates the multitarget visual

References:
https://github.com/Unity-Technologies/Unity-Robotics-Hub/blob/main/tutorials/ros_unity_integration/subscriber.md
https://docs.unity3d.com/ScriptReference/Transform.GetChild.html
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
        ROSConnection.GetOrCreateInstance().Subscribe<affect>(robot.affect_topic, updateAffect); //create subscriber

        GameObject emoji_plane = robot.multi_target_behavior.transform.GetChild(0).gameObject; 
        img = emoji_plane.gameObject.GetComponent<Renderer>();
    }

    void updateAffect(affect affectMessage) //on subscribe, update the emoji based on message contents
    {
        if (affectMessage.data == "happy")
        {
            img.material = Resources.Load<Material>("Emojis/Materials/happy");
        }
        else if (affectMessage.data == "sad")
        {
            img.material = Resources.Load<Material>("Emojis/Materials/sad");
        }
        else if (affectMessage.data == "angry")
        {
            img.material = Resources.Load<Material>("Emojis/Materials/angry");
        }
        else if (affectMessage.data == "neutral")
        {
            img.material = Resources.Load<Material>("Emojis/Materials/neutral");
        }
        else if (affectMessage.data == "confused")
        {
            img.material = Resources.Load<Material>("Emojis/Materials/confused");
        } 
        else if (affectMessage.data == "tired")
        {
            img.material = Resources.Load<Material>("Emojis/Materials/tired");
        }
    }
}
