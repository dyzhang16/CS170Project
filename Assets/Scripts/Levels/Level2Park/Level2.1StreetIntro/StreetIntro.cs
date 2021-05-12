using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetIntro : MonoBehaviour
{
    public NodeVisitedTracker tracker;

    public GameObject player;
    public GameObject exitToPark;
    public GameObject exitToStreetCoffee;

    public GameObject friend;

    void Awake(){
        if (GameManager.instance != null){

            //change player position based on the previous level
            if (GameManager.instance.previousScene == "Park"){
                //park calculation to deteremine new player position
                float perc = (-310 - GameManager.instance.playerPosition.z)/ 100;
                float newPos = -(perc * 115);

                player.transform.position = exitToPark.transform.position + new Vector3(15, 0, newPos);
            } else if (GameManager.instance.previousScene == "StreetCoffee"){

                float perc = (-190 - GameManager.instance.playerPosition.z)/ 140;
                float newPos = -(perc * 115);

                player.transform.position = exitToStreetCoffee.transform.position + new Vector3(-12, 0, newPos);
            }

            if (GameManager.instance.firstFriendMeeting == 2){
                friend.SetActive(true);
            }
        }
    }

    void Start(){
        //save inventory
         if (GameManager.instance != null){
            //load inventory stuff
            GameManager.instance.loadItems();
            GameManager.instance.deleteItems();
        }
    }
}
