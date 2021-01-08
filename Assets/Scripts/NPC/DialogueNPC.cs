using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;
public class DialogueNPC : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    public Player player;
    public string dialogueToRun;

    private bool runDialogue;
    // Start is called before the first frame update
    void Start()
    {
        runDialogue = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (runDialogue){
            if (Input.GetKeyDown(KeyCode.Space)){
                if (!dialogueRunner.IsDialogueRunning){
                    dialogueRunner.StartDialogue(dialogueToRun);
                    runDialogue = false;
                }
            }
        }
    }

    void OnTriggerStay(Collider collider)                          //https://www.youtube.com/watch?v=Bc9lmHjqLZc
    {
        if (collider.gameObject.CompareTag("Player") && !runDialogue)
        {
            runDialogue = true;
        }
    }

    void OnTriggerExit(Collider collider){
        if (collider.gameObject.CompareTag("Player"))
        {
            runDialogue = false;
        }
    }
}
