using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectManipulation : MonoBehaviour
{
    private Touch touch;
    private Vector2 touchPosition;
    private Quaternion rotationY;
    private float rotateSpeedModifier = 0.1f;
    private float defaultScaleX, defaultScaleY, defaultScaleZ;
    private float rotSpeed = 200;

    private void Start()
    {
        defaultScaleX = transform.localScale.x;
        defaultScaleY = transform.localScale.y;
        defaultScaleZ = transform.localScale.z;
    }

    /// <summary>
    /// Works with mouse, only usable in Unity editor, remove when going live
    /// </summary>
    void OnMouseDrag()
    {
        float rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
        float rotY = Input.GetAxis("Mouse Y") * rotSpeed * Mathf.Deg2Rad;
        transform.Rotate(Vector3.up, -rotX);
        transform.Rotate(Vector3.right, rotY);
    }

    //checks for input
    private void Update()
    {
        //one finger
        if (Input.touchCount == 1)
        {
            //gets the touch
            touch = Input.GetTouch(0);

            //if touch moved position, rotate at rotateSpeedModifier pace
            if (touch.phase == TouchPhase.Moved)
            {
                rotationY = Quaternion.Euler(0f, -touch.deltaPosition.x * rotateSpeedModifier, touch.deltaPosition.y * rotateSpeedModifier);
                transform.rotation = rotationY * transform.rotation;
            }
        }
        // if input is 2 fingers
        if (Input.touchCount == 2)
        {
            //get them both
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);
            // only scales when both fingers are moving. 
            //should be different or is ok?
            if (touch0.phase == TouchPhase.Moved && touch1.phase == TouchPhase.Moved)
            {
                Vector2 prevDist = (touch0.position - touch0.deltaPosition) - (touch1.position - touch1.deltaPosition);
                Vector2 curDist = touch0.position - touch1.position;
                float delta = curDist.magnitude - prevDist.magnitude;
                if (delta > 0)
                {
                    if (defaultScaleX >= transform.localScale.x * 4)
                    {

                    }
                    else if (defaultScaleX < transform.localScale.x - 0.1)
                    {

                    }
                    else if (defaultScaleX >= transform.localScale.x)
                    {

                        defaultScaleX += defaultScaleX/10;
                        defaultScaleY += defaultScaleY/10;
                        defaultScaleZ += defaultScaleZ/10;
                        transform.localScale = new Vector3(defaultScaleX, defaultScaleY, defaultScaleZ);
                    }
                    else
                    {

                        defaultScaleX += defaultScaleX / 10;
                        defaultScaleY += defaultScaleY / 10;
                        defaultScaleZ += defaultScaleZ / 10;
                        transform.localScale = new Vector3(defaultScaleX, defaultScaleY, defaultScaleZ);
                    }
                }
                else if (delta < 0)
                {

                    if (defaultScaleX <= transform.localScale.x)
                    {
                        defaultScaleX -= defaultScaleX / 10;
                        defaultScaleY -= defaultScaleY / 10;
                        defaultScaleZ -= defaultScaleZ / 10;
                        transform.localScale = new Vector3(defaultScaleX, defaultScaleY, defaultScaleZ);
                    }
                    else if (defaultScaleX > transform.localScale.x)
                    {

                        defaultScaleX -= defaultScaleX / 10;
                        defaultScaleY -= defaultScaleY / 10;
                        defaultScaleZ -= defaultScaleZ / 10;
                        transform.localScale = new Vector3(defaultScaleX, defaultScaleY, defaultScaleZ);
                    }
                    
                }
            }
        }
    }
}

