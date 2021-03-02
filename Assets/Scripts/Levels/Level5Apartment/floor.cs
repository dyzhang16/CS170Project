using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Yarn.Unity;

public class floor : MonoBehaviour//, IDropHandler
{

    public apartment_puzzle puzzle;

    public GameObject activeObj;
    public GameObject player;

    // IN ORDER FOR THIS TO WORK YOU MUST
    // https://answers.unity.com/questions/1161275/can-i-make-a-non-ui-gameobject-draggable-by-implem.html 
    // 1. add the Physics Raycaster component to your main camera
    // 2. have an EventSystem in your scene
    // 3. change camera to tagged as "MainCamera"
    // public void OnDrop(PointerEventData eventData){
    //     Item droppedItem = Inventory.instance.itemList[eventData.pointerDrag.GetComponent<ItemDragHandler>().transform.parent.GetSiblingIndex()];
    //     GameObject obj = GameObject.Find("Level/Foreground/" + droppedItem.itemName);

    //     RunDialogue dia = obj.GetComponent<RunDialogue>();
    //     dia.runDialogue = false;

    //     RaycastHit hit;
    //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //     if(Physics.Raycast(ray, out hit))
    //     {
    //         obj.SetActive(true);
    //         obj.transform.position = hit.point;

    //         Inventory.instance.RemoveItem(droppedItem);
    //         Inventory.instance.UpdateSlotUI();

    //         puzzle.dic[droppedItem.itemName] = true;
    //     }
    // }

    void Update(){
        if (activeObj != null){
            activeObj.transform.position = player.transform.position;

            if (Input.GetKeyDown(KeyCode.Space)){
                placeItem();
            }
        }
    }

    [YarnCommand("Possess")]
    public void Possess(string[] parameters){

        string Object = parameters[0];
        activeObj = GameObject.Find(Object);

        activeObj.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
        activeObj.GetComponent<RunDialogue>().runDialogue = false;
        activeObj.transform.Find("collider").gameObject.SetActive(false);

        player.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
    }

    public void placeItem(){
        player.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        activeObj.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        activeObj.transform.Find("collider").gameObject.SetActive(true);
        activeObj = null;
    }
}
