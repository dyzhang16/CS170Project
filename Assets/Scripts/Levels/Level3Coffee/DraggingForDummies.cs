using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Yarn.Unity;

public class DraggingForDummies : MonoBehaviour
{
    public DialogueRunner dia;
    // IN ORDER FOR THIS TO WORK YOU MUST
    // https://answers.unity.com/questions/1161275/can-i-make-a-non-ui-gameobject-draggable-by-implem.html 
    // 1. add the Physics Raycaster component to your main camera
    // 2. have an EventSystem in your scene
    // 3. change camera to tagged as "MainCamera"
    public void OnDrop(PointerEventData eventData)
    {
        Item droppedItem = Inventory.instance.itemList[eventData.pointerDrag.GetComponent<ItemDragHandler>().transform.parent.GetSiblingIndex()];
        Debug.Log(droppedItem);
        RaycastHit hit;
        Ray ray = eventData.pressEventCamera.ScreenPointToRay(eventData.position);
        Debug.Log(ray);
        if (Physics.Raycast(ray, out hit))
        {
            
            Inventory.instance.RemoveItem(droppedItem);
            //dia.StartDialogue("DraggingForDummies");
            //         obj.SetActive(true);
            //         obj.transform.position = hit.point;

            //         Inventory.instance.RemoveItem(droppedItem);
            //         Inventory.instance.UpdateSlotUI();

            //         puzzle.dic[droppedItem.itemName] = true;
        }
    }
}
