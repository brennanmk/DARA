/*
Brennan Miller-Klugman
Last Edited: 10/2/22
based off of https://www.youtube.com/watch?v=LziIlLB2Kt4
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class open_sidebar : MonoBehaviour
{ 
    public GameObject menu_panel;
    public GameObject menu_button;

    public void open(){
        if(menu_panel!=null && menu_button!=null){
            menu_panel.SetActive(true);
            menu_button.SetActive(false);
        }
    }
}
