using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //0 = not completed, 1 = completed
    public int flowerPuzzle = 0;
    //2 = friend is gone
    public int coffeePuzzle = 0;

    void Awake(){
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [YarnCommand("ChangeVariable")]
    public void ChangeVariable(string[] variables){

        if (variables.Length < 2){
            Debug.Log("No value given");
        } else {
            switch(variables[0]){
                // case "":
                //      = int.Parse(variables[1]);
                //     break;
                default:
                    break;
            }
        }
    }
}
