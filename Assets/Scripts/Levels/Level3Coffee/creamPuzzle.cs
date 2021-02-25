using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Yarn.Unity;

public class creamPuzzle : MonoBehaviour, IDropHandler
{
    public GameObject creamPuzzlePanel, coffeeUI, creamUI, cream;
    public int creamAdded;
    public bool creamShaking, cupThere;
    public Item droppedItem;

    private UnityEngine.UI.Button exitButton;
    private Quaternion originalRotation;
    [YarnCommand("ResetCream")]

    public void Reset()
    {
        cupThere = false;
        creamShaking = false;
        creamAdded = 0;
        creamUI.transform.rotation = originalRotation;
        coffeeUI.SetActive(false);
        // set exitButton to be interactable
        exitButton.interactable = true;
    }
    public void Awake()
    { 
        originalRotation = creamUI.transform.rotation;
    }
    public void Start()
    {
        // get the exitButton from all of this GameObject's puzzle's children
        // https://answers.unity.com/questions/594210/get-all-children-gameobjects.html
        foreach (Transform child in transform)
        {
            // if child has the HidePuzzle component (seen in GameObjects for exiting a puzzle)
            if (child.GetComponent<HidePuzzle>())
            {
                // assign the exit button
                exitButton = child.GetComponent<UnityEngine.UI.Button>();
                break;
            }
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        droppedItem = Inventory.instance.itemList[eventData.pointerDrag.GetComponent<ItemDragHandler>().transform.parent.GetSiblingIndex()];
        if (droppedItem)
        {
            coffeeUI.GetComponent<CoffeeAssignment>().droppedCoffee = droppedItem;
        }
        
        if (creamPuzzlePanel.GetComponent<CanvasGroup>().alpha == 1)
        {
            if (droppedItem is Drink)
            {
                var boa = droppedItem as Drink;
                Debug.Log("This is the Drink: " + droppedItem.itemName);
                if (droppedItem.itemName == "Random Coffee")
                {
                    creamAdded = boa.Cream;
                    cupThere = true;
                    coffeeUI.SetActive(true);
                    Inventory.instance.RemoveItem(droppedItem);
                    Inventory.instance.UpdateSlotUI();

                    // prevent user from selecting the exit button
                    exitButton.interactable = false;
                }
            }
        }
    }
    public void addCream()
    {
        if (cupThere)
        {
            if (!creamShaking)
            {
                creamShaking = !creamShaking;
                creamUI.transform.Rotate(0, 0, 35);
                ++creamAdded;
                setCream();
                Debug.Log("You've added" + creamAdded);
                Vector3 pos = new Vector3(creamUI.transform.position.x - 125, creamUI.transform.position.y, creamUI.transform.position.z);
                GameObject creams = Instantiate(cream, pos, creamUI.transform.localRotation, transform);
                creams.GetComponent<fallingstuff>().timeRemaining = 0.8f;
                creams.GetComponent<fallingstuff>().timerIsRunning = true;
            }
            else
            {
                creamShaking = !creamShaking;
                creamUI.transform.Rotate(0, 0, -35);
            }
        }
    }
    public void setCream()
    {
        if (droppedItem is Drink)
        {
            var boa = droppedItem as Drink;
            boa.Cream = creamAdded;
            Debug.Log("This coffee contains: " + boa.Sugar + " amount of sugar.");
            Debug.Log("This coffee contains: " + boa.Cream + " amount of cream.");
        }
    }
}
