using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CoffeePuzzle : MonoBehaviour, IDropHandler
{
    public GameObject puzzlePanel;
    public bool cupThere, filterThere, groundsThere, sleeveThere, complete = false;
    public GameObject cup, filter, grounds, sleeve, completedCup;

    public void Start() { 
    }

    // Update is called once per frame
    void Update()
    {
        if (cupThere && filterThere && groundsThere && sleeveThere) {
            complete = true;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        Item droppedItem = Inventory.instance.itemList[eventData.pointerDrag.GetComponent<ItemDragHandler>().transform.parent.GetSiblingIndex()];

        if (droppedItem.itemName == "Cup" && !cupThere)
        {
            cupThere = true;
            cup.SetActive(true);
            Inventory.instance.RemoveItem(droppedItem);
            Inventory.instance.UpdateSlotUI();
        }
        else if (droppedItem.itemName == "Paper Filter" && !filterThere)
        {
            filterThere = true;
            filter.SetActive(true);
            Inventory.instance.RemoveItem(droppedItem);
            Inventory.instance.UpdateSlotUI();
        }
        else if (droppedItem.itemName == "Coffee Grounds" && !groundsThere && filterThere)
        {
            groundsThere = true;
            grounds.SetActive(true);
            Inventory.instance.RemoveItem(droppedItem);
            Inventory.instance.UpdateSlotUI();
        }
        else if (droppedItem.itemName == "Sleeve" && !sleeveThere && cupThere)
        {
            sleeveThere = true;
            sleeve.SetActive(true);
            Inventory.instance.RemoveItem(droppedItem);
            Inventory.instance.UpdateSlotUI();
        }
    }

    public void brewCoffee(){
        if (complete) 
        {
            cupThere = false;
            cup.SetActive(false);
            filterThere = false;
            filter.SetActive(false);
            groundsThere = false;
            grounds.SetActive(false);
            sleeveThere = false;
            sleeve.SetActive(false);

            completedCup.SetActive(true);
            complete = false;
        }
        else
        {
            Debug.Log("can't make coffee");
        }
    }
}
