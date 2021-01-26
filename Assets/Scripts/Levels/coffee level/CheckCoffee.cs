using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CheckCoffee : MonoBehaviour , IDropHandler
{
    public GameObject puzzlePanel;
    public Drink droppedItem;
    public void OnDrop(PointerEventData eventData)
    {
        if (puzzlePanel.GetComponent<CanvasGroup>().alpha == 1)
        {
            if (droppedItem.itemName == "Coffee")
            {
                if (droppedItem.Sugar == 10 && droppedItem.Cream == 10) 
                {
                    Debug.Log("You gave the right coffee!");
                }
            }
            else 
            {
                Debug.Log("Not Coffee");
            }
        }
    }
}
