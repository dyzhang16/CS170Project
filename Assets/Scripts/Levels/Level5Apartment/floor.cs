using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Yarn.Unity;

public class floor : MonoBehaviour//, IDropHandler
{

    public apartment_puzzle puzzle;

    public GameObject activeObj;
    public GameObject activeObjPlaceHere;
    public GameObject player;

    public GameObject[] placeHereList = new GameObject[0];

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

        if (activeObj == null){
            string Object = parameters[0];
            activeObj = GameObject.Find(Object);
            Debug.Log(activeObj);

            string temp = Object + "PlaceHere";

            activeObjPlaceHere = GameObject.Find(Object + "PlaceHere"); 
            // Debug.Log(activeObjPlaceHere);

            activeObj.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
            // Debug.Log("color done");
            activeObj.GetComponent<RunDialogue>().runDialogue = false;
            // Debug.Log("run dialo false");
            activeObj.transform.GetChild(0).gameObject.SetActive(false);
            // Debug.Log("collider disabled");

            player.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
            // Debug.Log("player disabled");

            activeObj.GetComponent<RunDialogue>().dialogueCursor.SetActive(false);
            // Debug.Log("dia cursor disabled");

            puzzle.dic[activeObj.name] = false;

            activeObjPlaceHere.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.2f);
            // Debug.Log("sihlouttee working");
        }
            
    }

    public void placeItem(){

        if(puzzle.dic[activeObj.name] == true){
            activeObj.GetComponent<SpriteRenderer>().color = new Color(0.25f, 1f, 0.5f, 1f);
        } else {
            activeObj.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        }

        player.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        activeObj.transform.Find("collider").gameObject.SetActive(true);

        activeObjPlaceHere.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);

        activeObj = null;
        activeObjPlaceHere = null;
    }

    IEnumerator PossesItem(){
        for (float f = 1f; f >= 0f; f -= 0.1f) {
            player.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, f);
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator UnPossesItem(){
        yield return new WaitForSeconds(0.1f);
    }
}
