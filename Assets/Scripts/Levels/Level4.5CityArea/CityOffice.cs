using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class CityOffice : MonoBehaviour
{
    public NodeVisitedTracker tracker;

    public GameObject player , introduction, officedoor, officeintroduction;
    public GameObject Office;
    public GameObject friend;

    public GameObject officeDoorArrow;
    public GameObject exitToPark;


    void Awake(){
        if (GameManager.instance != null)
        {
            //set position of the player based on previousScene
            if (GameManager.instance.previousScene == "Park"){
                float perc = (-300 - GameManager.instance.playerPosition.z)/ 110;

                float newPos = -(perc * 115);

                if (newPos >= 0){
                    newPos = -5;
                } else if (newPos <= -115){
                    newPos = -110;
                }

                player.transform.position = exitToPark.transform.position + new Vector3(-10, 0, newPos);
            } else if (GameManager.instance.previousScene == "Office" || GameManager.instance.previousScene == "OfficeScene"){
                player.transform.position = officedoor.transform.position + new Vector3(-20, 0, 0);
            }

            //change dialogue
            if (GameManager.instance.firstFriendMeeting == 4){
                friend.SetActive(true);
                introduction.SetActive(true);
            } else if (GameManager.instance.firstFriendMeeting == 5){
                friend.SetActive(true);
                introduction.GetComponent<RunDialogue>().enabled = false;
                introduction.GetComponent<BoxCollider>().enabled = false;
                friend.transform.position = officedoor.transform.position;
                friend.GetComponent<SpriteRenderer>().flipX = true;
            } else {
                introduction.GetComponent<RunDialogue>().enabled = false;
                introduction.GetComponent<BoxCollider>().enabled = false;
            }
            
            if (GameManager.instance.officeDeskPuzzle == 1){
                friend.SetActive(true);
                friend.transform.position = officedoor.transform.position;
                officeintroduction.SetActive(true);
            }

            // if friend is stuck outside of the office, put his position outside of the office
            if (GameManager.instance.idNeeded == 1) // player needs to give Fredric the ID but hasn't yet
            {
                friend.SetActive(true);
                friend.transform.position = officedoor.transform.position;
                // strange bug but set the friend's collider to be a trigger
                friend.GetComponentInChildren<Collider>().isTrigger = true;
            } else if (GameManager.instance.idNeeded == 2){
                officeDoorArrow.GetComponent<BoxCollider>().enabled = true;
            }
        }

        if (MusicManagerScript.instance != null){
            MusicManagerScript.instance.sceneChecker();
        }
    }

    void Start()
    {
        if (GameManager.instance != null)
        {
            // Add the GetVariable function to check things
            DialogueRunner dialogueRunner = GameObject.Find("DialogueRunner").GetComponent<DialogueRunner>();
            dialogueRunner.AddFunction("GetVariable", 1, (Yarn.Value[] query) => GameManager.instance.GetVariable(query[0].AsString));

            if (GameManager.instance.clearInventory){
                GameManager.instance.deleteItems();
                GameManager.instance.clearItems();
                GameManager.instance.clearInventory = false;
            } else {
                GameManager.instance.loadItems();
                GameManager.instance.deleteItems();
            }
        }

    }
}
