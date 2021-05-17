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
                float perc = (-300 - GameManager.instance.playerPosition.z)/ 115;
                float newPos = -(perc * 115);

                if (newPos >= 0){
                    newPos = -5;
                } else if (newPos <= - 115){
                    newPos = -110;
                }

                player.transform.position = exitToPark.transform.position + new Vector3(15, 0, newPos);
            } else if (GameManager.instance.previousScene == "StreetCoffee"){

                float perc = (-180 - GameManager.instance.playerPosition.z)/ 150;
                float newPos = -(perc * 115);

                if (newPos >= 0){
                    newPos = -5;
                } else if (newPos <= - 115){
                    newPos = -108;
                }

                player.transform.position = exitToStreetCoffee.transform.position + new Vector3(-12, 0, newPos);
            }

            if (GameManager.instance.firstFriendMeeting == 2){
                friend.SetActive(true);
            }
        }

        if (MusicManagerScript.instance != null){
            MusicManagerScript.instance.sceneChecker();
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
