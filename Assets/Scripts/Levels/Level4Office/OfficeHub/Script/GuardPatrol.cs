using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class GuardPatrol : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    public string dialogueToRun;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Document") && !dialogueRunner.IsDialogueRunning)
        {
            GetComponentInParent<OfficeGuard>().isWalking = false;
            dialogueRunner.StartDialogue(dialogueToRun);
        }
    }

}
