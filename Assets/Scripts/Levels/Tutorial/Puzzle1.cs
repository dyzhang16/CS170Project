using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Yarn.Unity;

public class Puzzle1 : MonoBehaviour, IDropHandler
{
    public Sprite completeSprite;
    public VariableStorageBehaviour CustomVariableStorage;
    public GameObject puzzlePanel;
    public GameObject FlowerA, FlowerB, FlowerC, FlowerD;
    public static Puzzle1 instance;
    bool flowerASet, flowerBSet, flowerCSet, flowerDSet = false;
    
    public Item key;

    private void Start()
    {
        instance = this;
    }
    private void Update()
    {
        if (flowerASet == true && flowerBSet == true && flowerCSet == true && flowerDSet == true)
        {
            gameObject.GetComponent<UnityEngine.UI.Image>().sprite = completeSprite;
            SoundManagerScript.PlaySound("flower_success"); // plays sound tutorial puzzle complete
            CustomVariableStorage.SetValue("$puzzle", 1);
            Inventory.instance.AddItem(key);
            Hide();
        }
    }

    public void Hide()
    {
        Debug.Log("Close");
        puzzlePanel.SetActive(false);
    }

    public void sound()
    {
        SoundManagerScript.PlaySound("pickup_flower_2"); // pickup item sound
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