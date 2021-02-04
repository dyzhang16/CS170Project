using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Yarn;
using Yarn.Unity;

public class DocumentPuzzle : MonoBehaviour,IPointerClickHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private Transform originalParent;
    public bool isDragging;
    public GameObject Stamps, Signature;

    public void Start()
    {
        Debug.Log("Created a new Document");
    }
    public void Stamp()
    {
        var mousePos = Input.mousePosition;
        Instantiate(Stamps, mousePos, transform.localRotation, transform);
    }
    public void Sign()
    {
        var mousePos = Input.mousePosition;
        Instantiate(Signature, mousePos, transform.localRotation, transform);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isDragging)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                Sign();
                Debug.Log("Left click");
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                Stamp();
                Debug.Log("Right click");
            }
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = false;
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            originalParent = transform.parent;
            transform.SetParent(transform.parent.parent);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        isDragging = true;
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            GetComponent<CanvasGroup>().alpha = 0.5f;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
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
    }
}
