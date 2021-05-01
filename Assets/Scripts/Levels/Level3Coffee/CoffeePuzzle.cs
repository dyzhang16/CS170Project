using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Yarn.Unity;

public class CoffeePuzzle : MonoBehaviour, IDropHandler
{
    //List of GameObjects to show/hide when placed
    //Objects in the puzzlePanel
    public GameObject puzzleCup, puzzleWater, puzzleBeans, puzzleFilter, puzzleCompletedCup, puzzlePanel,
        //Objects in the gameWorld                                     
        gameWorldCup, gameWorldWater, gameWorldBeans, gameWorldFilter;
    //DialogueRunner Variables
    public DialogueRunner dialogueRunner;
    public VariableStorageBehaviour CustomVariableStorage;
    //List of Booleans for Logic
    [HideInInspector] public bool cupThere, filterThere, groundsThere, waterThere;
    
    //Called when finishing brewing a cup of coffee or leaving scene
    [YarnCommand("ResetCoffee")]

    public void Reset()
    {
        //resets all booleans
        waterThere = false;
        cupThere = false;
        filterThere = false;
        groundsThere = false;
        puzzleCompletedCup.SetActive(false);
        CustomVariableStorage.SetValue("$WaterThere", 0);
        CustomVariableStorage.SetValue("$FilterThere", 0);
        CustomVariableStorage.SetValue("$BeansThere", 0);
        CustomVariableStorage.SetValue("CupThere", 0);
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
            else if (droppedItem.itemName == "Cup o' water"   && !waterThere)
            {
                GameManager.instance.addedCoffeeMachineItem = 1;
                waterThere = true;
                puzzleWater.SetActive(true);
                gameWorldWater.SetActive(true);
                Inventory.instance.RemoveItem(droppedItem);
                //Adds a cup item back to inventory after dumping water into Coffee Machine
                Item cup = GetComponent<ItemAssignment>().item;
                Inventory.instance.AddItem(cup);
                Inventory.instance.UpdateSlotUI();
                CustomVariableStorage.SetValue("$WaterThere", 1);
                //still need a Pour Water Sound Fx
                SoundManagerScript.PlaySound("pour_coffee"); // coffee sound
            }
            else
            {
                dialogueRunner.StartDialogue("WrongStep");
            }
        }
    }

    public void brewCoffee()
    {
        if (cupThere && filterThere && groundsThere && waterThere)
        {
            SoundManagerScript.PlaySound("pour_coffee"); 
            //Booleans to False
            cupThere = false;
            waterThere = false;
            filterThere = false;
            groundsThere = false;
            //Puzzle Objects to False
            puzzleWater.SetActive(false);
            puzzleCup.SetActive(false);
            puzzleFilter.SetActive(false); 
            puzzleBeans.SetActive(false);
            //GameWorldObjects to False
            gameWorldWater.SetActive(false);
            gameWorldBeans.SetActive(false);
            gameWorldCup.SetActive(false);
            gameWorldFilter.SetActive(false);
            //BrewedCoffee to Active
            puzzleCompletedCup.SetActive(true);
        }
        else 
        {
            dialogueRunner.StartDialogue("MissingSomething");
        }
    }
}
