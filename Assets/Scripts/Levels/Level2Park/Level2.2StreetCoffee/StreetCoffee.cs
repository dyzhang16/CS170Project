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
    public GameObject CoffeeStandArrow;

    public GameObject exitToStreetIntro;

    void Awake(){
        if (GameManager.instance != null){

            //change player position based on the previous level
            if (GameManager.instance.previousScene == "StreetIntro"){
                float perc = (-210 - GameManager.instance.playerPosition.z)/ 115;
                float newPos = -(perc * 150);

                if (newPos >= 0){
                    newPos = -5;
                } else if (newPos <= - 150){
                    newPos = -145;
                }

                player.transform.position = exitToStreetIntro.transform.position + new Vector3(12, 0, newPos);
            } else if (GameManager.instance.previousScene == "CoffeeScene"){
                player.transform.position = coffeeStand.transform.position + new Vector3(15, 0, -10);
            }
            
            //change dialogue to correspond to certain cues
            if (GameManager.instance.firstFriendMeeting == 1){
                tracker.NodeComplete("friend_meeting");
                CoffeeStandArrow.GetComponent<BoxCollider>().enabled = true;

                //if you have the receipt
                if (GameManager.instance.hasReceipt == 1){
                    Destroy(receipt);
                } else {
                    receipt.SetActive(true);
                    receipt.GetComponent<Animator>().SetTrigger("ReceiptFall");
                }
            }
            
            if (GameManager.instance.gaveDrink == 1) {
                tracker.NodeComplete("friend_after_coffee");
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
