using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class ArcadeLevelManager : MonoBehaviour
{
    public NodeVisitedTracker tracker;

    public DialogueRunner dialogueRunner;
    public bool startAutomaticallyLate = false;

    void Awake()
    {
        if (dialogueRunner != null && startAutomaticallyLate && !dialogueRunner.IsDialogueRunning && dialogueRunner.startNode != Yarn.Dialogue.DEFAULT_START)
        {
            StartCoroutine(LateStartDialogueAuto());
        }
        if (GameManager.instance != null)
        {
            // if we already visited the arcade, do not start the node automatically
            if (GameManager.instance.arcadeFirstVisit == 1)
            {
                dialogueRunner.startAutomatically = false;
                tracker.NodeComplete("ArcadeStart");
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.loadItems();
            GameManager.instance.deleteItems();
        }
    }

    // Hacky fix for ensuring that the start node dialogue is running
    IEnumerator LateStartDialogueAuto()
    {
        yield return null;
        dialogueRunner.StartDialogue(dialogueRunner.startNode);
    }
}
