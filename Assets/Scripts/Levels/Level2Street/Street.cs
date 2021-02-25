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

                //set player position
                player.transform.position = friend.transform.position + new Vector3(30, 0, 0);
            } else if (GameManager.instance.visitedCoffee == 1){
                //set player position
                player.transform.position = friend.transform.position + new Vector3(30, 0, 0);

                //change friend dialogue   
                tracker.NodeComplete("friend_meeting");
            } else if (GameManager.instance.visitedAfterCoffee == 1){

            }
        }
    }

    void Start(){
        if (GameManager.instance != null){
            //load inventory stuff
            GameManager.instance.loadItems();
            Item key = Inventory.instance.FindItem("Key");
            if (key){
                Inventory.instance.RemoveItem(key);
            }
            GameManager.instance.deleteItems();
        }
    }
}
