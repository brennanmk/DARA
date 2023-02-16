/*
Brennan Miller-Klugman
Last Edited: 10/2/22
based off of https://www.youtube.com/watch?v=LziIlLB2Kt4
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class close_loading_screen : MonoBehaviour
{     
    public GameObject canvas;
    public GameObject overlay;

    public void close(){
        if(canvas!=null && overlay!=null){
            canvas.SetActive(false);
            overlay.SetActive(false);
        }
    }
}
