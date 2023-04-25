using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Unity.Robotics.ROSTCPConnector;
using compressed_image = RosMessageTypes.Sensor.CompressedImageMsg;
public class Depth : MonoBehaviour
{
    ROSConnection ros;
    public string topicName = "/depth_image";

    private ARCameraManager _cameraManager;
    private XRCameraIntrinsics _cameraIntrinsics;
    private bool _initialized;

    // Start is called before the first frame update
    void Start()
    {
        ros = ROSConnection.GetOrCreateInstance();
        ros.RegisterPublisher<compressed_image>(topicName);

        _cameraManager = FindObjectOfType<ARCameraManager>();
        _cameraManager.frameReceived += OnCameraFrameReceived;
    }

    void OnCameraFrameReceived(ARCameraFrameEventArgs eventArgs)
    {
        if (_cameraManager.TryAcquireLatestCpuImage(out XRCpuImage cameraImage))
        {
            using (cameraImage)
            {
                ros.Publish(topicName, cameraImage);
            }
        }
    }
}
