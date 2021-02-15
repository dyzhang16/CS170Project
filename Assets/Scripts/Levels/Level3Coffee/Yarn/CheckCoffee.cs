using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Yarn.Unity;

public class CheckCoffee : MonoBehaviour , IDropHandler
{
    public GameObject puzzlePanel;
    public VariableStorageBehaviour CustomVariableStorage;

    public void OnDrop(PointerEventData eventData)
    {
        Item droppedItem = Inventory.instance.itemList[eventData.pointerDrag.GetComponent<ItemDragHandler>().transform.parent.GetSiblingIndex()];
        if (puzzlePanel.GetComponent<CanvasGroup>().alpha == 1)
        {
            if (droppedItem.itemName == "Milk™")
            {
                Debug.Log("You gave the right coffee!");
                CustomVariableStorage.SetValue("$CoffeePuzzle", 1);
                puzzlePanel.GetComponent<CanvasGroup>().alpha = 0;
                puzzlePanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
                // allow background interactions
                GameObject blocker = GameObject.Find("Canvas/Blocker");
                if (blocker)
                {
                    blocker.GetComponent<CanvasGroup>().alpha = 0;
                    blocker.GetComponent<CanvasGroup>().blocksRaycasts = false;
                }
                Inventory.instance.RemoveItem(droppedItem);
            }
            else 
            {
                Debug.Log("Not Coffee");
            }
        }
    }
}
