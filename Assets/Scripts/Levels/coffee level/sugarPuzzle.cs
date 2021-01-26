using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Yarn.Unity;

public class sugarPuzzle : MonoBehaviour, IDropHandler
{
    public GameObject sugarPuzzlePanel, coffeeUI, sugarUI, sugar;
    public VariableStorageBehaviour CustomVariableStorage;
    public int sugarAdded = 0;
    public bool sweetShaking , cupThere;
    public Item droppedItem;

    private Quaternion originalRotation;
    [YarnCommand("ResetSugar")]

    public void Reset()
    {
        cupThere = false;
        sweetShaking = false;
        sugarAdded = 0;
        sugarUI.transform.rotation = originalRotation;
    }
    public void Awake()
    {
        originalRotation = sugarUI.transform.rotation;
    }
    public void OnDrop(PointerEventData eventData)
    {
        droppedItem = Inventory.instance.itemList[eventData.pointerDrag.GetComponent<ItemDragHandler>().transform.parent.GetSiblingIndex()];
        if (droppedItem)
        {
            coffeeUI.GetComponent<CoffeeAssignment>().droppedCoffee = droppedItem;
        }
        if (sugarPuzzlePanel.GetComponent<CanvasGroup>().alpha == 1)
        {
            if (droppedItem is Drink)
            {
                var boa = droppedItem as Drink;
                Debug.Log("This is the Drink: " + boa);
                if (droppedItem.itemName == "Random Coffee")
                {
                    sugarAdded = boa.Sugar;
                    cupThere = true;
                    coffeeUI.SetActive(true);
                    Inventory.instance.RemoveItem(droppedItem);
                    Inventory.instance.UpdateSlotUI();
                }
            }
        }
    }
    public void addSugar()
    {
        if (cupThere)
        {
            if (!sweetShaking)
            {
                sweetShaking = !sweetShaking;
                sugarUI.transform.Rotate(0, 0, -35);
                ++sugarAdded;
                setSugar();
                Debug.Log("You've added: " + sugarAdded);
                Vector3 pos = new Vector3(sugarUI.transform.position.x + 70, sugarUI.transform.position.y, sugarUI.transform.position.z);
                GameObject sugs = Instantiate(sugar, pos, sugarUI.transform.localRotation, transform);
            }
            else
            {
                sugarUI.transform.Rotate(0, 0, 35);
                sweetShaking = !sweetShaking;
            }
        }
        else 
        {
            Debug.Log("Can't Drop Sugar");
        }
    }
    public void setSugar()
    {
        if (droppedItem is Drink)
        {
            var boa = droppedItem as Drink;
            boa.Sugar = sugarAdded;
            Debug.Log("This coffee contains: " + boa.Sugar + " amount of sugar.");
            Debug.Log("This coffee contains: " + boa.Cream + " amount of cream.");
        }
    }
}
