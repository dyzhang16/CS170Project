using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemClickHandler : MonoBehaviour
{
    public void OnItemClicked()
    {
        if (!GetComponent<ItemDragHandler>().isDragging)
        {
            Inventory.instance.UseItem(Inventory.instance.itemList[transform.GetSiblingIndex()]);
        }
        else
        {
            Debug.Log("DraggingSomething");
        }
    }
}
