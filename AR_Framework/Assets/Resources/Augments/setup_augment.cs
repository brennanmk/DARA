/*
Brennan Miller-Klugman
12/1/22

Base class for all augment prefabs. 

Referances:
https://www.newtonsoft.com/json
*/
using UnityEngine;
using System.Collections;
using Newtonsoft.Json.Linq;
using UnityEngine.Networking;

public class setup_augment : MonoBehaviour
{
    public string config_location;
    public data robot;

    public GameObject augment_prefab;

    public IEnumerator read_config(System.Action<JToken> callback)
    {
        // read config file
        UnityWebRequest config_web_query = UnityWebRequest.Get(config_location);
        yield return config_web_query.SendWebRequest();

        var config = config_web_query.downloadHandler.text;

        Debug.Log(config);
        

        var _json = JToken.Parse(config);

        callback(_json);

    }
}
