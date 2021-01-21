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
        // Bottom URL links to how to block UI stuff
        // https://answers.unity.com/questions/822273/how-to-prevent-raycast-when-clicking-46-ui.html?childToView=862598#answer-862598
        if (!dialogueRunner.IsDialogueRunning && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            dialogueRunner.StartDialogue(dialogueToRun);
            Debug.Log("running" + dialogueToRun);
        }
    }
}
