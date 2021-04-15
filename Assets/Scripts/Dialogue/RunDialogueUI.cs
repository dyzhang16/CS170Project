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

    //hover
    void OnPointerEnter(){
        Cursor.SetCursor(GameManager.instance.cursorHoverTexture, Vector2.zero, CursorMode.Auto);
    }

    void OnPointerExit(){
        Cursor.SetCursor(GameManager.instance.cursorTexture, Vector2.zero, CursorMode.Auto);
    }
}
