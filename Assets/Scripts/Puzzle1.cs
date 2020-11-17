using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Puzzle1 : MonoBehaviour, IDropHandler
{
    public Sprite completeSprite;
    public GameObject puzzlePanel;
    public GameObject FlowerA, FlowerB, FlowerC, FlowerD;
    public static Puzzle1 instance;
    bool flowerASet, flowerBSet, flowerCSet, flowerDSet = false;
    private void Start()
    {
        instance = this;
    }
    private void Update()
    {
        if (flowerASet == true && flowerBSet == true && flowerCSet == true && flowerDSet == true)
        {
            gameObject.GetComponent<UnityEngine.UI.Image>().sprite = completeSprite;
            Hide();
        }
    }

    public void Hide()
    {
        Debug.Log("Close");
        puzzlePanel.SetActive(false);
    }
    public void OnDrop(PointerEventData eventData)
    {
        Item droppedItem = Inventory.instance.itemList[eventData.pointerDrag.GetComponent<ItemDragHandler>().transform.parent.GetSiblingIndex()];
        if (droppedItem.itemName == "FlowerA")
        {
            flowerASet = true;
            FlowerA.SetActive(true);
            Inventory.instance.RemoveItem(droppedItem);
            Inventory.instance.UpdateSlotUI();
            //Debug.Log(flowerASet);
        }
        else if (droppedItem.itemName == "FlowerB")
        {
            flowerBSet = true;
            FlowerB.SetActive(true);
            Inventory.instance.RemoveItem(droppedItem);
            Inventory.instance.UpdateSlotUI();
            //Debug.Log(flowerBSet);
        }
        else if (droppedItem.itemName == "FlowerC")
        {
            flowerCSet = true;
            FlowerC.SetActive(true);
            Inventory.instance.RemoveItem(droppedItem);
            Inventory.instance.UpdateSlotUI();
            //Debug.Log(flowerCSet);
        }
        else if (droppedItem.itemName == "FlowerD")
        {
            flowerDSet = true;
            FlowerD.SetActive(true);
            Inventory.instance.RemoveItem(droppedItem);
            Inventory.instance.UpdateSlotUI();
            //Debug.Log(flowerDSet);
        }
        else
        {
            //Debug.Log("NotCorrectItem");
        }
    }
}