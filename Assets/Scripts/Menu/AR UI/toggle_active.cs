/*
Brennan Miller-Klugman
Last Edited: 10/2/22
based loosely off of https://www.youtube.com/watch?v=LziIlLB2Kt4
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toggle_active : MonoBehaviour
{ 
    public GameObject failure;
    public GameObject success;
    public GameObject loading;
    public GameObject loading_screen;

    public void open_failure(){
        if(failure!=null){
            failure.SetActive(true);
        }
    }

    public void close_failure(){
        if(failure!=null&&loading_screen!=null){
            failure.SetActive(false);
            loading_screen.SetActive(false);
        }
    }

    public void open_success(){
        if(success!=null){
            success.SetActive(true);
        }
    }

    public void close_success(){
        if(success!=null&&loading_screen!=null){
            success.SetActive(false);
            loading_screen.SetActive(false);
        }
    }

    public void open_loading(){
        if(loading!=null&&loading_screen!=null){
            loading_screen.SetActive(true);
            loading.SetActive(true);
        }
    }

    public void close_loading(){
        if(loading!=null){
            loading.SetActive(false);
        }
    }
}
