using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetCoffee : MonoBehaviour
{
    public NodeVisitedTracker tracker;

    public GameObject player;
    public GameObject friend;
    public GameObject coffeeStand;

    public GameObject exitToStreetIntro;
    public GameObject exitToCityIntro;

    void Awake(){
        if (GameManager.instance != null){

            //change player position based on the previous level
            if (GameManager.instance.previousScene == "StreetIntro"){
                player.transform.position = exitToStreetIntro.transform.position + new Vector3(12, 0, 0);
            } else if (GameManager.instance.previousScene == "CoffeeScene"){
                player.transform.position = coffeeStand.transform.position + new Vector3(15, 0, -10);
            } else if (GameManager.instance.previousScene == "CityIntro"){
                player.transform.position = exitToCityIntro.transform.position + new Vector3(-12, 0, 0);
            }

            //change dialogue to correspond to certain cues
            if (GameManager.instance.visitedCoffee == 1){
                tracker.NodeComplete("friend_meeting");
            } else if (GameManager.instance.coffeePuzzle == 1){
                friend.GetComponent<RunDialogue>().dialogueToRun = "friend_after_coffee";
                coffeeStand.GetComponent<RunDialogue>().dialogueToRun = "coffee_done";
            }
        }
    }

    void Start()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.loadItems();
            GameManager.instance.deleteItems();
        }
    }
}
