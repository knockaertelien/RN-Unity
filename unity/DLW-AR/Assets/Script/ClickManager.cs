using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;

public class ClickManager : MonoBehaviour
{
    private Vector3 position;
    private Vector3 rotation;
    private Vector3 scale;

    public GameObject parts;
    public GameObject infoParts;
    public GameObject darkenBgPart;
    public GameObject blurPlanePrefab;
    public GameObject[] machine;
    public int chosenObject=5;

    public Vector3 boundsObj;
    private bool check = true;
    private int partState= 0;

    /// <summary>
    /// manages state of scene and changes behaviour of taps.
    /// </summary>
    private void Update()
    {
        if (partState ==0)
        {
            GetPart();
        }
        if (partState == 1)
        {
            GetInfoPart();
        }
        else
        {

        }
        
    }

    /// <summary>
    /// Instances the part you click on, adds background and scales appropriately.
    /// </summary>
    private void GetPart()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // check because it registers 2 taps for some reason.
            if (check)
            {
                //raycasting to get first object hit.
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                // more raycasting
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    //excludes any trackables from being instanced. you don't want to be able to rotate the mapping plane :)
                    if (hit.collider.gameObject.transform.parent.name != "Trackables")
                    {
                        UnityMessageManager.Instance.SendMessageToRN(new UnityMessage()
                        {
                            name = "To part",
                            callBack = (data) =>
                            {
                                Debug.Log("onClickCallBack:" + data);
                            }
                        });

                        // Hides machine object
                        HideObject();


                        var arCameraObject = GameObject.Find("AR Session Origin/AR Camera");
                        var trackablesObj = GameObject.Find("AR Session Origin/Trackables");
                        var arCameraLocation = arCameraObject.transform;

                        //places background
                        var resultingPosition = arCameraLocation.position + new Vector3(0, 0, 200);
                        var chosenGameObject = hit.collider.gameObject;

                        // Instantiates the chosen gameobject
                        var spawnedGameObject = Instantiate(chosenGameObject);
                        // saves the name for the exploded object for further use
                        var objName = spawnedGameObject.name.Substring(0, spawnedGameObject.name.Length - 7);
                        var objNameExplode = objName + " explode";

                        //places the chosen object in front of the camera
                        spawnedGameObject.transform.eulerAngles = arCameraLocation.eulerAngles - new Vector3(-90.0f, 0.0001f, 0.0001f);
                        spawnedGameObject.transform.position = resultingPosition;
                        spawnedGameObject.transform.parent = parts.transform;
                        spawnedGameObject.SetActive(true);
                        spawnedGameObject.AddComponent<PositionController>();
                        spawnedGameObject.transform.localScale = scale;

                        // virtual bounding box for the scaled objects to get placed inside
                        var bound = 2.5f;
                        var boundsObjX = spawnedGameObject.GetComponent<Collider>().bounds.extents.x;

                        var scaleObj = 0f;
                        //working number for scaling.
                        var magicNumber = 40;
                        //checks what side of the object is larger and scales accordingly
                        //places objects at a slight angle to give it a better look
                        if (spawnedGameObject.GetComponent<Collider>().bounds.extents.y > boundsObjX)
                        {
                            boundsObjX = spawnedGameObject.GetComponent<Collider>().bounds.extents.y;
                            scaleObj = bound / boundsObjX * magicNumber;
                            spawnedGameObject.transform.localScale *= scaleObj;
                            spawnedGameObject.transform.localEulerAngles = new Vector3(-15, 50, -15);
                        }
                        else if (spawnedGameObject.GetComponent<Collider>().bounds.extents.z > boundsObjX)
                        {
                            boundsObjX = spawnedGameObject.GetComponent<Collider>().bounds.extents.z;
                            scaleObj = bound / boundsObjX * magicNumber;
                            spawnedGameObject.transform.localScale *= scaleObj;
                            spawnedGameObject.transform.localEulerAngles = new Vector3(-15, 50, -15);
                        }
                        else
                        {
                            scaleObj = bound / boundsObjX * magicNumber;
                            spawnedGameObject.transform.localScale *= scaleObj;
                            spawnedGameObject.transform.localEulerAngles = new Vector3(-15, 50, -15);
                        }


                        // checks if the chosen object has an exploded "twin" and instantiates it if this is the case.
                        GameObject objExplode = null;
                        foreach (Transform t in chosenGameObject.transform.parent.GetComponentInChildren<Transform>(true))
                        {
                            if (t.gameObject.name == objNameExplode)
                            {
                                objExplode = Instantiate(t.gameObject);
                            }
                        }
                        // if this is the case, give the exploded object the same position, scale and rotation as the original.
                        if (objExplode != null)
                        {
                            //var objExplode = GameObject.Find(objNameExplode);
                            objExplode.transform.eulerAngles = arCameraLocation.eulerAngles - new Vector3(-90.0f, 0.0001f, 0.0001f);
                            objExplode.transform.position = resultingPosition;
                            objExplode.transform.parent = parts.transform;
                            objExplode.SetActive(false);
                            objExplode.AddComponent<PositionController>();
                            objExplode.transform.localScale = scale;

                            objExplode.transform.localScale = spawnedGameObject.transform.localScale;
                            objExplode.transform.localEulerAngles = spawnedGameObject.transform.localEulerAngles;
                            objExplode.AddComponent<objectManipulation>();

                        }


                        // add the darker background.
                        SceneBackground(darkenBgPart);
                        //add objectManipulation to rotate the object
                        spawnedGameObject.AddComponent<objectManipulation>();
                        // disable the ability to further expand on the tracking of the groundplane and pointcloud
                        arCameraObject.transform.parent.GetComponent<ARPlaneManager>().enabled = false;
                        arCameraObject.transform.parent.GetComponent<ARPointCloudManager>().enabled = false;
                        // disable the visible ground plane.
                        trackablesObj.SetActive(false);
                        ChangeMeshActive();
                        // changes state
                        partState = 1;
                    }
                }
                check = false;
            }
            else
            {
                check = true;
            }
        }
    }

    /// <summary>
    /// instantiates individual part
    /// </summary>
    private void GetInfoPart()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //raycast
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //more raycasts
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                //excludes any trackables from being instanced. you don't want to be able to rotate the mapping plane :)
                if (hit.collider.gameObject.transform.parent.name != "Trackables")
                {
                    UnityMessageManager.Instance.SendMessageToRN(new UnityMessage()
                    {
                        name = "To info",
                        callBack = (data) =>
                        {
                            Debug.Log("onClickCallBack:" + data);
                        }
                    });
                    //Hides part
                    HidePart();

                    var arCameraObject = GameObject.Find("AR Session Origin/AR Camera");
                    var arCameraObjectLocation = arCameraObject.transform;
                    var chosenGameObject = hit.collider.gameObject;
                    var parentClicked = hit.collider.gameObject.transform.parent.transform.parent.transform.localScale;

                    //instantiate, places, rotates and scales the chosen part 
                    var objClicked = Instantiate(chosenGameObject);
                    objClicked.transform.eulerAngles = arCameraObjectLocation.eulerAngles - new Vector3(-90.0f, 0.0001f, 0.0001f);
                    objClicked.transform.parent = infoParts.transform;
                    objClicked.SetActive(true);
                    objClicked.AddComponent<PositionController>();
                    scale = objClicked.transform.localScale;
                    objClicked.transform.localScale = parentClicked;
                    objClicked.transform.localEulerAngles = new Vector3(0, 0, 0);

                    // fictional bound to place the part in
                    var bound = 2.5f;
                    var boundsObjX = objClicked.GetComponent<Collider>().bounds.extents.x;

                    var scaleObj = 0f;
                    var magicNumber = 80;
                    //scales and places the object where it should.
                    if (objClicked.GetComponent<Collider>().bounds.extents.y > boundsObjX)
                    {
                        bound = 2;
                        boundsObjX = objClicked.GetComponent<Collider>().bounds.extents.y;
                        scaleObj = bound / boundsObjX * magicNumber;
                        objClicked.transform.localScale *= scaleObj;
                        objClicked.transform.localEulerAngles = new Vector3(0, 0, 0);
                    }
                    else if (objClicked.GetComponent<Collider>().bounds.extents.z > boundsObjX)
                    {
                        boundsObjX = objClicked.GetComponent<Collider>().bounds.extents.z;
                        scaleObj = bound / boundsObjX * magicNumber;
                        objClicked.transform.localScale *= scaleObj;

                        objClicked.transform.localEulerAngles = new Vector3(0, 0, 0);
                    }
                    else
                    {
                        scaleObj = bound / boundsObjX * magicNumber;
                        objClicked.transform.localScale *= scaleObj;
                        objClicked.transform.localEulerAngles = new Vector3(0, 0, 0);
                    }
                    if (objClicked.transform.localScale.x >= 0.1)
                    {
                        objClicked.transform.localScale /= 10;
                        if (objClicked.transform.localScale.x >= 0.1)
                        {
                            objClicked.transform.localScale /= 10;
                        }
                    }

                    objClicked.transform.position = objClicked.transform.position * 0;
                    objClicked.AddComponent<objectManipulation>();

                    // changes state.
                    partState = 2;
                }
            }
        }
    }

    
    /// <summary>
    /// places background on the right position
    /// </summary>
    /// <param name="darkenBgLocation"> Location for the background</param>
    private void SceneBackground(GameObject darkenBgLocation)
    { 
        if(GameObject.Find("AR Session Origin/AR Camera/DarkenBgParts").transform.childCount == 0)
        {
            var arCameraGameObject = GameObject.Find("AR Session Origin/AR Camera");
            var arCameraGameObjectLocation = arCameraGameObject.transform;
            var resultingPosition = arCameraGameObjectLocation.position + arCameraGameObjectLocation.forward * 500;
            var backgroundObject = Instantiate(blurPlanePrefab);
            backgroundObject.transform.eulerAngles = arCameraGameObjectLocation.eulerAngles - new Vector3(-90.0f, 0.0001f, 0.0001f);
            backgroundObject.transform.position = resultingPosition;
            backgroundObject.transform.parent = darkenBgLocation.transform;
            backgroundObject.SetActive(true);
            backgroundObject.transform.localEulerAngles = new Vector3(0, 0, 0);
        }      
    }

    /// <summary>
    /// Sets Machine object back to active
    /// Destroys all children of parts
    /// </summary>
    private void DestroyParts()
    {
        machine[chosenObject].transform.GetChild(0).gameObject.SetActive(true);
        foreach (Transform child in parts.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in darkenBgPart.transform)
        {
            Destroy(child.gameObject);
        }
    }

    /// <summary>
    /// Hides Parts
    /// </summary>
    private void HidePart()
    {
        parts.SetActive(false);
    }

    /// <summary>
    /// Shows Parts
    /// </summary>
    private void ShowPart()
    {
        parts.SetActive(true);
    }

    /// <summary>
    /// Hides Machine Object
    /// </summary>
    private void HideObject()
    {
        var GO = machine[chosenObject];
        GO.SetActive(false);
        scale = GO.transform.localScale;
    }

    /// <summary>
    /// Destroys Info parts
    /// </summary>
    private void DestroyInfoPart()
    {
        foreach (Transform child in infoParts.transform)
        {
            Destroy(child.gameObject);
        }
    }

    /// <summary>
    /// changes part mesh (clickables become clickable)
    /// </summary>
    public void ChangeMeshActive()
    {
        var objectPart = parts.transform;
        foreach (Transform part in objectPart)
        {
            part.GetComponent<Collider>().enabled = false;
            foreach (Transform child in part)
            {
                foreach (Transform child2 in child.transform)
                {
                    if (child2.GetComponent<Collider>() != null)
                    {
                        child2.GetComponent<Collider>().enabled = true;
                    }
                }
            }
        }

        
    }
    /// <summary>
    /// changes part mesh (Part becomes clickable, clickables do not.
    /// </summary>
    public void ChangeMeshActivePart()
    {
        var objectPart = parts.transform;
        foreach (Transform part in objectPart)
        {
            part.GetComponent<Collider>().enabled = true;
            foreach (Transform child in part)
            {
                foreach (Transform child2 in child.transform)
                {
                    if (child2.GetComponent<Collider>() != null)
                    {
                        child2.GetComponent<Collider>().enabled = false;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Return to Part state
    /// </summary>
    public void ReturnToPart()
    {
        DestroyInfoPart();
        ShowPart();
        partState = 1;
    }

    /// <summary>
    /// Return to Machine object state
    /// </summary>
    public void ReturnToMachine()
    {
        var backgroundPlaneObj = GameObject.Find("AR Session Origin/AR Camera");
        var trackablesObj = GameObject.Find("AR Session Origin/Trackables");
        DestroyParts();
        backgroundPlaneObj.transform.parent.GetComponent<ARPlaneManager>().enabled = true;
        backgroundPlaneObj.transform.parent.GetComponent<ARPointCloudManager>().enabled = true;
        trackablesObj.SetActive(true);
        partState = 0;
    }

    public void Reset()
    {
        partState = 0;
        chosenObject = 5;
        try
        {
            var machine = GameObject.Find("Machine");
            var component = machine.transform.GetChild(0).GetComponent<objectManipulation>();
            Destroy(component);
            machine.transform.GetChild(0).gameObject.SetActive(false);
            machine.transform.GetChild(0).parent = null;
        }
        catch (NullReferenceException)
        {
            //do nothing if machine is null
        }

        var sessionOriginObject = GameObject.Find("AR Session Origin");
        sessionOriginObject.GetComponent<AutoPlaceItem>().enabled = true;
        foreach(Transform child in sessionOriginObject.transform)
        {
            child.gameObject.SetActive(true);
        }
        var arCam = GameObject.Find("AR Session Origin/AR Camera");
        foreach (Transform child in arCam.transform)
        {
            child.gameObject.SetActive(true);
        }
        try
        {
            var parts = GameObject.Find("AR Session Origin/AR Camera/Parts");
            foreach (Transform child in parts.transform)
            {
                Destroy(child.gameObject);
            }
        }
        catch (Exception)
        {
            throw;
        }
        try
        {
            var infoparts = GameObject.Find("AR Session Origin/AR Camera/InfoParts");
            foreach (Transform child in infoparts.transform)
            {
                Destroy(child.gameObject);
            }
        }
        catch
        {

        }
        try
        {
            var darkBG = GameObject.Find("AR Session Origin/AR Camera/DarkenBgParts");
            foreach (Transform child in darkBG.transform)
            {
                Destroy(child.gameObject);
            }
        }
        catch
        {

        }

    }
}
