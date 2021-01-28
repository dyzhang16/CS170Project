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
    
    private Quaternion originalRotation;
    [YarnCommand("ResetCream")]

    public void Reset()
    {
        cupThere = false;
        creamShaking = false;
        creamAdded = 0;
        creamUI.transform.rotation = originalRotation;
        coffeeUI.SetActive(false);
    }
    public void Awake()
    { 
        originalRotation = creamUI.transform.rotation;
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
                Debug.Log("This is the Drink: " + boa);
                if (droppedItem.itemName == "Random Coffee")
                {
                    creamAdded = boa.Cream;
                    cupThere = true;
                    coffeeUI.SetActive(true);
                    Inventory.instance.RemoveItem(droppedItem);
                    Inventory.instance.UpdateSlotUI();
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
                Vector3 pos = new Vector3(creamUI.transform.position.x - 70, creamUI.transform.position.y, creamUI.transform.position.z);
                GameObject creams = Instantiate(cream, pos, creamUI.transform.localRotation, transform);
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
