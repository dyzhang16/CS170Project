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
    public DialogueRunner dia;
    public GameObject puzzlePanel;
    public Button exitButton;
    public GameObject FlowerA, FlowerB, FlowerC, FlowerD;
    bool flowerASet, flowerBSet, flowerCSet, flowerDSet = false;

    public GameObject bouqet;

    public Sprite flowerAComplete, flowerBComplete, flowerCComplete, flowerDComplete;
    
/*    private void Update()
    {
        if (flowerASet == true && flowerBSet == true && flowerCSet == true && flowerDSet == true)
        {
            
            
            bouqet.SetActive(true);
            GameManager.instance.flowerPuzzle = 1;
        }
    }*/

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
            SoundManagerScript.PlaySound("place_flower"); 
            FlowerA.transform.GetComponent<Image>().sprite = flowerAComplete;
            Inventory.instance.RemoveItem(droppedItem);
        }
        else if (droppedItem.itemName == "Red Flower")
        {
            flowerBSet = true;
            SoundManagerScript.PlaySound("place_flower");
            FlowerB.transform.GetComponent<Image>().sprite = flowerBComplete;
            Inventory.instance.RemoveItem(droppedItem);

        }
        else if (droppedItem.itemName == "Yellow Flower")
        {
            flowerCSet = true;
            SoundManagerScript.PlaySound("place_flower");
            FlowerC.transform.GetComponent<Image>().sprite = flowerCComplete;
            Inventory.instance.RemoveItem(droppedItem);

        }
        else if (droppedItem.itemName == "Purple Flower")
        {
            flowerDSet = true;
            SoundManagerScript.PlaySound("place_flower");
            FlowerD.transform.GetComponent<Image>().sprite = flowerDComplete;
            Inventory.instance.RemoveItem(droppedItem);
        }
        else
        {
            //Debug.Log("NotCorrectItem");
        }
        if (flowerASet == true && flowerBSet == true && flowerCSet == true && flowerDSet == true)
        {
            SoundManagerScript.PlaySound("flower_success"); // plays sound tutorial puzzle complete
            CustomVariableStorage.SetValue("$puzzle", 1);
            //exitButton.GetComponent<HidePuzzleAndDialogue>().puzzleFinished = true;
            bouqet.SetActive(true);
            GameManager.instance.flowerPuzzle = 1;
            exitButton.GetComponent<Button>().interactable = false;
            StartCoroutine(WaitCoroutine(4));
        }
    }
    IEnumerator WaitCoroutine(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        puzzlePanel.GetComponent<CanvasGroup>().alpha = 0;
        puzzlePanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
        dia.StartDialogue("prologue_doneExplore");
    }
}