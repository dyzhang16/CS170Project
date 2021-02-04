using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class floor : MonoBehaviour, IDropHandler
{

    public apartment_puzzle puzzle;

    // IN ORDER FOR THIS TO WORK YOU MUST
    // https://answers.unity.com/questions/1161275/can-i-make-a-non-ui-gameobject-draggable-by-implem.html 
    // 1. add the Physics Raycaster component to your main camera
    // 2. have an EventSystem in your scene
    // 3. change camera to tagged as "MainCamera"
    public void OnDrop(PointerEventData eventData){
        Item droppedItem = Inventory.instance.itemList[eventData.pointerDrag.GetComponent<ItemDragHandler>().transform.parent.GetSiblingIndex()];
        GameObject obj = GameObject.Find("Level/Foreground/" + droppedItem.itemName);

        RunDialogue dia = obj.GetComponent<RunDialogue>();
        dia.runDialogue = false;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit))
        {
            obj.SetActive(true);
            obj.transform.position = hit.point;

            Inventory.instance.RemoveItem(droppedItem);
            Inventory.instance.UpdateSlotUI();

            puzzle.dic[droppedItem.itemName] = true;
            puzzle.checkIfComplete();
        }
    }
}
