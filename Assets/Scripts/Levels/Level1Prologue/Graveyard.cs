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
    public GameObject key;
    public GameObject gateApproach;

    void Awake(){
        if (GameManager.instance != null){
            if (GameManager.instance.flowerPuzzle == 1){
                //completed gravestone dialogue and completed puzzle
                tracker.NodeComplete("prologue_begin");
                tracker.NodeComplete("prologue_doneExplore");
                CustomVariableStorage.SetValue("$puzzle", 1);

                //has key
                CustomVariableStorage.SetValue("$key", 1);

                //destroy gravekeeper
                Destroy(gravekeeper);

                //destroy flowers
                Destroy(flowerA);
                Destroy(flowerB);
                Destroy(flowerC);
                Destroy(flowerD);

                //destroy key
                Destroy(key);

                //gravestone start instantly = false;
                RunDialogue dia = gravestone.GetComponent<RunDialogue>();
                dia.startInstantly = false;

                //set player position to originial position in gamemanager.instance;
                Vector3 pos = new Vector3((gateApproach.transform.position.x - player.transform.Find("collider").GetComponent<BoxCollider>().size.x), 
                    gateApproach.transform.position.y, gateApproach.transform.position.z);
                player.transform.position = pos;

                //destroy gate approach
                Destroy(gateApproach);
            }
        }
    }
}
