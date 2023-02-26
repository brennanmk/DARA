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

public class augment_cube : MonoBehaviour
{

    public GameObject side1;


    private Renderer rend1;


    void Start()
    {
        rend1 = side1.GetComponent<Renderer>();
    }

    public void update_render(Material mat)
    {
        rend1.material = mat;
    }

}
