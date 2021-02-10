using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int notFriend;

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
                case "notFriend":
                    notFriend = int.Parse(variables[1]);
                    break;
                default:
                    break;
            }
        }
    }
}
