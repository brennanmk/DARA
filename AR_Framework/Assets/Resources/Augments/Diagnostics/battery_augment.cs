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
using battery = RosMessageTypes.Std.Int16Msg;

public class battery_augment : MonoBehaviour
    {
    ROSConnection ros;
    public data robot;
    public string topicName, dataset, target_name;
    public augment_cube cube_;
    public GameObject cube_object, battery_object;

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
            ros = robot.ros_connection;

            topicName = (string)config_json["diagnostic_topic"];
            dataset = (string)config_json["multi_target_dataset"];
            target_name = (string)config_json["target_name"];

            StartCoroutine(getMultiTarget());
        }
        ));
    }

    public IEnumerator getMultiTarget()
    {
        /* Function to create multi image target based on database info
            References:
            The following Unity forums were referenced:
            https://answers.unity.com/questions/181893/copy-data-from-resources-to-persistent-data-path.html
            https://answers.unity.com/questions/228150/hold-or-wait-while-coroutine-finishes.html
            https://forum.unity.com/threads/download-a-file-from-url-and-save-it-to-persistentdatapath.520817/
            https://forum.unity.com/threads/should-be-simple-but-i-just-cant-do-it-changing-texture-by-script.872356/
            Additionally, https://docs.unity3d.com was referenced for general documentation on Unity scripting including the following posts:
            https://docs.unity3d.com/ScriptReference/Transform-rotation.html
            https://docs.unity3d.com/ScriptReference/GameObject.CreatePrimitive.html
        */

        string datFile = string.Format("{0}.dat", dataset);
        string xmlFile = string.Format("{0}.xml", dataset);

        //download dat
        UnityWebRequest _dat = new UnityWebRequest(datFile);
        _dat.downloadHandler = new DownloadHandlerBuffer();

        yield return _dat.SendWebRequest();
        if(_dat.isNetworkError || _dat.isHttpError) {
            Debug.Log(_dat.error);
        }
        else {
            byte[] results_dat = _dat.downloadHandler.data;
            string savePath_dat = string.Format("{0}/{1}.dat", Application.persistentDataPath, robot.name);        

            System.IO.File.WriteAllBytes(savePath_dat, results_dat);
            Debug.Log("File successfully downloaded and saved to " + savePath_dat);
        }

        //download xml
        UnityWebRequest _xml = new UnityWebRequest(xmlFile);
        _xml.downloadHandler = new DownloadHandlerBuffer();

        yield return _xml.SendWebRequest();
        if(_xml.isNetworkError || _xml.isHttpError) {
            Debug.Log(_xml.error);
        }
        else {
            byte[] results_xml = _xml.downloadHandler.data;
            string savePath_xml = string.Format("{0}/{1}.xml", Application.persistentDataPath, robot.name);        

            System.IO.File.WriteAllBytes(savePath_xml, results_xml);
            Debug.Log("File successfully downloaded and saved to " + savePath_xml);
            

            MultiTargetBehaviour target = VuforiaBehaviour.Instance.ObserverFactory.CreateMultiTarget(
                savePath_xml,
                target_name);    
            
            target.transform.SetParent(battery_object.transform);

            var handler = target.gameObject.AddComponent<DefaultObserverEventHandler>(); //add event handler
            handler.UsePoseSmoothing = true;
          
            cube_object.transform.SetParent(target.transform);

            //Update cube transform (these values were found by trial and error)
            cube_object.transform.position = new Vector3(0, (float)0.1, (float)0.25);

            Quaternion rotate = Quaternion.Euler(90, 0, 0); //https://docs.unity3d.com/ScriptReference/Transform-rotation.html
            cube_object.transform.rotation = rotate;

            cube_object.transform.localScale = new Vector3((float)0.2, (float)0.2, (float)0.2);
            
        }
    }

    void updateCube(battery data) //on subscribe, update the emoji based on message contents
    {

        if (data.data <= 20){
            cube_.update_render(Resources.Load<Material>("Augments/Diagnostics/ux/Materials/critical_battery"));
        } else if(data.data <= 40){
            cube_.update_render(Resources.Load<Material>("Augments/Diagnostics/ux/Materials/low_battery"));
        } else if(data.data <= 60){
            cube_.update_render(Resources.Load<Material>("Augments/Diagnostics/ux/Materials/half_battery"));
        } else if(data.data <= 80){
            cube_.update_render(Resources.Load<Material>("Augments/Diagnostics/ux/Materials/high_battery"));
        } else if(data.data <= 100){
            cube_.update_render(Resources.Load<Material>("Augments/Diagnostics/ux/Materials/full_battery"));
        }
    }
}
