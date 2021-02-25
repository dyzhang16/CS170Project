using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Yarn.Unity;

public class CheckCoffee : MonoBehaviour , IDropHandler
{
    public GameObject puzzlePanel;
    public VariableStorageBehaviour CustomVariableStorage;
    public DialogueRunner dia;

    public void OnDrop(PointerEventData eventData)
    {
        Item droppedItem = Inventory.instance.itemList[eventData.pointerDrag.GetComponent<ItemDragHandler>().transform.parent.GetSiblingIndex()];
        if (puzzlePanel.GetComponent<CanvasGroup>().alpha == 1)
        {
            if (droppedItem.itemName == "Moo-cha™")
            {
                Debug.Log("You gave the right coffee!");
                CustomVariableStorage.SetValue("$CoffeePuzzle", 1);
                puzzlePanel.GetComponent<CanvasGroup>().alpha = 0;
                puzzlePanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
                GameManager.instance.coffeePuzzle = 1;
                // allow background interactions
                GameObject blocker = GameObject.Find("Canvas/Blocker");
                if (blocker)
                {
                    blocker.GetComponent<CanvasGroup>().alpha = 0;
                    blocker.GetComponent<CanvasGroup>().blocksRaycasts = false;
                }
                Inventory.instance.RemoveItem(droppedItem);

                dia.StartDialogue("CorrectCoffee");
            }
            else 
            {
                Debug.Log("Not Coffee");
            }
        }
    }
}
