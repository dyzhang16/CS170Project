using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Yarn.Unity;

public class CheckCoffee : MonoBehaviour , IDropHandler
{
    public GameObject puzzlePanel;
    public DialogueRunner dia;

    public void OnDrop(PointerEventData eventData)
    {
        Item droppedItem = Inventory.instance.itemList[eventData.pointerDrag.GetComponent<ItemDragHandler>().transform.parent.GetSiblingIndex()];
        if (puzzlePanel.GetComponent<CanvasGroup>().alpha == 1)
        {
            if (droppedItem.itemName == "Macho-iatto")
            {
                Debug.Log("You gave the right coffee!");
                puzzlePanel.GetComponent<CanvasGroup>().alpha = 0;
                puzzlePanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
                // allow background interactions
                Inventory.instance.RemoveItem(droppedItem);

                dia.StartDialogue("correct_coffee");
            }
            else 
            {
                Debug.Log("You gave the wrong coffee!");
                puzzlePanel.GetComponent<CanvasGroup>().alpha = 0;
                puzzlePanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
                // allow background interactions
                Inventory.instance.RemoveItem(droppedItem);
                dia.StartDialogue("wrong_coffee");
            }
        }
    }
}
