using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reactnative : MonoBehaviour
{
    public AutoPlaceItem scriptAutoPlaceItem;
    public ClickManager scriptClickManager;
    public Explode scriptExplode;
    public ResetScript scriptResetScript;
    private string guiDebugText = "test";
    private GUIStyle guiStyle = new GUIStyle(); //create a new variable

    /// <summary>
    /// gets called every time the script activates.
    /// Adds message
    /// </summary>
    private void Awake()
    {
        UnityMessageManager.Instance.OnRNMessage += OnMessage;
    }

    /// <summary>
    /// Removes message
    /// </summary>
    private void onDestroy()
    {
        UnityMessageManager.Instance.OnRNMessage -= OnMessage;
    }

    /// <summary>
    /// gets called whenever Unity client gets message from React-Native
    /// Switch case populated with possible messages from React-Native front-end.
    /// </summary>
    private void OnMessage(MessageHandler message)
    {
        var data = message.getData<string>();
        var name = message.name;

        switch (name)
        {
            case "Layer 1":
                scriptAutoPlaceItem.ViewLayer1();
                break;
            case "Layer 2":
                scriptAutoPlaceItem.ViewLayer2();
                break;
            case "All Layers":
                scriptAutoPlaceItem.ViewAllLayers();
                break;
            case "Return to part":
                scriptClickManager.ReturnToPart();
                break;
            case "Return to machine":
                scriptClickManager.ReturnToMachine();
                break;
            case "Explode":
                scriptExplode.ExplodeObjToggle();
                break;
            case "Herstel van onderdeel X":
                scriptClickManager.chosenObject = 0;
                scriptAutoPlaceItem.chosenObject = 0;
                break;
            case "Algemene controle":
                scriptClickManager.chosenObject = 1;
                scriptAutoPlaceItem.chosenObject = 1;
                break;
            case "Kwaliteitscontrole":
                scriptClickManager.chosenObject = 2;
                scriptAutoPlaceItem.chosenObject = 2;
                break;
            case "Reset":
                scriptResetScript.ResetScene();
                break;
            default:
                break;
        }

        guiDebugText = name;
    
        
        Debug.Log("onMessage:" + data);
        message.send(new { CallbackTest = "I am Unity callback" });
    }
    private void OnGUI()
    {
        guiStyle.fontSize = 30; //change the font size
        GUI.Label(new Rect(10, 10, 100, 20), guiDebugText,guiStyle);
        //if(GUI.Button(new Rect(20,20,150, 150),"choose Object")){
        //    var randomNumber = Random.Range(0, 2);
        //    scriptClickManager.chosenObject = randomNumber;
        //    scriptAutoPlaceItem.chosenObject = randomNumber;
        //}

        //if (GUI.Button(new Rect(20, 170, 150, 150), "Reset"))
        //{
        //    scriptResetScript.ResetScene();
        //}
    }
}
