using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Yarn.Unity;


public class creamPuzzle : MonoBehaviour, IDropHandler
{
    public GameObject creamPuzzlePanel, coffeeShadow, coffee, creamUI, cream;
    public Button creamButton;
    public bool creamShaking, cupThere;
    [HideInInspector]public Item droppedItem;
    private int creamAdded;
    [YarnCommand("ResetCream")]
    public void Reset()
    {
        coffeeShadow.SetActive(true);
        coffee.SetActive(false);
        cupThere = false;
        creamShaking = false;
        droppedItem = null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        droppedItem = Inventory.instance.itemList[eventData.pointerDrag.GetComponent<ItemDragHandler>().transform.parent.GetSiblingIndex()];
        if (droppedItem)
        {
            coffee.GetComponent<CoffeeAssignment>().droppedCoffee = droppedItem;
        }
    
        if (creamPuzzlePanel.GetComponent<CanvasGroup>().alpha == 1)
        {
            if (droppedItem is Drink)
            {
                var boa = droppedItem as Drink;
                if (droppedItem.itemName == "Random Coffee")
                {
                    creamAdded = boa.Cream;
                    cupThere = true;
                    coffeeShadow.SetActive(false);
                    coffee.SetActive(true);
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
                StartCoroutine(ShakingCream(2));
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
    IEnumerator ShakingCream(float waitTime)
    {
        creamShaking = !creamShaking;
        creamButton.enabled = false;
        creamUI.transform.Rotate(0, 0, 35);
        ++creamAdded;
        setCream();
        Debug.Log("You've added " + creamAdded);
        Vector3 pos = new Vector3(creamUI.transform.position.x - 250, creamUI.transform.position.y, creamUI.transform.position.z);
        GameObject creams = Instantiate(cream, pos, creamUI.transform.localRotation, transform);
        creams.transform.SetAsFirstSibling();
        yield return new WaitForSeconds(waitTime);
        creamShaking = !creamShaking;
        creamUI.transform.Rotate(0, 0, -35);
        creamButton.enabled = true;
        Debug.Log("Finished Couroutine"); 

    }
}
