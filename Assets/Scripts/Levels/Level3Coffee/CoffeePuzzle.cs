using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Yarn.Unity;

public class CoffeePuzzle : MonoBehaviour, IDropHandler
{
    public GameObject puzzlePanel;
    public VariableStorageBehaviour CustomVariableStorage;
    public bool cupThere, filterThere, groundsThere;
    public GameObject cup, filter, grounds, brewedCoffee;
   
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
                SoundManagerScript.PlaySound("place_cup"); // cup sound
            }
            else if (droppedItem.itemName == "Paper Filter" && !filterThere)
            {
                filterThere = true;
                filter.SetActive(true);
                Inventory.instance.RemoveItem(droppedItem);
                Inventory.instance.UpdateSlotUI();
                SoundManagerScript.PlaySound("place_filter"); // filter sound
            }
            else if (droppedItem.itemName == "Coffee Grounds" && !groundsThere && filterThere)
            {
                groundsThere = true;
                grounds.SetActive(true);
                Inventory.instance.RemoveItem(droppedItem);
                Inventory.instance.UpdateSlotUI();
                SoundManagerScript.PlaySound("place_coffee"); // coffee sound
            }
            else
            {
                Debug.Log("Cannot be Dropped");
            }
        }
    }

    public void brewCoffee()
    {
        if (cupThere && filterThere && groundsThere)
        {
            SoundManagerScript.PlaySound("pour_coffee"); 
            
            cupThere = false;
            cup.SetActive(false);
            filterThere = false;
            filter.SetActive(false);
            groundsThere = false;
            grounds.SetActive(false);

            brewedCoffee.SetActive(true);

            
        }
        else 
        {
            Debug.Log("Missing something");
        }
    }
}
