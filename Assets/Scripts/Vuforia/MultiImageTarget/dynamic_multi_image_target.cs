using System.IO;
using UnityEngine;
using Vuforia;

public class dynamic_multi_image_target : MonoBehaviour
{
    // Load and activate a data set at the given path.
    public void CreateMultiTargetFromWeb(string dataSetPath, string targetName, data robot)
    {
        // Create an Image Target from the database.
        MultiTargetBehaviour target = VuforiaBehaviour.Instance.ObserverFactory.CreateMultiTarget(
            dataSetPath,
            targetName);    

        robot.multi_target_behavior = target;
    }

}