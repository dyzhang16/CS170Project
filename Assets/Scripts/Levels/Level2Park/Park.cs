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
                float perc = (-210 - GameManager.instance.playerPosition.z)/ 115;
                float newPos = -(perc * 115);

                if (newPos >= 0){
                    newPos = -5;
                } else if (newPos <= - 115){
                    newPos = -110;
                }

                player.transform.position = exitToStreetIntro.transform.position + new Vector3(-10, 0, newPos);
            } else if (GameManager.instance.previousScene == "CityOffice"){
                float perc = (-216 - GameManager.instance.playerPosition.z)/ 115;
                float newPos = -(perc * 115);

                if (newPos >= 0){
                    newPos = -5;
                } else if (newPos <= - 115){
                    newPos = -110;
                }

                player.transform.position = exitToCityOffice.transform.position + new Vector3(10, 0, newPos);
            }

            if (GameManager.instance.firstFriendMeeting == 3){
                friend.SetActive(true);
            }
        }

        if (MusicManagerScript.instance != null){
            MusicManagerScript.instance.sceneChecker();
        }
    }

    void Start(){
        if (GameManager.instance != null){
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
