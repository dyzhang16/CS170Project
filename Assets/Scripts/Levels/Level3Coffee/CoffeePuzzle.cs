using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Yarn.Unity;

public class CoffeePuzzle : MonoBehaviour, IDropHandler
{
    public GameObject puzzlePanel, CoffeeCup;
    public DialogueRunner dialogueRunner;
    public VariableStorageBehaviour CustomVariableStorage;
    public GameObject cup, filter, grounds, brewedCoffee;
    [HideInInspector] public bool cupThere, filterThere, groundsThere, waterThere;
    
   
    [YarnCommand("ResetCoffee")]

    public void Reset()
    {
        cupThere = false;
        filterThere = false;
        groundsThere = false;
        brewedCoffee.SetActive(false);
    }

    public void OnDrop(PointerEventData eventData)
    {
        Item droppedItem = Inventory.instance.itemList[eventData.pointerDrag.GetComponent<ItemDragHandler>().transform.parent.GetSiblingIndex()];
        if (puzzlePanel.GetComponent<CanvasGroup>().alpha == 1)
        {
            if (droppedItem.itemName == "Cup" && !cupThere)
            {
                cupThere = true;
                cup.SetActive(true);
                Inventory.instance.RemoveItem(droppedItem);
                Inventory.instance.UpdateSlotUI();
                CustomVariableStorage.SetValue("$CupThere", 1);
                CoffeeCup.SetActive(true);
                SoundManagerScript.PlaySound("place_cup"); // cup sound
            }
            else if (droppedItem.itemName == "Paper Filter" && !filterThere)
            {
                filterThere = true;
                filter.SetActive(true);
                Inventory.instance.RemoveItem(droppedItem);
                Inventory.instance.UpdateSlotUI();
                CustomVariableStorage.SetValue("$FilterThere", 1);
                SoundManagerScript.PlaySound("place_filter"); // filter sound
            }
            else if (droppedItem.itemName == "Coffee Grounds" && !groundsThere && filterThere)
            {
                groundsThere = true;
                grounds.SetActive(true);
                Inventory.instance.RemoveItem(droppedItem);
                Inventory.instance.UpdateSlotUI();
                CustomVariableStorage.SetValue("$BeansThere", 1);
                SoundManagerScript.PlaySound("place_coffee"); // coffee sound
            }
            else if (droppedItem.itemName == "Blender")
            {
                waterThere = true;
                Inventory.instance.RemoveItem(droppedItem);
                Inventory.instance.UpdateSlotUI();
                GameManager.instance.waterAdded = 1;
                CustomVariableStorage.SetValue("$WaterThere", 1);
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
            
            cupThere = false;
            waterThere = false;
            cup.SetActive(false);
            filterThere = false;
            filter.SetActive(false);
            groundsThere = false;
            grounds.SetActive(false);
            CoffeeCup.SetActive(false);
            brewedCoffee.SetActive(true);
        }
        else 
        {
            dialogueRunner.StartDialogue("MissingSomething");
        }
    }
}
