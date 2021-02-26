using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private Transform originalParent;
    public bool isDragging;

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = false;
        if (Inventory.instance.itemList[transform.parent.GetSiblingIndex()] != null)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                originalParent = transform.parent;
                transform.SetParent(transform.parent.parent);
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        isDragging = true;
        if (Inventory.instance.itemList[originalParent.transform.GetSiblingIndex()] != null && eventData.button == PointerEventData.InputButton.Left)
        {
            GetComponent<CanvasGroup>().alpha = 0.5f;
            GetComponent<RectTransform>().localScale = new Vector3(9,9,9);
            GetComponent<CanvasGroup>().blocksRaycasts = false;
            //Debug.Log("Dragging");
            transform.position = Input.mousePosition;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            GetComponent<CanvasGroup>().alpha = 1f;
            GetComponent<RectTransform>().localScale = new Vector3(3, 3, 3);
            transform.SetParent(originalParent);
            transform.localPosition = Vector3.zero;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }

        // Debug.Log(eventData.pointerDrag.transform.parent);
        // Debug.Log(eventData.position);
        // Debug.Log(Camera.main.ScreenToWorldPoint(eventData.position));
    }
}