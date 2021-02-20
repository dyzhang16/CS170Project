using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Yarn.Unity;
using UnityEngine.UI;

public class Puzzle1 : MonoBehaviour, IDropHandler
{
    public VariableStorageBehaviour CustomVariableStorage;
    public GameObject puzzlePanel;
    public GameObject FlowerA, FlowerB, FlowerC, FlowerD;
    public static Puzzle1 instance;
    bool flowerASet, flowerBSet, flowerCSet, flowerDSet = false;

    public Sprite flowerAComplete, flowerBComplete, flowerCComplete, flowerDComplete;
    
    public Item key;

    private void Start()
    {
        instance = this;
    }
    private void Update()
    {
        if (flowerASet == true && flowerBSet == true && flowerCSet == true && flowerDSet == true)
        {
            SoundManagerScript.PlaySound("flower_success"); // plays sound tutorial puzzle complete
            CustomVariableStorage.SetValue("$puzzle", 1);
            GameManager.instance.flowerPuzzle = 1;
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
        if (droppedItem.itemName == "White Flower")
        {
            flowerASet = true;
            FlowerA.transform.GetComponent<Image>().sprite = flowerAComplete;
            Inventory.instance.RemoveItem(droppedItem);
            Inventory.instance.UpdateSlotUI();
        }
        else if (droppedItem.itemName == "Red Flower")
        {
            flowerBSet = true;
            FlowerB.transform.GetComponent<Image>().sprite = flowerBComplete;
            Inventory.instance.RemoveItem(droppedItem);
            Inventory.instance.UpdateSlotUI();
        }
        else if (droppedItem.itemName == "Yellow Flower")
        {
            flowerCSet = true;
            FlowerC.transform.GetComponent<Image>().sprite = flowerCComplete;
            Inventory.instance.RemoveItem(droppedItem);
            Inventory.instance.UpdateSlotUI();
        }
        else if (droppedItem.itemName == "Purple Flower")
        {
            flowerDSet = true;
            FlowerD.transform.GetComponent<Image>().sprite = flowerDComplete;
            Inventory.instance.RemoveItem(droppedItem);
            Inventory.instance.UpdateSlotUI();
        }
        else
        {
            //Debug.Log("NotCorrectItem");
        }
    }
}