using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //inventory itemList
    //updated on Transition's changeScene
    //items loaded and then cleared on each scene's Start()
    public Item[] items;

    //previous scene
    public string previousScene;

    //0 = not completed, 1 = completed
    //tutorial
    //changed when the flower puzzle is completed
    public int flowerPuzzle = 0;

    //street area
    //changed when the friend drops the receipt
    //1 = dropped receipt
    //2 = leaves streetCoffee and goes to office 
    //3 = after first encounter in cityOffice area
    public int firstFriendMeeting = 0;
    //changes when the player picks up the receipt
    public int hasReceipt = 0;
    //change when the player gives any drink to the player
    public int gaveDrink = 0;
    //changed when the friend leaves the streetCoffee scene after recieving the correct coffee
    public int visitedAfterCoffee = 0;

    public int visitedCoffee = 0;
    public int blender = 0;
    public int waterAdded = 0;

    //office hub
    public int followFriendinOffice = 0;
    //office puzzle
    public int officePuzzle = 0;

    // Arcade variables
    public int arcadeFirstVisit = 0;
    public int arcadeFirstCrane = 0;
    public int arcadeFirstDance = 0;
    public int arcadeNoCraneDirs = 0;
    public int arcadeNoDanceDirs = 0;

    void Awake(){
        if (instance != null && instance != this){
            Destroy(this.gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void saveItems(){
        if (Inventory.instance != null){
            for (int i = 0; i < items.Length; ++i) {
                items[i] = Inventory.instance.itemList[i];
            }
        }
    }

    public void loadItems(){
        if (Inventory.instance != null){
            for (int i = 0; i < items.Length; ++i) {
                if(items[i] != null) 
                {
                    Inventory.instance.AddItem(items[i]);
                }
            }
        }
    }

    public void deleteItems(){
        if (Inventory.instance != null){
            for (int i = 0; i < items.Length; ++i) {
                items[i] = null;
            }
        }
    }

    [YarnCommand("ChangeVariable")]
    public void ChangeVariable(string[] variables){
        if (variables.Length < 2){
            Debug.Log("No value given");
        } else {
            switch(variables[0]){
                case "hasReceipt":
                    hasReceipt = int.Parse(variables[1]);
                    break;
                case "Blender":
                    blender = int.Parse(variables[1]);
                    break;
                case "visitedCoffee":
                    visitedCoffee = int.Parse(variables[1]);
                    break;
                case "waterAdded":
                    waterAdded = int.Parse(variables[1]);
                    break;
                case "gaveDrink":
                    gaveDrink = int.Parse(variables[1]);
                    break;
                case "arcadeFirstVisit":
                    arcadeFirstVisit = int.Parse(variables[1]);
                    break;
                case "followFriendinOffice":
                    followFriendinOffice = int.Parse(variables[1]);
                    break;
                default:
                    System.Type GMType = typeof(GameManager);
                    System.Reflection.FieldInfo fieldInfo = GMType.GetField(variables[0]);
                    if (fieldInfo != null)
                    {
                        fieldInfo.SetValue(this, int.Parse(variables[1]));
                    }
                    else
                    {
                        Debug.LogError(string.Format("Error, could not find variables {0}.", variables[0]));
                    }
                    break;
            }
        }
    }
}
