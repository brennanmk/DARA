/*
Brennan Miller-Klugman
Last Edited: 10/2/22
based off of https://www.youtube.com/watch?v=LziIlLB2Kt4
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toggle_dropdown : MonoBehaviour
{ 
    private int status = 0; //0 indicates that menu dropdown is closed
    public GameObject dropdown_close;
    public GameObject dropdown_open;
    public GameObject dropdown_content;

    public void toggle(){
        if(status == 0){
            open();
            status = 1;
        } else {
            close();
            status = 0;
        }
    }

    public void close(){
        if(dropdown_close!=null && dropdown_open!=null && dropdown_content!=null){
            dropdown_open.SetActive(false);
            dropdown_close.SetActive(true);
            dropdown_content.SetActive(false);
        }
    }

    public void open(){
        if(dropdown_close!=null && dropdown_open!=null && dropdown_content!=null){
            dropdown_open.SetActive(true);
            dropdown_close.SetActive(false);
            dropdown_content.SetActive(true);
        }
    }
}
