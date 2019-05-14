using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionController : MonoBehaviour
{

    private Transform cameraTransform;
    private float distanceFromCamera = 300;
    private Vector3 resultingPosition;

    private void Start()
    {
        //gets camera location
        cameraTransform = GameObject.Find("AR Session Origin/AR Camera").gameObject.transform;
    }

    // Update is called once per frame
    private void Update()
    {
        
        // places parts at the right distance from the camera.
        if (transform.parent.name == "InfoParts")
        {
            resultingPosition = cameraTransform.position + cameraTransform.forward * 20 + cameraTransform.up *5;
            transform.position = resultingPosition;
        }
        else
        {
            resultingPosition = cameraTransform.position + cameraTransform.forward * distanceFromCamera;
            transform.position = resultingPosition;
        }
        
    }

}
