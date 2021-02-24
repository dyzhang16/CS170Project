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

    void Start(){
        if (GameManager.instance != null){
            if (GameManager.instance.flowerPuzzle == 1){
                //completed gravestone dialogue and completed puzzle
                //Debug.Log(CustomVariableStorage.GetValue("$checkpoint_1"));
                // CustomVariableStorage.SetValue("$checkpoint_1", 1);
                //Debug.Log(CustomVariableStorage.GetValue("$checkpoint_1"));
                // CustomVariableStorage.SetValue("$puzzle", 1);

                gravestone.GetComponent<RunDialogue>().dialogueToRun = "prologue_doneFlowers";
                gate.GetComponent<RunDialogue>().dialogueToRun = "return_gate";

                // tracker.NodeComplete("prologue_begin");

                //has key
                // CustomVariableStorage.SetValue("$key", 1);

                //destroy gravekeeper
                Destroy(gravekeeper);

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
        }
    }
}
