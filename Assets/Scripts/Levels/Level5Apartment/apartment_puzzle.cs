using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class apartment_puzzle : MonoBehaviour
{

    public GameObject[] packingList = new GameObject[0];
    public Dictionary<string, bool> dic = new Dictionary<string, bool>();

    public GameObject[] placeHeres = new GameObject[0];
    
    public GameObject[] escapeRoomItems = new GameObject[0];

    public bool puzzleComplete;
    public VariableStorageBehaviour CustomVariableStorage;
    public DialogueRunner dia;

    public GameObject friend;
    public GameObject bed;

    // Start is called before the first frame update
    void Start()
    {
        //transfer array to dictionary
        foreach (GameObject obj in packingList){
            dic.Add(obj.name, false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    [YarnCommand("checkIfComplete")]
    public void checkIfComplete(){
        foreach(bool key in dic.Values){
            if (!key){
                puzzleComplete = false;
                return;
            }
        }

        puzzleComplete = true;
        CustomVariableStorage.SetValue("$apartment_puzzle", 1);
    }

    [YarnCommand("pickUpObj")]
    public void AddPuzzle(string obj){
        dic[obj] = false;
    }

    [YarnCommand("finishedLevel")]
    public void finishedLevel(){

        //add delay to make the transition better
        StartCoroutine(finishedCleaning());

        //add all new items to scene
        

        //change dialogeu to run of friend
        friend.GetComponent<RunDialogue>().dialogueToRun = "";

        //change dialogue of bed
        bed.AddComponent<RunDialogue>();

        //remove all placeheres
        foreach (GameObject obj in placeHeres) {
            Destroy(obj);
        }
    }

    IEnumerator finishedCleaning(){
        yield return new WaitForSeconds(1f);

        //change color back
        foreach (GameObject obj in packingList) {
            obj.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            if (obj.name != "Bookshelf") {
                Destroy(obj.GetComponent<RunDialogue>());
            } else {
                obj.GetComponent<RunDialogue>().dialogueToRun = "BookshelfEscape";
            }
        }

        yield return new WaitForSeconds(1f);
        //start first friend dialogue
        dia.StartDialogue("first_friend_after_cleaning");
    }
}
