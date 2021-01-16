using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

namespace Yarn.Unity.Dialogue
{
    public class LoadDialogue : MonoBehaviour
    {
        public int size;
        public YarnProgram[] scriptToLoad = new YarnProgram[10];
        void Start()
        {
            if (scriptToLoad != null)
            {
                for (int i = 0; i < size; i++)
                {
                    DialogueRunner dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
                    dialogueRunner.Add(scriptToLoad[i]);
                    Debug.Log(scriptToLoad[i]);
                }
            }
        }
    }
}

