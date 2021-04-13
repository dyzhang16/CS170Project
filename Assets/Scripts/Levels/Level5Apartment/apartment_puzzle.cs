using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class apartment_puzzle : MonoBehaviour
{

    public GameObject[] packingList = new GameObject[0];
    public Dictionary<string, bool> dic = new Dictionary<string, bool>();

    public GameObject placeHeres;
    public GameObject puzzleItems;
    public GameObject otherItems;
    public GameObject timeCapsule;
    public GameObject Bookshelf;

    public GameObject laptop;

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

        //change dialogeu to run of friend
        friend.GetComponent<RunDialogue>().dialogueToRun = "friend_after_cleaning";

        GameManager.instance.cleanedRoom = 1;

        Destroy(placeHeres);
    }

    IEnumerator finishedCleaning(){
        yield return new WaitForSeconds(1f);

        //change color back
        foreach (GameObject obj in packingList) {
            obj.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            if (obj.name == "Bookshelf") {
                obj.GetComponent<RunDialogue>().dialogueToRun = "BookshelfEscape";
            } else if (obj.name == "Table"){
                obj.GetComponent<RunDialogue>().dialogueToRun = "Laptop";
            } else {
                Destroy(obj.GetComponent<RunDialogue>());
            }
        }

        //add all new items to scene
        //puzzle elements
        puzzleItems.SetActive(true);
        //other items
        otherItems.SetActive(true);
        //set laptop position
        laptop.transform.position = packingList[0].transform.position + new Vector3(0, 6, 8);
        //destroy bed
        Destroy(bed);

        yield return new WaitForSeconds(1f);
        //start first friend dialogue
        dia.StartDialogue("first_friend_after_cleaning");
    }

    [YarnCommand("revealTimeCapsule")]
    public void revealTimeCapsule(){
        timeCapsule.SetActive(true);
    }
}
