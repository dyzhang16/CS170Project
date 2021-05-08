using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class CityArcade : MonoBehaviour
{
    public NodeVisitedTracker tracker;

    public GameObject player;
    public GameObject Arcade;
    public GameObject friend, introduction;
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

            //first friend meeting
            if (GameManager.instance.firstFriendMeeting == 2)
            {
                friend.SetActive(true);

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
    }
}
