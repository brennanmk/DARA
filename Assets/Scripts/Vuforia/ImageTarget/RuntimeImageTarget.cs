/*
Brennan Miller-Klugman
Last Edited: 10/2/22

References:
https://dev.mysql.com/doc/connector-net/en/connector-net-tutorials-sql-command.html
https://github.com/freseco/Unity3d_MySQL_Client
https://stackoverflow.com/questions/37344167/how-to-get-float-value-with-sqldatareader
https://library.vuforia.com/objects/create-and-load-targets-unity
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using MySqlConnector;
using UnityEngine.Networking;
namespace Unity.Robotics.ROSTCPConnector
{
    public class RuntimeImageTarget : MonoBehaviour
    {
        ArrayedEventHandler eventHandler;
        // Start is called before the first frame update
        void Start()
        {
            VuforiaApplication.Instance.OnVuforiaInitialized += OnVuforiaInitialized;
        }

        void OnVuforiaInitialized(VuforiaInitError error)
        {
            StartCoroutine(RetrieveTextureFromWeb());
        }

        IEnumerator RetrieveTextureFromWeb()
        {
            string connStr = @"server=98.229.202.174;port=3307;user=root;password=NveQlG8bKp89hPWMdhdC6jBnd;database=ar;";
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();

            string sql = "SELECT image, name, image_target_width, ip_addr, port, id FROM robots";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
                {
                    Texture2D imageFromWeb;

                    using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(rdr[0].ToString()))
                    {
                        yield return uwr.SendWebRequest();
                        if (uwr.result != UnityWebRequest.Result.Success)
                        {
                            Debug.Log(uwr.error);
                        }
                        else
                        {
                            // Get downloaded texture once the web request completes
                            var texture = DownloadHandlerTexture.GetContent(uwr);
                            imageFromWeb = texture;
                            Debug.Log("Image downloaded " + uwr);
                            CreateImageTargetFromDownloadedTexture(rdr[1].ToString(), imageFromWeb, (float)rdr[2], rdr[3].ToString(), (int)rdr[4], (int)rdr[5]); 
                        }
                    }
                }
                rdr.Close();
        }
        void CreateImageTargetFromDownloadedTexture(string targetName, Texture2D imageFromWeb,  float width, string ip, int port, int id)
        {
            var target = VuforiaBehaviour.Instance.ObserverFactory.CreateImageTarget(
                imageFromWeb,
                width,
                targetName
            );

            // Add the DefaultObserverEventHandler to the newly created game object
            eventHandler = target.gameObject.AddComponent<ArrayedEventHandler>(); //add ArrayedEventHandler Script;
            eventHandler.id = id; 

            eventHandler.ip = ip; 
            eventHandler.port = port;


            Debug.Log("Target created and active" + target);
        }
    }
}