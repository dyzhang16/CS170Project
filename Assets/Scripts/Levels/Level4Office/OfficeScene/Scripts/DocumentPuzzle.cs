using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class DocumentPuzzle : MonoBehaviour, IPointerClickHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public GameObject Stamps, Signature, SignArea, StampArea, StampAreaTwo, SignDocument;
    [HideInInspector] public bool signedDocument, stampedDocument, signed, stamped, halfStamped = false;
    public bool isDragging = false;
    private Transform originalParent, SignParent;

    public void Start()
    {
        Debug.Log("Created a new Document");
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isDragging)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                Sign();
                GameObject Note = GameObject.Find("StickyNote");
                if (Note)
                {
                    Note.GetComponent<CanvasGroup>().alpha = 1;
                }
                else
                {
                    Debug.LogError("Could not find");
                }
                if (SignArea)
                {
                    if (SignArea.GetComponent<mouseOver>().isMouseOver && !signed)
                    {
                        signed = true;
                        signedDocument = true;
                    }
                    else
                    {
                        signed = true;
                        signedDocument = false;
                    }
                }
                else
                {
                    signedDocument = true;
                    Debug.LogWarning("No Sign Area");
                }
                Debug.Log("Signing This document made it: " + signedDocument);
            }
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                Stamp();
                GameObject Note = GameObject.Find("StickyNote");
                if (Note)
                {
                    Note.GetComponent<CanvasGroup>().alpha = 1;
                }
                else
                {
                    Debug.LogError("Could not find");
                }
                if (StampArea && StampAreaTwo)
                {
                    if ((StampArea.GetComponent<mouseOver>().isMouseOver || StampAreaTwo.GetComponent<mouseOver>().isMouseOver) && halfStamped)
                    {
                        stampedDocument = true;
                        halfStamped = false;
                    }
                    else if ((StampArea.GetComponent<mouseOver>().isMouseOver || StampAreaTwo.GetComponent<mouseOver>().isMouseOver) && !stamped)
                    {
                        stamped = true;
                        halfStamped = true;
                    }
                    else
                    {
                        stamped = true;
                        stampedDocument = false;
                    }
                }
                else if (StampArea)
                {
                    if (StampArea.GetComponent<mouseOver>().isMouseOver && !stamped)
                    {
                        stamped = true;
                        stampedDocument = true;
                    }
                    else
                    {
                        stamped = true;
                        stampedDocument = false;
                    }
                }
                else
                {
                    Debug.LogWarning("No Stamp Area");
                    stampedDocument = true;
                }
                Debug.Log("Stamping This document made it:" + stampedDocument);
            }
        }
    }
    
    private void Stamp()
    {
        var mousePos = Input.mousePosition;
        GameObject obj = Instantiate(Stamps, mousePos, SignDocument.transform.localRotation, SignDocument.transform);
        SignDocument.transform.SetAsLastSibling();
        SoundManagerScript.PlaySound("stamp_sound");
    }
    private void Sign()
    {
        var mousePos = Input.mousePosition;
        GameObject obj = Instantiate(Signature, mousePos, SignDocument.transform.localRotation, SignDocument.transform);
        SignDocument.transform.SetAsLastSibling();
        SoundManagerScript.PlaySound("sign_sound");
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = false;
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            originalParent = transform.parent;
            transform.SetParent(transform.parent.parent);
            SignParent = SignDocument.transform.parent;
            SignDocument.transform.parent.SetParent(SignDocument.transform.parent.parent);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        isDragging = true;
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            SignDocument.GetComponent<CanvasGroup>().alpha = 0.5f;
            
            SignDocument.transform.position = Input.mousePosition;
            GetComponent<CanvasGroup>().alpha = 0.5f;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
            transform.position = Input.mousePosition;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            SignDocument.GetComponent<CanvasGroup>().alpha = 1f;
            SignDocument.transform.SetParent(SignParent);
            SignDocument.transform.localPosition = Vector3.zero;
            GetComponent<CanvasGroup>().alpha = 1f;
            transform.SetParent(originalParent);
            transform.localPosition = Vector3.zero;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            SignDocument.transform.SetAsLastSibling();
        }
    }
}
