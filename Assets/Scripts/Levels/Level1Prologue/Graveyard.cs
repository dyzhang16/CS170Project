using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Graveyard : MonoBehaviour
{

    public NodeVisitedTracker tracker;
    public VariableStorageBehaviour CustomVariableStorage;

    public GameObject player;
    public GameObject gravekeeper;
    public GameObject gravestone;
    public GameObject flowerA;
    public GameObject flowerB;
    public GameObject flowerC;
    public GameObject flowerD;
    public GameObject bouqet;
    public GameObject key;
    public GameObject gateApproach;
    public GameObject gate;

    public DialogueRunner dialogueRunner;
    public GameObject fred;
    public GameObject fredGrave;
    public GameObject outside;

    void Start(){
        if (GameManager.instance != null){
            if (GameManager.instance.flowerPuzzle == 1){
                //completed gravestone dialogue and completed puzzle
                //Debug.Log(CustomVariableStorage.GetValue("$checkpoint_1"));
                // CustomVariableStorage.SetValue("$checkpoint_1", 1);
                //Debug.Log(CustomVariableStorage.GetValue("$checkpoint_1"));
                // CustomVariableStorage.SetValue("$puzzle", 1);

                gravestone.GetComponent<RunDialogue>().dialogueToRun = "returnToGrave";
                gate.GetComponent<RunDialogue>().dialogueToRun = "return_gate";

                // tracker.NodeComplete("prologue_begin");

                //has key
                // CustomVariableStorage.SetValue("$key", 1);

                //destroy gravekeeper
                gravekeeper.SetActive(false);

                //destroy flowers
                Destroy(flowerA);
                Destroy(flowerB);
                Destroy(flowerC);
                Destroy(flowerD);

                //destroy key
                Destroy(key);

                //reveal bouqet
                bouqet.SetActive(true);

                //gravestone start instantly = false;
                RunDialogue dia = gravestone.GetComponent<RunDialogue>();
                dia.startInstantly = false;

                //set player position to originial position in gamemanager.instance;
                Vector3 pos = new Vector3((gateApproach.transform.position.x - 20), 
                    gateApproach.transform.position.y, gateApproach.transform.position.z);
                player.transform.position = pos;
                //destroy gate approach
                Destroy(gateApproach);

                Player p = player.GetComponent<Player>();
                p.goingToFade = false;
                p.allowMovement = true;
            }

            if (GameManager.instance.ending == 1){
                StartCoroutine(objActive());
                //add friend's grave
                fredGrave.SetActive(true);
                //change gate dialogue
                gate.GetComponent<RunDialogue>().dialogueToRun = "NoGate";
                //change gravestone dialogue
                gravestone.GetComponent<RunDialogue>().dialogueToRun = "YourGrave";
                gravestone.GetComponent<RunDialogue>().startInstantly = false;

                //change player positions
                player.transform.position = player.transform.position + new Vector3(-40, 0, 0);
                fred.transform.position = player.transform.position + new Vector3(10, 0, 0);

                //start dialogue immediately
                dialogueRunner.startAutomatically = true;
                dialogueRunner.startNode = "EndingDialogue";

                //change gravekeeper stuff
                gravekeeper.transform.position = outside.transform.position;
                gravekeeper.SetActive(true);
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

        if (MusicManagerScript.instance != null){
            MusicManagerScript.instance.sceneChecker();
        }
    }

    IEnumerator objActive(){
        yield return new WaitForSeconds(0.1f);
        fred.SetActive(true);
    }
}
