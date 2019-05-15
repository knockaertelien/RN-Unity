using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScript : MonoBehaviour
{
    public ClickManager clickManager;
    public AutoPlaceItem autoPlaceItem;
    
    public void ResetScene()
    {
        clickManager.Reset();
        autoPlaceItem.Reset();
    }
}
