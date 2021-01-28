using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public GameObject icon;
    public void UpdateSlot()
    {
        if (Inventory.instance.itemList[transform.GetSiblingIndex()] != null)
        {
            icon.GetComponent<UnityEngine.UI.Image>().sprite = Inventory.instance.itemList[transform.GetSiblingIndex()].icon;
            icon.SetActive(true);
        }
        else
        {
            icon.SetActive(false);
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        Item droppedItem = Inventory.instance.itemList[eventData.pointerDrag.GetComponent<ItemDragHandler>().transform.parent.GetSiblingIndex()];
        // if on itself
        if (eventData.pointerDrag.transform.parent.name == gameObject.name)
        {
            //Debug.Log("on itself");
            return;
        }
        else if (Inventory.instance.itemList[transform.GetSiblingIndex()] == null)  // empty slot
        {
            //Debug.Log("on empty slot");
            Inventory.instance.itemList[transform.GetSiblingIndex()] = droppedItem;
            Inventory.instance.itemList[eventData.pointerDrag.GetComponent<ItemDragHandler>().transform.parent.GetSiblingIndex()] = null;
            Inventory.instance.UpdateSlotUI();
        }
        else //non-empty slot
        {
            //Debug.Log("on non-empty slot");
            Item tempItem = Inventory.instance.itemList[transform.GetSiblingIndex()];
            Inventory.instance.itemList[transform.GetSiblingIndex()] = droppedItem;
            Inventory.instance.itemList[eventData.pointerDrag.GetComponent<ItemDragHandler>().transform.parent.GetSiblingIndex()] = tempItem;
            Inventory.instance.UpdateSlotUI();
        }
    }
}