using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class apartment_puzzle : MonoBehaviour
{

    public GameObject[] packingList = new GameObject[0];
    public Dictionary<string, bool> dic = new Dictionary<string, bool>();

    public bool puzzleComplete;
    public VariableStorageBehaviour CustomVariableStorage;

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
        //change all dialogue to runs of objs

        //change dialogeu to run of friend

        //remove all placeheres
    }
}
