using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{

    public GameObject parts;
    //private string guiDebugText = "test";

    /// <summary>
    /// Looks for parts to explode them.
    /// </summary>
    private void Start()
    {
        parts = GameObject.Find("AR Session Origin/AR Camera/Parts");
    }

    /// <summary>
    /// Draws Gui on screen. Used for Debug
    /// Here used to explode object.
    /// </summary>
    private void OnGUI()
    {
        //GUI.Label(new Rect(10, 10, 100, 20), guiDebugText);
        //if (GUI.Button(new Rect(50, 50, 200, 200), "explode"))
        //{
        //    ExplodeObjToggle();
            
        //}
    }

    /// <summary>
    /// Toggles the exploded state of the object under Parts directory (AR Origin/Ar Camera/Parts)
    /// </summary>
    public void ExplodeObjToggle()
    {
        var amountOfObj = parts.transform.childCount;

        // checks if parts has more than 1 child (if it does, it means it can be "exploded"
        if (amountOfObj >1)
        {
            if (parts.transform.GetChild(0).gameObject.activeInHierarchy)
            {
                parts.transform.GetChild(1).localEulerAngles = parts.transform.GetChild(0).localEulerAngles;
                parts.transform.GetChild(1).localScale = parts.transform.GetChild(0).localScale;
                parts.transform.GetChild(0).gameObject.SetActive(false);
                parts.transform.GetChild(1).gameObject.SetActive(true);
                //guiDebugText = "toggle on";
            }
            else
            {
                parts.transform.GetChild(0).localEulerAngles = parts.transform.GetChild(1).localEulerAngles;
                parts.transform.GetChild(0).localScale = parts.transform.GetChild(1).localScale;
                parts.transform.GetChild(0).gameObject.SetActive(true);
                parts.transform.GetChild(1).gameObject.SetActive(false);
                //guiDebugText = "toggle off";
            }
        }
        
    }
}
