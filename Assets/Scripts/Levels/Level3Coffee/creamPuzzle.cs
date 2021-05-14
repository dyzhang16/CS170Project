using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Yarn.Unity;


public class creamPuzzle : MonoBehaviour, IDropHandler
{
    public GameObject creamPuzzlePanel, coffeeShadow, coffee, creamUI, cream;
    public Button creamButton, exitButton;
    public bool creamShaking, cupThere;
    [HideInInspector]public Item droppedItem;
    private int creamAdded;

    public Text creamText;

    public GameObject[] slots;
    public GameObject[] images;

    [YarnCommand("ResetCream")]
    public void Reset()
    {
        coffeeShadow.SetActive(true);
        coffee.SetActive(false);
        cupThere = false;
        creamShaking = false;
        droppedItem = null;

        creamText.text = "Cream Added: 0";

        //destroy falling cream
        StartCoroutine(DestroyCream());

        //disable inventory drag
        foreach(GameObject obj in slots){
            obj.GetComponent<InventorySlot>().allowDrag = false;
        }

        foreach(GameObject obj in images){
            obj.GetComponent<ItemDragHandler>().allowDrag = false;
        }

        //close inv
        GameObject.Find("InventoryController").GetComponent<OpenMenus>().closeInv();
    }

    IEnumerator DestroyCream(){
        GameObject creams = GameObject.Find("cream(Clone)");

        while (creams != null){
            Destroy(creams);

            yield return new WaitForSeconds(0.05f);

            creams = GameObject.Find("cream(Clone)");
        }
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

                    //set cream added variable
                    creamText.text = "Cream Added: " + creamAdded;
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
                StartCoroutine(ShakingCream(0.1f));
            }
            
        }
    }
    public void setCream()
    {
        if (droppedItem is Drink)
        {
            var boa = droppedItem as Drink;
            boa.Cream = creamAdded;
            creamText.text = "Cream Added: " + creamAdded;
            // Debug.Log("This coffee contains: " + boa.Sugar + " amount of sugar.");
            // Debug.Log("This coffee contains: " + boa.Cream + " amount of cream.");
        }
    }
    IEnumerator ShakingCream(float waitTime)
    {
        creamShaking = !creamShaking;
        creamButton.enabled = false;
        creamUI.transform.Rotate(0, 0, 35);
        ++creamAdded;
        setCream();
        // Debug.Log("You've added " + creamAdded);
        Vector3 pos = new Vector3(coffee.transform.position.x, coffee.transform.position.y + 400, coffee.transform.position.z);
        GameObject creams = Instantiate(cream, pos, coffee.transform.localRotation, transform);
        creams.transform.SetAsFirstSibling();
        yield return new WaitForSeconds(waitTime);
        creamShaking = !creamShaking;
        creamUI.transform.Rotate(0, 0, -35);
        creamButton.enabled = true;
        // Debug.Log("Finished Couroutine");
    }
}
