/*
Brennan Miller-Klugman
12/14/22

Rotates the plane to face the camera

References:
https://developer.vuforia.com/forum/unity/3d-or-2d-ui-text-constantly-facing-camera
https://docs.unity3d.com/ScriptReference/Transform.LookAt.html
https://docs.unity3d.com/ScriptReference/Camera-main.html
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class face_camera : MonoBehaviour
{
    public Transform cam;

    void Start()
    {
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(cam, Vector3.zero);
        transform.Rotate(90, 0, 0);
    }
}
