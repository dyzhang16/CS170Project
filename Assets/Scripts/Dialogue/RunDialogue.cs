using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;


public class RunDialogue : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    public Player player;
    public string dialogueToRun;
    public bool startInstantly;
    public string NPCName = "";
    public string NPCInteract = "";

    private BoxCollider box;

    private bool runDialogue;
    // Start is called before the first frame update
    void Start()
    {
        runDialogue = false;
        box = GetComponent<BoxCollider>();
        if (box == null){
            box = transform.Find("collider").GetComponent<BoxCollider>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (runDialogue)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!dialogueRunner.IsDialogueRunning && player.allowMovement)
                {
                    dialogueRunner.StartDialogue(dialogueToRun);

                    if (box.isTrigger){
                        runDialogue = false;
                    }
                    // Debug.Log("running" + dialogueToRun);
                }
            }
        }
    }

    void OnTriggerStay(Collider collider)                          //https://www.youtube.com/watch?v=Bc9lmHjqLZc
    {
        if (startInstantly)
        {
            dialogueRunner.StartDialogue(dialogueToRun);
            startInstantly = false;
        }
        else
        {
            if (collider.gameObject.CompareTag("Player") && !runDialogue)
            {
                runDialogue = true;
            }
        }

    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            runDialogue = false;
        }
    }

    void OnCollisionEnter(Collision collision){
        if (startInstantly)
        {
            dialogueRunner.StartDialogue(dialogueToRun);
            startInstantly = false;
        }
        else 
        {
            if (collision.gameObject.CompareTag("Player") && !runDialogue)
            {
                runDialogue = true;
            } 
        }
    }

    void OnCollisionExit(Collision collision){
        if (collision.gameObject.CompareTag("Player"))
        {
            runDialogue = false;
        }
    }
}
