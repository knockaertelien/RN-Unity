using System.Collections.Generic;
using UnityEngine;

public class Layers : MonoBehaviour
{
    public static List<GameObject> layers = new List<GameObject>();
    public static List<GameObject> objects = new List<GameObject>();
    public static List<GameObject> clickables = new List<GameObject>();
    public GameObject buttonprefab;
    public GameObject dropdownlistPT;
    public GameObject dropdownlistLS;

    public void Reset()
    {
        layers = new List<GameObject>();
        objects = new List<GameObject>();
        clickables = new List<GameObject>();
        foreach (Transform child in dropdownlistPT.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in dropdownlistLS.transform)
        {
            Destroy(child.gameObject);
        }
    } 

    public void ToggleDropdown()
    {
        var dropDownObject = dropdownlistPT.transform.parent.transform.parent.transform.parent.gameObject;
        if (dropDownObject.activeInHierarchy)
        {
            dropDownObject.SetActive(false);
        }
        else
        {
            dropDownObject.SetActive(true);
        }
        dropDownObject = dropdownlistLS.transform.parent.transform.parent.transform.parent.gameObject;
        if (dropDownObject.activeInHierarchy)
        {
            dropDownObject.SetActive(false);
        }
        else
        {
            dropDownObject.SetActive(true);
        }
    }

    public void CreateStructure(Transform imageTargetTemplate)
    {
        foreach (Transform child in imageTargetTemplate)
        {
            foreach (Transform child2 in child.transform)
            {
                var layer = child2.gameObject;
                layers.Add(layer);
                foreach (Transform child3 in child2.transform)
                {
                    var obj = child3.gameObject;
                    objects.Add(obj);
                    foreach (Transform child4 in child3.transform)
                    {
                        var objName = child4.gameObject.name;
                        if (objName == "Clickable")
                        {
                            foreach (Transform child5 in child4.transform)
                            {
                                var clickableObj = child5.gameObject;
                                clickables.Add(clickableObj);
                            }
                        }
                    }
                }
            }
        }
        AddButtons();
        var spawnedObject = GameObject.Find("ImageTarget");
        foreach (Transform child in spawnedObject.transform)
        {
            //child1 = turbine
            foreach (Transform child2 in child.transform)
            {
                //child2 = layers
                foreach (Transform child3 in child2.transform)
                {
                    //child3 = objects
                    if (child3.GetComponent<MeshCollider>() != null)
                    {
                        child3.GetComponent<MeshCollider>().enabled = true;
                        child3.GetComponent<MeshCollider>().convex = true;
                        child3.GetComponent<MeshCollider>().isTrigger = true;
                    }
                    foreach (Transform child4 in child3.transform)
                    {
                        //child4 = clickables
                        foreach (Transform child5 in child4.transform)
                        {
                            if (child5.GetComponent<MeshCollider>() != null)
                            {
                                child5.GetComponent<MeshCollider>().enabled = false;
                                child5.GetComponent<MeshCollider>().convex = true;
                                child5.GetComponent<MeshCollider>().isTrigger = false;
                            }
                        }
                    }
                }
            }
        }
    }

    public void AddButtons()
    {
        DropdownButton button = buttonprefab.GetComponent<DropdownButton>();
        var name = "All";
        button.Setup(name, dropdownlistPT);
        for (int i = 0; i < layers.Count; i++)
        {
            name = layers[i].gameObject.name;
            button.Setup(name, dropdownlistPT);
        }
        button.Setup(name, dropdownlistLS);
        for (int i = 0; i < layers.Count; i++)
        {
            name = layers[i].gameObject.name;
            button.Setup(name, dropdownlistLS);
        }
    }
}
