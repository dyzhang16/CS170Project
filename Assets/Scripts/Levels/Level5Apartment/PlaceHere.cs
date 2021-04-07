using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceHere : MonoBehaviour
{
    public string objName;

    public apartment_puzzle puzzle;
    public floor floor;

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Player" && floor.activeObj != null){

            if (floor.activeObj.name == objName) {
                floor.activeObj.GetComponent<RunDialogue>().dialogueToRun = "ObjectFinished";

                puzzle.dic[objName] = true;
            }
        }
    }
}
