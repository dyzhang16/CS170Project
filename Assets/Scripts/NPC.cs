using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yarn.Unity.Dialogue
{
    public class NPC : MonoBehaviour
    {
    public string NPCName = "";
    public string NPCInteract = "";

    public YarnProgram scriptToLoad;
        void Start()
        {
            if (scriptToLoad != null)
            {
                DialogueRunner dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
                dialogueRunner.Add(scriptToLoad);
            }
        }
    }
}
