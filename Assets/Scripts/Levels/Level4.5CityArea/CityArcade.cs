using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class CityArcade : MonoBehaviour
{
    public NodeVisitedTracker tracker;

    public GameObject player;
    public GameObject Arcade;
    public GameObject friend, introduction, lanyard;
    public GameObject apartment;

    public GameObject exitToCityOffice;
    public GameObject exitToStreetCoffee;

    void Awake()
    {
        if (GameManager.instance != null)
        {
            if (GameManager.instance.previousScene == "StreetCoffee")
            {
                player.transform.position = exitToStreetCoffee.transform.position + new Vector3(20, 0, 0);
            }
            else if (GameManager.instance.previousScene == "CityOffice")
            {
                player.transform.position = exitToCityOffice.transform.position + new Vector3(-10, 0, 0);
            }
            else if (GameManager.instance.previousScene == "ArcadeScene")
            {
                player.transform.position = Arcade.transform.position + new Vector3(0, 0, -10);
            }
            else if (GameManager.instance.previousScene == "ApartmentScene")
            {
                player.transform.position = apartment.transform.position + new Vector3(0, 0, -10);
            }

            //first friend meeting
            if (GameManager.instance.firstFriendMeeting == 2)
            {
                friend.SetActive(true);

            }
            else if (GameManager.instance.firstDateDia == 1)
            {
                friend.SetActive(true);
                friend.GetComponent<RunDialogue>().dialogueToRun = "friendWalkToStreet";
                friend.transform.position = apartment.transform.position + new Vector3(0, 0, -20);
            }
            else if (GameManager.instance.walkedToApartment == 1)
            {
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
            // Add the GetVariable function to check things
            DialogueRunner dialogueRunner = GameObject.Find("DialogueRunner").GetComponent<DialogueRunner>();
            dialogueRunner.AddFunction("GetVariable", 1, (Yarn.Value[] query) => GameManager.instance.GetVariable(query[0].AsString));

            GameManager.instance.loadItems();
            GameManager.instance.deleteItems();
        }
        if (GameManager.instance.firstFriendMeeting != 2)
        {
            introduction.GetComponent<RunDialogue>().enabled = false;
            introduction.GetComponent<BoxCollider>().enabled = false;
        }
        if (GameManager.instance.idNeeded == 0)
        {
            lanyard.SetActive(false);
        }
    }
}
