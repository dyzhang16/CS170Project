using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static bool exists;

    //inventory itemList
    public Item[] items;

    //previous scene
    public string previousScene;

    //0 = not completed, 1 = completed
    public int flowerPuzzle = 0;
    public int coffeePuzzle = 0;
    public int visitedCoffee = 0;
    public int blender = 0;
    public int officePuzzle = 0;
    public int visitedAfterCoffee = 0;

    void Awake(){
        if (!exists){
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            exists = true;
        } else {
            Destroy(this.gameObject);
        }
    }

    void Start(){
        if (items.Length == 0){
            items = new Item[6];
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
                Inventory.instance.AddItem(items[i]);
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
                case "Blender":
                    blender = int.Parse(variables[1]);
                    break;
                case "visitedCoffee":
                    visitedCoffee = int.Parse(variables[1]);
                    break;
                case "visitedAfterCoffee":
                    visitedAfterCoffee = int.Parse(variables[1]);
                    break;
                default:
                    break;
            }
        }
    }
}
