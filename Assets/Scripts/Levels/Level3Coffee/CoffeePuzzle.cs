using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Yarn.Unity;
using UnityEngine.UI;

public class CoffeePuzzle : MonoBehaviour, IDropHandler
{
    //List of GameObjects to show/hide when placed
    //Objects in the puzzlePanel
    public GameObject puzzleCup, puzzleBeans, puzzleFilter, puzzleCompletedCup, puzzlePanel,
        //Objects in the gameWorld                                     
        gameWorldCup, gameWorldBeans, gameWorldFilter;
    //DialogueRunner Variables
    public DialogueRunner dialogueRunner;
    public VariableStorageBehaviour CustomVariableStorage;
    //List of Booleans for Logic
    [HideInInspector] public bool cupThere, filterThere, groundsThere;

    public GameObject[] slots;
    public GameObject[] images;

    public Button brewButton;
    
    //Called when finishing brewing a cup of coffee or leaving scene
    [YarnCommand("ResetCoffee")]

    public void Reset()
    {
        //resets all booleans
        cupThere = false;
        filterThere = false;
        groundsThere = false;
        puzzleCompletedCup.SetActive(false);
        CustomVariableStorage.SetValue("$FilterThere", 0);
        CustomVariableStorage.SetValue("$BeansThere", 0);
        CustomVariableStorage.SetValue("$CupThere", 0);

        //disable inventory drag
        foreach(GameObject obj in slots){
            obj.GetComponent<InventorySlot>().allowDrag = false;
        }

        foreach(GameObject obj in images){
            obj.GetComponent<ItemDragHandler>().allowDrag = false;
        }

        //close inv 
        GameObject.Find("InventoryController").GetComponent<OpenMenus>().closeInv();
    }

    //Called when dropping an item from inventory to puzzlePanel
    public void OnDrop(PointerEventData eventData)
    {
        Item droppedItem = Inventory.instance.itemList[eventData.pointerDrag.GetComponent<ItemDragHandler>().transform.parent.GetSiblingIndex()];
        if (puzzlePanel.GetComponent<CanvasGroup>().alpha == 1)
        {
            if (droppedItem.itemName == "Cup" && !cupThere)
            {
                GameManager.instance.addedCoffeeMachineItem = 1;
                cupThere = true;
                puzzleCup.SetActive(true);
                gameWorldCup.SetActive(true);
                Inventory.instance.RemoveItem(droppedItem);
                Inventory.instance.UpdateSlotUI();
                CustomVariableStorage.SetValue("$CupThere", 1);
                SoundManagerScript.PlaySound("place_cup"); // cup sound
            }
            else if (droppedItem.itemName == "Paper Filter" && !filterThere)
            {
                GameManager.instance.addedCoffeeMachineItem = 1;
                filterThere = true;
                puzzleFilter.SetActive(true);
                gameWorldFilter.SetActive(true);
                Inventory.instance.RemoveItem(droppedItem);
                Inventory.instance.UpdateSlotUI();
                CustomVariableStorage.SetValue("$FilterThere", 1);
                SoundManagerScript.PlaySound("place_filter"); // filter sound
            }
            else if (droppedItem.itemName == "Coffee Grounds" && !groundsThere && filterThere)
            {
                GameManager.instance.addedCoffeeMachineItem = 1;
                groundsThere = true;
                puzzleBeans.SetActive(true);
                gameWorldBeans.SetActive(true);
                Inventory.instance.RemoveItem(droppedItem);
                Inventory.instance.UpdateSlotUI();
                CustomVariableStorage.SetValue("$BeansThere", 1);
                SoundManagerScript.PlaySound("place_coffee"); // coffee sound
            }
            else
            {
                dialogueRunner.StartDialogue("WrongStep");
            }

            if (cupThere && filterThere && groundsThere){
                brewButton.image.color = new Color(0f, 255f, 0f, 1f);
            }
        }
    }

    public void brewCoffee()
    {
        if (cupThere && filterThere && groundsThere)
        {
            SoundManagerScript.PlaySound("pour_coffee"); 
            //Booleans to False
            cupThere = false;
            filterThere = false;
            groundsThere = false;
            //Puzzle Objects to False
            puzzleCup.SetActive(false);
            puzzleFilter.SetActive(false); 
            puzzleBeans.SetActive(false);
            //GameWorldObjects to False
            gameWorldBeans.SetActive(false);
            gameWorldCup.SetActive(false);
            gameWorldFilter.SetActive(false);
            //BrewedCoffee to Active
            puzzleCompletedCup.SetActive(true);

            brewButton.image.color = new Color(255f, 255f, 255f, 1f);
        }
        else 
        {
            dialogueRunner.StartDialogue("MissingSomething");
        }
    }
}
