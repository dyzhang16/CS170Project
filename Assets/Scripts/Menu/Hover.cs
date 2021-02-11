using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject hands;

    public void OnPointerEnter(PointerEventData eventData){
        hands.SetActive(true);
        Vector2 pos = new Vector2(hands.transform.position.x, transform.position.y);

        hands.transform.position = pos;//Vector2.MoveTowards(hands.transform.position, pos, (25.0f * Time.deltaTime));
    }

    public void OnPointerExit(PointerEventData eventData){
        hands.SetActive(false);
    }
}
