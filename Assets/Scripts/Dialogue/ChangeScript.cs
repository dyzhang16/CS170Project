using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class ChangeScript : MonoBehaviour
{
    //Attach to Object that needs to Change it's own script 
    //Useful for One Script
    public DialogueRunner dialogueRunner;       
    public GameObject GameObject;               //GameObject's script that needs to be changed
    public string diaToRun;                     //script to run
    [YarnCommand("ChangeScript")]
    public void ChangeScriptToRun() 
    {
        //checks which type of RunDialogue Script exists and sets the dialogue accordingly. else returns error
        if (GetComponent<RunDialogue>())
        {
            GetComponent<RunDialogue>().dialogueToRun = diaToRun;
        }
        else if (GetComponent<RunDialogueMC>())
        {
            GetComponent<RunDialogueMC>().dialogueToRun = diaToRun;
        }
        else if (GetComponent<RunDialogueUI>())
        {
            GetComponent<RunDialogueUI>().dialogueToRun = diaToRun;
        }
        else 
        {
            // Debug.Log("Run Dialogue Script does not exist");
        }
    }
}
