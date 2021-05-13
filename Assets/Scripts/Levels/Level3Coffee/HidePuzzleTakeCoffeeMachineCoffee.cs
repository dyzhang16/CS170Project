using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class HidePuzzleTakeCoffeeMachineCoffee : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject puzzlePanel;
    public Button RemoveButton;
    public GameObject Drink;
    public DialogueRunner dialogueRunner;

    public GameObject[] slots;
    public GameObject[] images;

    public void HidePuzzleCoffee()
    {
        puzzlePanel.GetComponent<CanvasGroup>().alpha = 0;
        puzzlePanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
        // allow background interactions
        GameObject blocker = GameObject.Find("Canvas/Blocker");
        if (blocker)
        {
            blocker.GetComponent<CanvasGroup>().alpha = 0;
            blocker.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
        if (Drink.activeSelf)
        {
            dialogueRunner.StartDialogue("TakeCoffeeCM");
            Drink.SetActive(false);
        }
        else
        {
            // Debug.Log("No Coffee Dropped");
        }

        //disable drag
        foreach(GameObject obj in slots){
            obj.GetComponent<InventorySlot>().allowDrag = false;
        }

        foreach(GameObject obj in images){
            obj.GetComponent<ItemDragHandler>().allowDrag = false;
        }
    }
}
