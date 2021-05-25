using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Yarn.Unity;

public class CheckCoffee : MonoBehaviour , IDropHandler
{
    //used to hide puzzlePanel
    public GameObject puzzlePanel;
    //used to start dialogue
    public DialogueRunner dia;
    //used to temporarily disable exitButton while showing dropped item
    public Button exitButton;

    //Placeholder for original sprite
    private Sprite EmptySprite;
    //Blocker
    private GameObject blocker;
    
    public void Start()
    {
        blocker = GameObject.Find("Canvas/Blocker");
        EmptySprite = GetComponent<Image>().sprite;
    }
    public void OnDrop(PointerEventData eventData)
    {
        Item droppedItem = Inventory.instance.itemList[eventData.pointerDrag.GetComponent<ItemDragHandler>().transform.parent.GetSiblingIndex()];
        if (puzzlePanel.GetComponent<CanvasGroup>().alpha == 1)
        {
            if (droppedItem is Drink)
            {
                if (droppedItem.itemName == "Macho-iatto")
                {
                    Inventory.instance.RemoveItem(droppedItem);
                    GetComponent<Image>().sprite = droppedItem.icon;
                    exitButton.GetComponent<Button>().interactable = false;
                    GameManager.instance.gaveDrink = 1;
                    exitButton.gameObject.GetComponent<HidePuzzle>().disableDrag();
                    StartCoroutine(WaitCoroutine(3, 1));
                }
                else if (droppedItem.itemName == "Random Coffee")
                {
                    Inventory.instance.RemoveItem(droppedItem);
                    GetComponent<Image>().sprite = droppedItem.icon;
                    exitButton.GetComponent<Button>().interactable = false;
                    StartCoroutine(WaitCoroutine(3, 4));
                }
                else
                {
                    Inventory.instance.RemoveItem(droppedItem);
                    GetComponent<Image>().sprite = droppedItem.icon;
                    exitButton.GetComponent<Button>().interactable = false;
                    StartCoroutine(WaitCoroutine(3, 2));
                }
            }
            else
            {
                // Inventory.instance.RemoveItem(droppedItem);
                // GetComponent<Image>().sprite = droppedItem.icon;
                // exitButton.GetComponent<Button>().interactable = false;
                // StartCoroutine(WaitCoroutine(1, 3));
            }
        }
    }
    IEnumerator WaitCoroutine(float waitTime, int i)
    {
        yield return new WaitForSeconds(waitTime);
        puzzlePanel.GetComponent<CanvasGroup>().alpha = 0;
        puzzlePanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
        if (blocker)
        {
            blocker.GetComponent<CanvasGroup>().alpha = 0;
            blocker.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
        switch (i)
        {
            case 1:
                dia.StartDialogue("correct_coffee");
                break;
            case 2:
                dia.StartDialogue("wrong_coffee");
                break;
            case 3:
                dia.StartDialogue("wrong_item");
                break;
            case 4:
                dia.StartDialogue("no_safety");
                break;
            default:
                Debug.Log("Your switch statement in CheckCoffee is hot garbage.");
                break;
        }
        exitButton.GetComponent<Button>().interactable = true;
        GetComponent<Image>().sprite = EmptySprite;
    }
}
