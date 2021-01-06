using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{

    private Transform originalParent;
    public List<GameObject> droppers = new List<GameObject>();
    public bool coffee = false;
    public GameObject creamPuzzle;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Inventory.instance.itemList[transform.parent.GetSiblingIndex()] != null)
        {
            Item pickedItem = Inventory.instance.itemList[transform.parent.GetSiblingIndex()];

            if (pickedItem.name == "coffee"){
                coffee = !coffee;
                creamPuzzle.SetActive(coffee);
            }
            
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                GetComponent<CanvasGroup>().alpha = 0.5f;
                originalParent = transform.parent;
                transform.SetParent(transform.parent.parent);
                GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Inventory.instance.itemList[originalParent.transform.GetSiblingIndex()] != null && eventData.button == PointerEventData.InputButton.Left)
        {
            //Debug.Log("Dragging");
            transform.position = Input.mousePosition;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            GetComponent<CanvasGroup>().alpha = 1f;
            transform.SetParent(originalParent);
            transform.localPosition = Vector3.zero;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }

        // Debug.Log(eventData.pointerDrag.transform.parent);
        // Debug.Log(eventData.position);
        // Debug.Log(Camera.main.ScreenToWorldPoint(eventData.position));
    }
}