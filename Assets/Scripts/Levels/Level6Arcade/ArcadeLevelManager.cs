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
            if (GameManager.instance.arcadeFirstCrane == 1)
            {
                tracker.NodeComplete("CraneDirections");
            }
            if (GameManager.instance.arcadeFirstDance == 1)
            {
                tracker.NodeComplete("DanceDirections");
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartUpdateBetween());
    }

    // Hacky fix for ensuring that the start node dialogue is running
    IEnumerator LateStartDialogueAuto()
    {
        yield return null;
        dialogueRunner.StartDialogue(dialogueRunner.startNode);
    }

    // Hacky fix #2, the in-between between Start and Update
    //  Call this in Start()
    IEnumerator StartUpdateBetween()
    {
        yield return null;
        if (GameManager.instance != null)
        {
            GameManager.instance.loadItems();
            GameManager.instance.deleteItems();

            if (GameManager.instance.arcadeNoCraneDirs == 1)
            {
                dialogueRunner.variableStorage.SetValue("$doNotShowCraneDirs", true);
            }
            if (GameManager.instance.arcadeNoDanceDirs == 1)
            {
                dialogueRunner.variableStorage.SetValue("$doNotShowDanceDirs", true);
            }
        }
    }
}
