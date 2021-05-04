using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using Yarn.Unity;

public class sugarPuzzle : MonoBehaviour, IDropHandler
{
    public GameObject sugarPuzzlePanel, coffeeShadow, coffee, sugarUI , sugar;
    public Sprite pushedSugar;
    public Button sugarButton, exitButton;
    private int sugarAdded;
    public bool sugarShaking , cupThere;
    [HideInInspector]public Item droppedItem;
    private Sprite tempSprite;
    void Start()
    {
        tempSprite = sugarUI.GetComponent<Image>().sprite;
    }

    [YarnCommand("ResetSugar")]
    public void Reset()
    {
        sugarUI.GetComponent<Image>().sprite = tempSprite;
        cupThere = false;
        sugarShaking = false;
        coffeeShadow.SetActive(true);
        coffee.SetActive(false);
        cupThere = false;
        droppedItem = null;
    }
    public void OnDrop(PointerEventData eventData)
    {
        droppedItem = Inventory.instance.itemList[eventData.pointerDrag.GetComponent<ItemDragHandler>().transform.parent.GetSiblingIndex()];
        if (droppedItem)
        {
            coffee.GetComponent<CoffeeAssignment>().droppedCoffee = droppedItem;
        }

        if (sugarPuzzlePanel.GetComponent<CanvasGroup>().alpha == 1)
        {
            if (droppedItem is Drink)
            {
                var boa = droppedItem as Drink;
                if (droppedItem.itemName == "Random Coffee")
                {
                    sugarAdded = boa.Sugar;
                    cupThere = true;
                    coffeeShadow.SetActive(false);
                    coffee.SetActive(true);
                    Inventory.instance.RemoveItem(droppedItem);
                    Inventory.instance.UpdateSlotUI();
                }
                else
                { 
                
                }
            }
        }
    }
    public void addSugar()
    {
        if (cupThere)
        {
            if (!sugarShaking)
            {
                StartCoroutine(ShakingSugar(0.1f));
            }

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
    IEnumerator ShakingSugar(float waitTime)
    {
        sugarShaking = !sugarShaking;
        sugarButton.enabled = false;
        sugarUI.GetComponent<Image>().sprite = pushedSugar;
        ++sugarAdded;
        setSugar();
        Debug.Log("You've added " + sugarAdded);
        Vector3 pos = new Vector3(coffee.transform.position.x, coffee.transform.position.y + 400, coffee.transform.position.z);
        GameObject sugars = Instantiate(sugar, pos, coffee.transform.localRotation, transform);
        sugars.transform.SetAsFirstSibling();
        yield return new WaitForSeconds(waitTime);
        sugarShaking = !sugarShaking;
        sugarUI.GetComponent<Image>().sprite = tempSprite;
        sugarButton.enabled = true;
        Debug.Log("Finished Couroutine");
    }
}
