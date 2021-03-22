using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class HidePuzzleTakeCoffee : MonoBehaviour
{
    public GameObject puzzlePanel;
    public Button RemoveButton;
    public GameObject Drink;

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
            Item boa = Drink.GetComponent<CoffeeAssignment>().droppedCoffee as Drink;
            Inventory.instance.AddItem(boa);
            Drink.SetActive(false);
        }
        else 
        {
            Debug.Log("No Coffee Dropped");
        }
    }
}
