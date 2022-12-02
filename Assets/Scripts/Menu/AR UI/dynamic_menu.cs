/*
Brennan Miller-Klugman
12/1/22

Creates menu options for every connected robot using prefabs

References:
https://gamedev.stackexchange.com/questions/102431/how-to-create-gui-image-with-script
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dynamic_menu : MonoBehaviour
{
    public GameObject menu_options;

    public GameObject menu_container;
    public GameObject menu_header;
    public GameObject menu_item_container;
    public GameObject menu_item;

    private List<GameObject> headers = new List<GameObject>();

    // Start is called before the first frame update
    public void create()
    {

        for(int val = 0; val < static_variables.robot.Count; val++)
        {
            GameObject container = Instantiate(menu_container);
            container.name = "container_" + static_variables.robot[val].name;
            container.transform.SetParent(menu_options.transform);

            GameObject header = Instantiate(menu_header);
            header.name = "header_" + static_variables.robot[val].name;
            header.transform.SetParent(container.transform);
            header.GetComponentInChildren<TextMesh>().text = static_variables.robot[val].name;

            GameObject item_container = Instantiate(menu_item_container);
            item_container.name = "item_container_" + static_variables.robot[val].name;
            item_container.transform.SetParent(container.transform);

            GameObject item = Instantiate(menu_item);
            item.name = "item_" + static_variables.robot[val].name;
            item.transform.SetParent(item_container.transform);
            item.GetComponent<TextMesh>().text = "Affect Visualization";

            header.GetComponent<toggle_dropdown>().dropdown_content = item_container; //assign item container to header for the toggle button

        }
    } 
}
