using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class OfficeHub : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    public VariableStorageBehaviour CustomVariableStorage;
    public GameObject player, Documents;
    public GameObject exitToCityOffice;
    public GameObject desk;

    void Awake()
    {
        if (GameManager.instance != null)
        {
            if (GameManager.instance.previousScene == "OfficeScene")
            {
                player.transform.position = desk.transform.position + new Vector3(0, 0, -10);
            }
            if (GameManager.instance.followFriendinOffice == 1)
            {
                dialogueRunner.startAutomatically = false;
                //code to move the friend to the right area
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
            if (GameManager.instance.officePuzzle == 1)
            {
                CustomVariableStorage.SetValue("$TookMDocument", 1);
                CustomVariableStorage.SetValue("$TookEDocument", 1);
                CustomVariableStorage.SetValue("$TookMaxDocument", 1);
            } else if (GameManager.instance.officeDeskPuzzle == 1){
                exitToCityOffice.GetComponent<RunDialogue>().dialogueToRun = "LeavingOfficeHub";
            }
        }
    }
}
