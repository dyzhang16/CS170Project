using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class addPen : MonoBehaviour, IDropHandler
{
    public Item droppedItem;
    public GameObject pen, documentPuzzlePanel;

    public void OnDrop(PointerEventData eventData)
    {
        droppedItem = Inventory.instance.itemList[eventData.pointerDrag.GetComponent<ItemDragHandler>().transform.parent.GetSiblingIndex()];
        if (documentPuzzlePanel.GetComponent<CanvasGroup>().alpha == 1)
        {
            if (droppedItem.itemName == "Pen")
            {
                Debug.LogWarning("Dropping Pen");
                this.gameObject.SetActive(false);
                pen.SetActive(true);
                Inventory.instance.RemoveItem(droppedItem);
                Inventory.instance.UpdateSlotUI();            
            }
        }
    }
}
