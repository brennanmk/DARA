using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class open_menu : MonoBehaviour
{ //from https://www.youtube.com/watch?v=LziIlLB2Kt4
    public GameObject menu_panel;
    public GameObject menu_button;

    public void open(){
        if(menu_panel!=null && menu_button!=null){
            menu_panel.SetActive(true);
            menu_button.SetActive(false);
        }
    }
}
