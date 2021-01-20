using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CoffeePuzzle : MonoBehaviour, IDropHandler
{
    public GameObject puzzlePanel;
    public bool cupThere, filterThere, groundsThere, brewed, lidThere;
    public GameObject cup, filter, grounds, brewedCup, lid, completedCup;

    public void Start() { 
    }

    // Update is called once per frame
    void Update()
    {
        
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
            else if (droppedItem.itemName == "Lid" && brewed)
            {
                brewedCup.SetActive(false);
                lidThere = true;
                lid.SetActive(true);
                Inventory.instance.RemoveItem(droppedItem);
                Inventory.instance.UpdateSlotUI();
            }
            else if (droppedItem.itemName == "Sleeve" && lidThere && brewed)
            {
                lid.SetActive(false);
                completedCup.SetActive(true);
                Inventory.instance.RemoveItem(droppedItem);
                Inventory.instance.UpdateSlotUI();
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
            cupThere = false;
            cup.SetActive(false);
            filterThere = false;
            filter.SetActive(false);
            groundsThere = false;
            grounds.SetActive(false);
            brewed = true;

            brewedCup.SetActive(true);
        }
        else 
        {
            Debug.Log("Missing something");
        }
    }
}
