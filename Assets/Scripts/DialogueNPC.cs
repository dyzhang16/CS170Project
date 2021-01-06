using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;
public class DialogueNPC : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    public Player player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D collider)                          //https://www.youtube.com/watch?v=Bc9lmHjqLZc
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            dialogueRunner.StartDialogue("Introduction");
        }
    }
}
