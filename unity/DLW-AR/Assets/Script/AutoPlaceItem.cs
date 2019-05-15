using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARFoundation;


public class AutoPlaceItem : MonoBehaviour
{
    public GameObject[] machineObject;
    public int chosenObject = 5;
    private bool placed = false;
    private Vector3 hitPose;
    public GameObject machineHolder;
    public LayerMask layerMask;
    public GameObject testingFloor;
    public GameObject layer1;
    public GameObject layer2;


    

    /// <summary>
    /// rotates object to camera
    /// if placed, prepares mesh and adds manipulation.
    /// then disables this script
    /// </summary>
    /// <param name="hitPoint"></param>
    private void ObjectPlacer(Vector3 hitPoint)
    {
        if (chosenObject < 4)
        {


            if (!placed)
            {
                FollowCamera(hitPoint);
                // look at camera...
                machineObject[chosenObject].transform.LookAt(Camera.main.transform.position);
                // then lock rotation to Y axis only...
                machineObject[chosenObject].transform.localEulerAngles = new Vector3(0, machineObject[chosenObject].transform.localEulerAngles.y, 0);
            }
            else
            {
                ChangeMeshInactive();
                machineObject[chosenObject].AddComponent<objectManipulation>();
                GetComponent<AutoPlaceItem>().enabled = false;
            }
        }
    }

    /// <summary>
    /// prepares mesh to be able to get clicked on
    /// </summary>
    private void ChangeMeshInactive()
    {
        foreach (Transform child in machineHolder.transform)
        {
            //child1 = turbine
            foreach (Transform child2 in child.transform)
            {
                //child2 = layers
                foreach (Transform child3 in child2.transform)
                {
                    //child3 = objects
                    if (child3.GetComponent<Collider>() != null)
                    {
                        child3.GetComponent<Collider>().enabled = true;
                    }
                    foreach (Transform child4 in child3.transform)
                    {
                        //child4 = clickables
                        foreach (Transform child5 in child4.transform)
                        {
                            // child5 = parts
                            if (child5.GetComponent<Collider>() != null)
                            {
                                child5.GetComponent<Collider>().enabled = false;
                            }
                        }
                    }
                }
            }
        }
    }

    // on awake
    private void Awake()
    {
        sessionOrigin = GetComponent<ARSessionOrigin>();

        if (Application.isEditor)
        {
            testingFloor.SetActive(true);
        }
    }
    
    /// <summary>
    /// Demo for meeting
    /// </summary>
    public void ViewLayer1()
    {
        layer1.SetActive(true);
        layer2.SetActive(false);
    }
    /// <summary>
    /// Demo for meeting
    /// </summary>
    public void ViewAllLayers()
    {
        layer1.SetActive(true);
        layer2.SetActive(true);
    }
    /// <summary>
    /// Demo for meeting
    /// </summary>
    public void ViewLayer2()
    {
        layer1.SetActive(false);
        layer2.SetActive(true);
    }

    //changes behaviour of script in editor vs app (to be able to debug in editor)
    private void Update()
    {
        if (chosenObject <4)
        {

        
            if (machineObject[chosenObject].transform.parent != null)
            {
                placed = false;
            }

            if (Application.isEditor)
            {
                if (Input.touchCount >= 1 || Input.GetMouseButtonDown(0))
                {
                    placed = true;
                }

                Ray ray = Camera.main.ScreenPointToRay(new Vector3(Camera.main.pixelWidth * 0.5f, Camera.main.pixelHeight * 0.5f, 0f));

                if (Physics.Raycast(ray, out RaycastHit hit, 500f, layerMask))
                {
                    ObjectPlacer(hit.point);
                }

            }
            else
            {
                if (Input.touchCount >= 1)
                {
                    placed = true;
                }
                if (sessionOrigin.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)), hits, TrackableType.PlaneWithinPolygon))
                {              
                    var hitPose = hits[0].pose;
                    ObjectPlacer(hitPose.position);                
                }

            }
        }
    }

    /// <summary>
    /// follow the camera
    /// </summary>
    /// <param name="newPos"></param>
    private void FollowCamera(Vector3 newPos)
    {
        machineObject[chosenObject].transform.parent = machineHolder.transform;

        machineObject[chosenObject].transform.position = newPos;
        machineObject[chosenObject].transform.rotation = Quaternion.identity;

        if (!machineObject[chosenObject].activeInHierarchy)
        {
            machineObject[chosenObject].SetActive(true);
        }
    }

    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    ARSessionOrigin sessionOrigin;

    public void Reset()
    {
        chosenObject = 5;
        placed = false;
    }
}
