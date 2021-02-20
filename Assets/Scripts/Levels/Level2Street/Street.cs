using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Street : MonoBehaviour
{
    public NodeVisitedTracker tracker;
    public VariableStorageBehaviour CustomVariableStorage;

    public GameObject player;

    public GameObject friend;
    public GameObject coffeeStand;

    void Awake(){
        if (GameManager.instance != null){
            //if coffee puzzle is finished
            if (GameManager.instance.coffeePuzzle == 1){

                //set the coffeestand to complete
                coffeeStand.GetComponent<RunDialogue>().dialogueToRun = "coffee_done";
                //set the friend dialogue to complete
                friend.GetComponent<RunDialogue>().dialogueToRun = "friend_after_coffee";

                //set friend position
                Vector3 pos = new Vector3(coffeeStand.transform.position.x -18, coffeeStand.transform.position.y, coffeeStand.transform.position.z -6);
                friend.transform.position = pos;

                //set player position
                player.transform.position = pos + new Vector3(30, 0, 0);
            }
        }
    }
}
