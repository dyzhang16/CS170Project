using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityOffice : MonoBehaviour
{
    public NodeVisitedTracker tracker;

    public GameObject player;
    public GameObject Office;
    public GameObject friend;

    public GameObject exitToCityArcade;

    void Awake(){
        if (GameManager.instance != null)
        {
            //set position of the player based on previousScene
            if (GameManager.instance.previousScene == "CityArcade"){
                player.transform.position = exitToCityArcade.transform.position + new Vector3(10, 0, 0);
            } else if (GameManager.instance.previousScene == "Office"){
                player.transform.position = Office.transform.position + new Vector3(0, 0, -10);
            }

            //change dialogue
            if (GameManager.instance.firstFriendMeeting == 3){
                friend.SetActive(true);
                friend.GetComponent<RunDialogue>().dialogueToRun = "cityoffice_friend_intro";
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
