using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Park : MonoBehaviour
{
    public NodeVisitedTracker tracker;
    public VariableStorageBehaviour CustomVariableStorage;

    public GameObject player;
    public GameObject exitToStreetIntro; 
    public GameObject exitToCityOffice;

    public GameObject friend;

    void Awake(){
        if (GameManager.instance != null){
            //changes player position based on previous level
            if (GameManager.instance.previousScene == "StreetIntro"){
                player.transform.position = exitToStreetIntro.transform.position + new Vector3(-10, 0, 0);
            } else if (GameManager.instance.previousScene == "CityOffice"){
                player.transform.position = exitToCityOffice.transform.position + new Vector3(10, 0, 0);
            }

            if (GameManager.instance.firstFriendMeeting == 3){
                friend.SetActive(true);
            }
        }
    }

    void Start(){
        if (GameManager.instance != null){
            // Add the GetVariable function to check things
            DialogueRunner dialogueRunner = GameObject.Find("DialogueRunner").GetComponent<DialogueRunner>();
            dialogueRunner.AddFunction("GetVariable", 1, (Yarn.Value[] query) => GameManager.instance.GetVariable(query[0].AsString));

            //load inventory stuff
            GameManager.instance.loadItems();
            GameManager.instance.deleteItems();
        }
    }
}
