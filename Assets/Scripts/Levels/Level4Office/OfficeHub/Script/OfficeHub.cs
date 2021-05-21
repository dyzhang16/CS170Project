using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class OfficeHub : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    public VariableStorageBehaviour CustomVariableStorage;
    public GameObject player, friend;
    public GameObject playerPosition, friendPosition;
    public GameObject documentA, documentB, documentC;

    void Awake()
    {
        if (GameManager.instance != null)
        {
            if (GameManager.instance.previousScene == "OfficeScene" && GameManager.instance.officeDeskPuzzle == 1)
            {
                player.transform.position = playerPosition.transform.position;
                friend.transform.position = friendPosition.transform.position;
                friend.GetComponent<RunDialogue>().dialogueToRun = ("doneWithWork");
            }
        }

        if (MusicManagerScript.instance != null){
            MusicManagerScript.instance.sceneChecker();
        }
    }
    void Start()
    {
        if (GameManager.instance != null)
        {
            if (GameManager.instance.previousScene == "OfficeScene" && GameManager.instance.officeDeskPuzzle == 1)
            {
                GameObject obj = GameObject.Find("DoorApproach");
                Destroy(obj);
                dialogueRunner.startNode = "doneWithWork";
                dialogueRunner.startAutomatically = true;
                Destroy(documentA);
                Destroy(documentB);
                Destroy(documentC);
            }

            if (GameManager.instance.clearInventory){
                GameManager.instance.deleteItems();
                GameManager.instance.clearItems();
                GameManager.instance.clearInventory = false;
            } else {
                GameManager.instance.loadItems();
                GameManager.instance.deleteItems();
            }
        }
    }
}
