using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class mouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector] public bool isMouseOver;

    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseOver = true;
        Debug.Log("Mouse is Over" + gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseOver = false;
        //Debug.Log("Mouse is Exit");
    }  
}
