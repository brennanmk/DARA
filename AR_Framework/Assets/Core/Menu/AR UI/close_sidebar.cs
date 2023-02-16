/*
Brennan Miller-Klugman
Last Edited: 10/2/22
based off of https://www.youtube.com/watch?v=LziIlLB2Kt4
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class close_sidebar : MonoBehaviour
{ 
    public GameObject side_bar;
    public GameObject menu_button;

    public void close(){
        if(side_bar!=null && menu_button!=null){
            side_bar.SetActive(false);
            menu_button.SetActive(true);

        }
    }
}
