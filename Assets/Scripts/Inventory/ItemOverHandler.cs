using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemOverHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private ToolTipDisplay tooltip;
    [HideInInspector] public Item item;
    public void OnPointerEnter(PointerEventData eventData)
    {
        item = Inventory.instance.itemList[transform.parent.GetSiblingIndex()];
        tooltip.DisplayInfo(item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.HideInfo();
    }

}
