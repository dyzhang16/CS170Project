using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class DocumentPuzzle : MonoBehaviour, IPointerClickHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
{

    public GameObject Stamps, Signature, SignArea, StampArea, StampAreaTwo; 
    public bool isDragging = false;
    [HideInInspector] public bool stampedSpace, stampedSpaceTwo, signedSpace = false;
    private Transform originalParent;
    private bool stamp, sign, stampTwo , halfStamp= false;

    public void Start()
    {
        Debug.Log("Created a new Document");
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isDragging)
        {
            GameObject PenObject = GameObject.Find("Pen");   
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (PenObject)
                {
                    Sign();
                    GameObject Arrow = GameObject.Find("Arrow");
                    if (Arrow)
                    {
                        Arrow.GetComponent<CanvasGroup>().alpha = 1;
                    }
                    else
                    {
                        Debug.LogError("Could not find");
                    }
                    if (SignArea)
                    {
                        if (SignArea.GetComponent<mouseOver>().isMouseOver && !sign)
                        {
                            signedSpace = true;
                            sign = true;
                        }
                        else
                        {
                            signedSpace = false;
                            sign = true;
                        }
                    }
                    else
                    {
                        Debug.LogWarning("No SignArea on ths Document");
                    }
                }
            }
            GameObject StampObject = GameObject.Find("Stamp");
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                if (StampObject)
                {
                    Stamp();
                    GameObject Arrow = GameObject.Find("Arrow");
                    if (Arrow)
                    {
                        Arrow.GetComponent<CanvasGroup>().alpha = 1;
                    }
                    else
                    {
                        Debug.LogError("Could not find");
                    }
                    if (StampArea && StampAreaTwo)
                    {
                        if ((StampArea.GetComponent<mouseOver>().isMouseOver || StampAreaTwo.GetComponent<mouseOver>().isMouseOver) && halfStamp)
                        {
                            stampedSpace = true;
                        }
                        else if (StampArea.GetComponent<mouseOver>().isMouseOver && !stamp)
                        {
                            halfStamp = true;
                            stamp = true;
                        }
                        else if (StampAreaTwo.GetComponent<mouseOver>().isMouseOver && !stampTwo)
                        {
                            halfStamp = true;
                            stampTwo = true;
                        }
                        else
                        {
                            stampedSpace = false;
                            stamp = true;
                            stampTwo = true;
                        }
                    }
                    else if (StampArea)
                    {
                        if (StampArea.GetComponent<mouseOver>().isMouseOver && !stamp)
                        {
                            stampedSpace = true;
                            stamp = true;
                        }
                        else
                        {
                            stampedSpace = false;
                            stamp = true;
                        }
                    }
                    else
                    {
                        Debug.LogWarning("No StampArea on ths Document");
                    }

                }
            }
        }
    }
    private void Stamp()
    {
        var mousePos = Input.mousePosition;
        Instantiate(Stamps, mousePos, transform.localRotation, transform);
        SoundManagerScript.PlaySound("stamp_sound");
    }
    private void Sign()
    {
        var mousePos = Input.mousePosition;
        Instantiate(Signature, mousePos, transform.localRotation, transform);
        SoundManagerScript.PlaySound("sign_sound");
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
