using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class addStamp : MonoBehaviour, IDropHandler
{
    public Item droppedItem;
    public GameObject stamp, documentPuzzlePanel;

    public void OnDrop(PointerEventData eventData)
    {
        droppedItem = Inventory.instance.itemList[eventData.pointerDrag.GetComponent<ItemDragHandler>().transform.parent.GetSiblingIndex()];
        if (documentPuzzlePanel.GetComponent<CanvasGroup>().alpha == 1)
        {
            if (droppedItem.itemName == "Stamp")
            {
                this.gameObject.SetActive(false);
                stamp.SetActive(true);
                Inventory.instance.RemoveItem(droppedItem);
                Inventory.instance.UpdateSlotUI();
            }
        }
    }
}
