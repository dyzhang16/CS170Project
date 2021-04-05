using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class DragTutorial : MonoBehaviour
{

    public DialogueRunner dialogueRunner;
    public bool visit = false;

    //this is for the tutorial dialogue to teach the player how to drag flowers onto the inventory
    [YarnCommand("Drag")]
    public void DragDialogue(){
        if (Inventory.instance.CheckItem("White Flower") || Inventory.instance.CheckItem("Red Flower") || 
        Inventory.instance.CheckItem("Yellow Flower") || Inventory.instance.CheckItem("Purple Flower")) {
            if (!visit) {
                dialogueRunner.StartDialogue("dragTutorial");
                visit = true;
            }
        }
    }
}
