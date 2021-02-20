using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Street : MonoBehaviour
{
    public NodeVisitedTracker tracker;
    public VariableStorageBehaviour CustomVariableStorage;

    public GameObject friend;
    public GameObject coffeeStand;

    void Awake(){
        if (GameManager.instance != null){
            //if coffee puzzle is finished
            if (GameManager.instance.coffeePuzzle == 1){

                //set friend dialogue to transition to office
                tracker.NodeComplete("friend_street");
                //set coffeestand dialogue complete
                tracker.NodeComplete("coffeestand");
                //coffeepuzzle complete for dialogue to transition to office
                CustomVariableStorage.SetValue("$coffeepuzzle", 1);

                //set friend position
                Vector3 pos = new Vector3(coffeeStand.transform.position.x + 4, coffeeStand.transform.position.y, coffeeStand.transform.position.z -6);
                friend.transform.position = pos;


            }
        }
    }
}
