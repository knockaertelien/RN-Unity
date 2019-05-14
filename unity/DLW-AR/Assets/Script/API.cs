using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class API : MonoBehaviour
{
    // manuele databaseId 
    private string databaseId = "dlwar";
    public Text titlePT;
    public Text keyPT;
    public Text valuePT;

    public Text titleLS;
    public Text keyLS;
    public Text valueLS;

    List<string> listKeys = new List<string>();
    List<string> listValues = new List<string>();

    // get collectionId and partId
    public void GetData(string collectionId, string partId)
    {
        StartCoroutine(GetText(collectionId, partId));
    }

    IEnumerator GetText(string collectionId, string partId)
    {
        UnityWebRequest www = UnityWebRequest.Get("https://dlwar.azurewebsites.net/api/db/" + databaseId + "/" + collectionId + "/" + partId);
        yield return www.SendWebRequest();
        ClearText();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            string response = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
            object dec = JsonConvert.DeserializeObject(response); // deserializing Json string (it will deserialize Json string)
            JObject obj = JObject.Parse(dec.ToString()); // it will parse deserialize Json object
            
            foreach (var o in obj)
            {
                if (!o.Key.Contains('_'))
                {
                    if (o.Key.Contains("id"))
                    {
                        titlePT.text = o.Value.ToString();
                        titleLS.text = o.Value.ToString();
                    }
                    else
                    {
                        var par = o.Key.ToString() + Environment.NewLine;
                        keyPT.text += par;
                        keyLS.text += par;
                        var par2 = o.Value.ToString() + Environment.NewLine;
                        valuePT.text += par2;
                        valueLS.text += par2;
                    }
                }
            }
        }
    }

    public void ClearText()
    {
        titlePT.text = "";
        keyPT.text = "";
        valuePT.text = "";

        titleLS.text = "";
        keyLS.text = "";
        valueLS.text = "";
    }
}
