using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CheckCoffee : MonoBehaviour , IDropHandler
{
    public GameObject puzzlePanel;

    public void OnDrop(PointerEventData eventData)
    {
        Item droppedItem = Inventory.instance.itemList[eventData.pointerDrag.GetComponent<ItemDragHandler>().transform.parent.GetSiblingIndex()];
        if (puzzlePanel.GetComponent<CanvasGroup>().alpha == 1)
        {
            if (droppedItem.itemName == "Milk™")
            {
                Debug.Log("You gave the right coffee!");
            }
            else 
            {
                Debug.Log("Not Coffee");
            }
        }
    }
}
