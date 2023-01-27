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
using MySqlConnector;

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

    public void create()
    /*
        The create function is used to dynamically create menu options for each robot

        References:
        https://gamedev.stackexchange.com/questions/102431/how-to-create-gui-image-with-script
        https://stackoverflow.com/questions/69427848/how-to-modify-ui-text-via-script
        https://docs.unity3d.com/2018.1/Documentation/ScriptReference/UI.Toggle-onValueChanged.html
        https://answers.unity.com/questions/674127/how-to-find-a-prefab-via-script.html
        https://docs.unity3d.com/Manual/UnityWebRequest-DownloadingAssetBundle.html
        https://www.softwaretestinghelp.com/c-sharp/csharp-list-and-dictionary/#C_Dictionary
        https://answers.unity.com/questions/1104847/use-a-string-to-call-a-class.html
    */  
    {
        string connStr = @"server=98.229.202.174;port=3307;user=root;password=NveQlG8bKp89hPWMdhdC6jBnd;database=ar;";
        MySqlConnection conn = new MySqlConnection(connStr);
        conn.Open();

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

            //query robot_features table
            string sql = $"SELECT name, config FROM robot_features WHERE id = {static_variables.robot[val].id}";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
                { //update static variables 
                    var ft_name = rdr[0].ToString();
                    var config = rdr[2].ToString();

                    GameObject temp_obj = Instantiate(menu_item);
                    temp_obj.name = static_variables.robot[val].name + ft_name;
                    temp_obj.transform.SetParent(item_container.transform);
                    temp_obj.GetComponentInChildren<TextMeshProUGUI>().text = ft_name;

                    var prefab = (GameObject)System.Activator.CreateInstance(System.Type.GetType("Assets.Resources.Prefabs.Augments." + ft_name));
                    var instant = Instantiate(prefab);
                    instant.GetComponent<Setup>().config = config;
                    instant.GetComponent<Setup>().robot = static_variables.robot[val];
                    
                    temp_obj.GetComponent<Toggle>().onValueChanged.AddListener(delegate { //when toggled on, close sidebar and open joysticks
                    instant.SetActive(temp_obj.GetComponent<Toggle>().isOn);
                    side_bar.SetActive(!temp_obj.GetComponent<Toggle>().isOn);
                    open_menu_button.SetActive(temp_obj.GetComponent<Toggle>().isOn);
                    });
                }
            
            rdr.Close();
       
        }
    }
}
