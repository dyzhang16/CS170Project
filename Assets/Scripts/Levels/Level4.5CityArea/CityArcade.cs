using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityArcade : MonoBehaviour
{
    public NodeVisitedTracker tracker;

    public GameObject player;
    public GameObject Arcade;
    public GameObject friend;
    public GameObject apartment;
    public GameObject car;

    public GameObject exitToCityOffice;
    public GameObject exitToStreetCoffee;

    void Awake(){
        if (GameManager.instance != null){
            if (GameManager.instance.previousScene == "StreetCoffee"){
                player.transform.position = exitToStreetCoffee.transform.position + new Vector3(10, 0, 0);
            } else if (GameManager.instance.previousScene == "CityOffice"){
                player.transform.position = exitToCityOffice.transform.position + new Vector3(-10, 0, 0);
            } else if (GameManager.instance.previousScene == "ArcadeScene"){
                player.transform.position = Arcade.transform.position + new Vector3(0, 0, -10);
            } else if (GameManager.instance.previousScene == "ApartmentScene") {
                player.transform.position = apartment.transform.position + new Vector3(0, 0, -10);
            }

            //first friend meeting
            if (GameManager.instance.firstFriendMeeting == 2){
                friend.SetActive(true);
                friend.GetComponent<RunDialogue>().dialogueToRun = "cityarcade_friend_intro";
            } else if (GameManager.instance.firstDateDia == 1){
                friend.SetActive(true);
                friend.transform.position = apartment.transform.position + new Vector3(0, 0, -20);
            } else if (GameManager.instance.walkedToApartment == 1) {
                friend.SetActive(true);
                apartment.GetComponent<RunDialogue>().dialogueToRun = "cityapartment_apartment";
                friend.GetComponent<RunDialogue>().dialogueToRun = "goingToApartment2";
                friend.transform.position = exitToCityOffice.transform.position + new Vector3(-20, 0, 0);
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
