using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Office : MonoBehaviour
{

    public NodeVisitedTracker tracker;
    public VariableStorageBehaviour CustomVariableStorage;
    public DialogueRunner dialogueRunner;

    public GameObject pen;


    void Awake()
    {
        if (GameManager.instance != null)
        {
            if (GameManager.instance.officeDeskPuzzle == 1)
            {
                //completed gravestone dialogue and completed puzzle
                //Debug.Log(CustomVariableStorage.GetValue("$checkpoint_1"));
                // CustomVariableStorage.SetValue("$checkpoint_1", 1);
                //Debug.Log(CustomVariableStorage.GetValue("$checkpoint_1"));
                // CustomVariableStorage.SetValue("$puzzle", 1);

                dialogueRunner.startNode = "GoHome";
                dialogueRunner.startAutomatically = true;

                // tracker.NodeComplete("prologue_begin");

                //has key
                // CustomVariableStorage.SetValue("$key", 1);



                //destroy key
                Destroy(pen);


            }
        }
    }
}
