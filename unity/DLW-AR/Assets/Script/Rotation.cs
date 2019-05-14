using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using is = DeviceChange;

public class Rotation : MonoBehaviour
{
    //public DeviceChange orientation;
    public GameObject PortraitUI;
    public GameObject LandscapeUI;
    public GameObject infoPart;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        StartCoroutine(Wait());
        
    }

    IEnumerator Wait()
    {
        if (LandscapeUI.activeInHierarchy)
        {
            yield return new WaitUntil(() => Screen.orientation == ScreenOrientation.Portrait);
            LandscapeUI.SetActive(false);
            PortraitUI.SetActive(true);            
        }

        else if(PortraitUI.activeInHierarchy)
        {
            yield return new WaitUntil(() => Screen.orientation == ScreenOrientation.Landscape);
            LandscapeUI.SetActive(true);
            PortraitUI.SetActive(false);            
        }

    }
}

