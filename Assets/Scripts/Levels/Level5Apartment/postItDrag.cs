using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class postItDrag : MonoBehaviour, IDragHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        // GetComponent<CanvasGroup>().alpha = 0.5f;
        // GetComponent<CanvasGroup>().blocksRaycasts = false;
        //Debug.Log("Dragging");
        transform.position = Input.mousePosition;
    }
}
