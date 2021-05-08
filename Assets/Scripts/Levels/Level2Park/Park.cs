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

    void Awake(){
        if (GameManager.instance != null){
            //changes player position based on previous level
            if (GameManager.instance.previousScene == "StreetIntro"){
                player.transform.position = exitToStreetIntro.transform.position + new Vector3(-10, 0, 0);
            }
        }
    }

    void Start(){
        if (GameManager.instance != null){
            //load inventory stuff
            GameManager.instance.loadItems();
            GameManager.instance.deleteItems();
        }
    }
}
