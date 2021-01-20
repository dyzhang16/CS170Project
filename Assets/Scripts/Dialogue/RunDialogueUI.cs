using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Yarn.Unity;

public class RunDialogueUI : MonoBehaviour , IPointerDownHandler
{
    public DialogueRunner dialogueRunner;
    public string dialogueToRun;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!dialogueRunner.IsDialogueRunning)
        {
            dialogueRunner.StartDialogue(dialogueToRun);
            Debug.Log("running" + dialogueToRun);
        }
    }
}
