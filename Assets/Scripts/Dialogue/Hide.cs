using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Hide : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    // Start is called before the first frame update
    void Awake()
    {
        dialogueRunner.AddCommandHandler(
        "HidePanel",     // the name of the command
        HidePanel // the method to run
        );

    }
    private void HidePanel (string[] parameters)
    {
        string Object = parameters[0];
        GameObject PotentialObject = GameObject.Find(Object);
        // Debug.Log("Hiding:" + PotentialObject);
        PotentialObject.GetComponent<CanvasGroup>().alpha = 0;
        PotentialObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
        GameObject blocker = GameObject.Find("Canvas/Blocker");
        if (blocker)
        {
            blocker.GetComponent<CanvasGroup>().alpha = 0;
            blocker.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }
}
