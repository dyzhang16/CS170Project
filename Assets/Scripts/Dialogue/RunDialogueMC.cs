using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class RunDialogueMC : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    public string dialogueToRun;
    public void OnMouseDown()
    {
        if (!dialogueRunner.IsDialogueRunning)
        {
            dialogueRunner.StartDialogue(dialogueToRun);
            Debug.Log("running" + dialogueToRun);
        }
    }
}
