/*
Brennan Miller-Klugman
12/1/22

Creates menu options for every connected robot using prefabs
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Vuforia;
using UnityEngine.Networking;

public class dynamic_menu : MonoBehaviour
{
    public GameObject menu_options;
    public GameObject side_bar;
    public GameObject open_menu_button;

    public GameObject scripts;
    public GameObject overlay;

    public GameObject menu_container;
    public GameObject menu_header;
    public GameObject menu_item_container;
    public GameObject menu_item;

    public GameObject emoji_cube;

    private List<GameObject> headers = new List<GameObject>();

    public void create()
    /*
        The create function is used to dynamically create menu options for each robot

        References:
        https://gamedev.stackexchange.com/questions/102431/how-to-create-gui-image-with-script
        https://stackoverflow.com/questions/69427848/how-to-modify-ui-text-via-script
        https://docs.unity3d.com/2018.1/Documentation/ScriptReference/UI.Toggle-onValueChanged.html
        https://answers.unity.com/questions/674127/how-to-find-a-prefab-via-script.html
        https://answers.unity.com/questions/1271901/index-out-of-range-when-using-delegates-to-set-onc.html
        https://www.w3schools.com/cs/cs_switch.php
    */
    {
        for(int val = 0; val < static_variables.robot.Count; val++)
        {
            //create a container to hold the header and items
            GameObject container = Instantiate(menu_container);
            container.name = "container_" + static_variables.robot[val].name;
            container.transform.SetParent(menu_options.transform);

            //create header prefab 
            GameObject header = Instantiate(menu_header);
            header.name = "header_" + static_variables.robot[val].name;
            header.transform.SetParent(container.transform);
            header.GetComponentInChildren<TextMeshProUGUI>().text = static_variables.robot[val].name.ToUpper();

            //create a container to hold menu items
            GameObject item_container = Instantiate(menu_item_container);
            item_container.name = "item_container_" + static_variables.robot[val].name;
            item_container.transform.SetParent(container.transform);

            //create teleoperation menu item
            if(static_variables.robot[val].base_dof != 0)
            {
                //add a menu item
                GameObject twist_item = Instantiate(menu_item);
                twist_item.name = static_variables.robot[val].name + "_twist";
                twist_item.transform.SetParent(item_container.transform);
                twist_item.GetComponentInChildren<TextMeshProUGUI>().text = "Base Teleoperation";

                GameObject joysticks = null;

                switch (static_variables.robot[val].base_dof) 
                {
                    case 2:
                        var prefab = (GameObject)Resources.Load("Prefabs/twist/1dof_joystick", typeof(GameObject));
                        joysticks = Instantiate(prefab);
                        joysticks.GetComponent<one_dof_twist_publisher>().robot = static_variables.robot[val];
                        break; 
                }
                joysticks.name = static_variables.robot[val].name + "_joysticks";
                joysticks.transform.SetParent(overlay.transform);

                twist_item.GetComponent<Toggle>().onValueChanged.AddListener(delegate { //when toggled on, close sidebar and open joysticks
                    joysticks.SetActive(twist_item.GetComponent<Toggle>().isOn);
                    side_bar.SetActive(!twist_item.GetComponent<Toggle>().isOn);
                    open_menu_button.SetActive(twist_item.GetComponent<Toggle>().isOn);
                });

                joysticks.SetActive(false);
            }

            //create affect menu item
            if (static_variables.robot[val].multi_target_dataset != "" && static_variables.robot[val].multi_target_name != "") //add cube
            {
                StartCoroutine(getMultiTarget(static_variables.robot[val]));

                //add a menu item
                GameObject affect = Instantiate(menu_item);
                affect.name = static_variables.robot[val].name + "_affectVisuals";
                affect.transform.SetParent(item_container.transform);
                affect.GetComponentInChildren<TextMeshProUGUI>().text = "Affect Visualization";
                
                //add a subscriber
                var affect_subscriber = affect.AddComponent<visual_affects>(); //add visual affect subscriber
                affect_subscriber.robot = static_variables.robot[val];

                var temp_iterator = val; 
                affect.GetComponent<Toggle>().onValueChanged.AddListener(delegate { //when toggled on, close sidebar and turn on affect visuals
                    static_variables.robot[temp_iterator].multi_target_behavior.enabled = affect.GetComponent<Toggle>().isOn;
                    side_bar.SetActive(!affect.GetComponent<Toggle>().isOn);
                    open_menu_button.SetActive(affect.GetComponent<Toggle>().isOn);
                });
            }

            item_container.SetActive(false); //default to inactive
            header.GetComponent<toggle_dropdown>().dropdown_content = item_container; //assign item container to header for the toggle button

        }
    } 

    IEnumerator getMultiTarget(data robot)
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

        string datFile = string.Format("{0}.dat", robot.multi_target_dataset);
        string xmlFile = string.Format("{0}.xml", robot.multi_target_dataset);

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
            
            string targetName = robot.multi_target_name;

            MultiTargetBehaviour target = VuforiaBehaviour.Instance.ObserverFactory.CreateMultiTarget(
                savePath_xml,
                targetName);    
                
            var handler = target.gameObject.AddComponent<DefaultObserverEventHandler>(); //add event handler
            handler.UsePoseSmoothing = true;
            Debug.Log(targetName);

            GameObject cube_emoji = Instantiate(emoji_cube);
            cube_emoji.transform.SetParent(target.transform);
            
            //Update cube transform (these values were found by trial and error)
            cube_emoji.transform.position = new Vector3(0, (float)0.1, (float)0.25);

            Quaternion rotate = Quaternion.Euler(90, 0, 0); //https://docs.unity3d.com/ScriptReference/Transform-rotation.html
            cube_emoji.transform.rotation = rotate;

            cube_emoji.transform.localScale = new Vector3((float)0.2, (float)0.2, (float)0.2);

            target.enabled = false; //default target to inactive

            robot.multi_target_behavior = target;
        }

    }
}
