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
                player.transform.position = exitToPark.transform.position + new Vector3(15, 0, 0);
            } else if (GameManager.instance.previousScene == "StreetCoffee"){
                player.transform.position = exitToStreetCoffee.transform.position + new Vector3(-12, 0, 0);
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
