using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownButton : MonoBehaviour
{
    public Button button;
    public Text layerName;
    
    public void Setup(string name,GameObject dropdownlist)
    {
        layerName.text = name;
        var layerButton = Instantiate(button);        
        layerButton.transform.parent = dropdownlist.transform;
    }

    public void OnButtonClick()
    {
        var layerList = new List<GameObject>();
        var buttonText = this.GetComponentInChildren<Text>().text;
        Debug.Log(buttonText);
        var spawnedObject = GameObject.Find("ImageTarget");
        var childName = spawnedObject.transform.GetChild(0).gameObject.name;
        var layerPath = childName + "/" + buttonText;
        foreach (Transform child in spawnedObject.transform)
        {
            foreach (Transform child2 in child.transform)
            {
                layerList.Add(child2.gameObject);
            }
        }

        if(buttonText == "All")
        {
            for (int i = 0; i < layerList.Count; i++)
            {
                layerList[i].SetActive(true);
            }
        }
        else if (spawnedObject.transform.Find(layerPath) != null)
        {
            for (int i = 0; i < layerList.Count; i++)
            {
                layerList[i].SetActive(false);
            }
            spawnedObject.transform.Find(layerPath).gameObject.SetActive(true);
        }
    }
}
