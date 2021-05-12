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
                float perc = (-221 - GameManager.instance.playerPosition.z)/ 115;
                float newPos = -(perc * 100);

                player.transform.position = exitToStreetIntro.transform.position + new Vector3(-10, 0, newPos);
            } else if (GameManager.instance.previousScene == "CityOffice"){
                float perc = (-223 - GameManager.instance.playerPosition.z)/ 110;
                float newPos = -(perc * 100);

                player.transform.position = exitToCityOffice.transform.position + new Vector3(10, 0, newPos);
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
