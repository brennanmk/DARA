/*
Brennan Miller-Klugman
Last Edited: 10/2/22
based off of https://www.youtube.com/watch?v=LziIlLB2Kt4
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class continue_button : MonoBehaviour
{     

    public void launch_ar(){
        SceneManager.LoadScene("AR");
    }
}
