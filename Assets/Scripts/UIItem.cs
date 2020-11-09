using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIItem : MonoBehaviour , IPointerClickHandler
{
    public Item item;
    private UIItem selectedItem;
    
    private void Awake()
    {
        selectedItem = GameObject.Find("SelectedItem").GetComponent<UIItem>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (this.item != null)
        {
            if (selectedItem.item != null)
            {
                Item clone = selectedItem.item;
            }
        }
    }
}
