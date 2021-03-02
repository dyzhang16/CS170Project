using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetCoffee : MonoBehaviour
{
    public NodeVisitedTracker tracker;

    public GameObject player;
    public GameObject friend;
    public GameObject coffeeStand;
    public GameObject receipt;

    public GameObject exitToStreetIntro;
    public GameObject exitToCityArcade;

    void Awake(){
        if (GameManager.instance != null){

            //change player position based on the previous level
            if (GameManager.instance.previousScene == "StreetIntro"){
                player.transform.position = exitToStreetIntro.transform.position + new Vector3(12, 0, 0);
            } else if (GameManager.instance.previousScene == "CoffeeScene"){
                player.transform.position = coffeeStand.transform.position + new Vector3(15, 0, -10);
            } else if (GameManager.instance.previousScene == "CityArcade"){
                player.transform.position = exitToCityArcade.transform.position + new Vector3(-12, 0, 0);
            }
            //change dialogue to correspond to certain cues
            if (GameManager.instance.firstFriendMeeting == 1){
                tracker.NodeComplete("friend_meeting");

                //if you have the receipt
                if (GameManager.instance.hasReceipt == 1){
                    Destroy(receipt);
                } else {
                    receipt.SetActive(true);
                    receipt.GetComponent<Animator>().SetTrigger("ReceiptFall");
                }
            }
            
            if (GameManager.instance.visitedAfterCoffee == 1){
                Destroy(friend);
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
