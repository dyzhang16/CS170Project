using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

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
            } else if (GameManager.instance.officeDeskPuzzle == 1){
                friend.SetActive(true);
                friend.GetComponent<RunDialogue>().dialogueToRun = "goingToApartment1";
                friend.transform.position = Office.transform.position + new Vector3(0, 0, -20);
            }

            // if friend is stuck outside of the office, put his position outside of the office
            if (GameManager.instance.idNeeded == 1) // player needs to give Fredric the ID but hasn't yet
            {
                friend.SetActive(true);
                friend.transform.position = new Vector3(162.95f, 0f, -213.1492f);
                // strange bug but set the friend's collider to be a trigger
                friend.GetComponentInChildren<Collider>().isTrigger = true;
            }
        }
    }

    void Start()
    {
        if (GameManager.instance != null)
        {
            // Add the GetVariable function to check things
            DialogueRunner dialogueRunner = GameObject.Find("DialogueRunner").GetComponent<DialogueRunner>();
            dialogueRunner.AddFunction("GetVariable", 1, (Yarn.Value[] query) => GameManager.instance.GetVariable(query[0].AsString));

            GameManager.instance.loadItems();
            GameManager.instance.deleteItems();
        }
    }
}
