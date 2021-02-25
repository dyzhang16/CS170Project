using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //inventory itemList
    public Item[] items;
    //0 = not completed, 1 = completed
    public int flowerPuzzle = 0;
    //2 = friend is gone
    public int coffeePuzzle = 0;
    public int officePuzzle = 0;
    public int visitedCoffee = 0;
    public int blender = 0;

    void Awake(){
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void Start(){
        if (items.Length == 0){
            items = new Item[Inventory.instance.itemList.Length];
        }
    }

    public void saveItems(){
        for (int i = 0; i < items.Length; ++i) {
            items[i] = Inventory.instance.itemList[i];
        }
    }

    public void loadItems(){
        for (int i = 0; i < items.Length; ++i) {
            Inventory.instance.AddItem(items[i]);
        }
    }

    public void deleteItems(){
        for (int i = 0; i < items.Length; ++i) {
            items[i] = null;
        }
    }

    [YarnCommand("ChangeVariable")]
    public void ChangeVariable(string[] variables){

        if (variables.Length < 2){
            Debug.Log("No value given");
        } else {
            switch(variables[0]){
                case "Blender":
                   blender = int.Parse(variables[1]);
                     break;
                case "visitedCoffee":
                    visitedCoffee = int.Parse(variables[1]);
                    break;
                default:
                    break;
            }
        }
    }
}
