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

    public JObject config_json;

    public void read_config()
    {
        StartCoroutine(read_config_coroutine());
    }

    public IEnumerator read_config_coroutine()
    {
        // read config file
        UnityWebRequest config_web_query = UnityWebRequest.Get("config_location");
        yield return config_web_query.SendWebRequest();

        var config = config_web_query.downloadHandler.text;

        var _json = JObject.Parse(config);

        config_json = _json;

    }
}
