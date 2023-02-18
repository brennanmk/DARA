/*
Brennan Miller-Klugman
12/13/22

This script allows all 4 sides of the emoji cube to be updated with a single material

References:
https://answers.unity.com/questions/30934/texturing-a-cube-different-textures-on-a-face.html
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class emoji_cube_update : MonoBehaviour
{

    public GameObject side1;
    public GameObject side2;
    public GameObject side3;
    public GameObject side4;

    private Renderer rend1;
    private Renderer rend2;
    private Renderer rend3;
    private Renderer rend4;

    void Start()
    {
        rend1 = side1.GetComponent<Renderer>();
        rend2 = side2.GetComponent<Renderer>();
        rend3 = side3.GetComponent<Renderer>();
        rend4 = side4.GetComponent<Renderer>();
    }

    public void update_render(Material mat)
    {
        rend1.material = mat;
        rend2.material = mat;
        rend3.material = mat;
        rend4.material = mat;
    }

}
