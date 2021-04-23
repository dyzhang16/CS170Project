using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Yarn;
using Yarn.Unity;

public class DocumentPuzzle : MonoBehaviour, IPointerClickHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
{

    public GameObject Stamps, Signature, SignArea, StampArea; 
    public bool isDragging = false;
    [HideInInspector] public bool stampedSpace, signedSpace = false;
    private Transform originalParent;
    private bool stamp, sign = false;

    public void Start()
    {
        Debug.Log("Created a new Document");
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isDragging)
        {
            GameObject Arrow = GameObject.Find("Arrow");
            if (Arrow)
            {
                Arrow.GetComponent<CanvasGroup>().alpha = 1;
            }
            else 
            {
                Debug.LogError("Could not find");
            }
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                Sign();
                if (SignArea.GetComponent<mouseOver>().isMouseOver && !sign)
                {
                    signedSpace = true;
                }
                else
                {
                    signedSpace = false;
                    sign = true;
                }
            }
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                Stamp();
                if (StampArea.GetComponent<mouseOver>().isMouseOver && !stamp)
                {
                    stampedSpace = true;
                }
                else
                {
                    stampedSpace = false;
                    stamp = true;
                }
            }
        }
    }
    private void Stamp()
    {
        var mousePos = Input.mousePosition;
        Instantiate(Stamps, mousePos, transform.localRotation, transform);
    }
    private void Sign()
    {
        var mousePos = Input.mousePosition;
        Instantiate(Signature, mousePos, transform.localRotation, transform);
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
