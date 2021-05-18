using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.UI;

public class DragTutorial : MonoBehaviour
{
    public GameObject TutorialPanel;
    public bool playingTutorial;
    private bool continueCycle;
    public GameObject cursor;

    public GameObject whiteFlower;
    public GameObject redFlower;
    public GameObject yellowFlower;
    public GameObject purpleFlower;

    public GameObject flower;

    public GameObject bouqetUI;
    private int cycle;
    private Vector3 originalPos;

    public DialogueRunner dialogueRunner;
    public bool visit = false;

    public GameObject wrap;
    
    void Start(){
        originalPos = Inventory.instance.inventorySlots[0].gameObject.transform.position + new Vector3(-13, 0, 0);
        whiteFlower.transform.position = Inventory.instance.inventorySlots[0].gameObject.transform.position + new Vector3(-13, 0, 0);
        redFlower.transform.position = Inventory.instance.inventorySlots[0].gameObject.transform.position + new Vector3(-13, 0, 0);
        yellowFlower.transform.position = Inventory.instance.inventorySlots[0].gameObject.transform.position + new Vector3(-13, 0, 0);
        purpleFlower.transform.position = Inventory.instance.inventorySlots[0].gameObject.transform.position + new Vector3(-13, 0, 0);
    }

    //this is for the tutorial dialogue to teach the player how to drag flowers onto the inventory
    [YarnCommand("Drag")]
    public void DragDialogue(){
        if (Inventory.instance.CheckItem("White Flower") || Inventory.instance.CheckItem("Red Flower") || 
        Inventory.instance.CheckItem("Yellow Flower") || Inventory.instance.CheckItem("Purple Flower")) {
            if (!visit) {
                dialogueRunner.StartDialogue("dragTutorial");
                playingTutorial = true;
                continueCycle = true;
                TutorialPanel.SetActive(true);

                //determine which flower is in the first slot and then use that flower
                if (Inventory.instance.itemList[0].itemName == "White Flower"){
                    whiteFlower.SetActive(true);
                    flower = whiteFlower;
                } else if (Inventory.instance.itemList[0].itemName == "Red Flower"){
                    redFlower.SetActive(true);
                    flower = redFlower;
                } else if (Inventory.instance.itemList[0].itemName == "Yellow Flower"){
                    yellowFlower.SetActive(true);
                    flower = yellowFlower;
                } else if (Inventory.instance.itemList[0].itemName == "Purple Flower"){
                    purpleFlower.SetActive(true);
                    flower = purpleFlower;
                }

                visit = true;
            }
        }
    }

    [YarnCommand("Activate")]
    public void Activate(){
        wrap.SetActive(true);
    }

    [YarnCommand("TurnOffTutorial")]
    public void TurnOffTutorial(){
        TutorialPanel.SetActive(false);
        playingTutorial = false;
    }

    void Update(){
        if (playingTutorial && continueCycle){

            float step = 300 * Time.deltaTime;

            switch(cycle){
                //move to flower
                case 0:
                    cursor.transform.position = Vector3.MoveTowards(cursor.transform.position, flower.transform.position, step);

                    if (Vector3.Distance(cursor.transform.position, flower.transform.position) < 0.001f)
                    {
                        StartCoroutine(updateCycle());
                    }
                    break;
                case 1:
                    cursor.transform.position = Vector3.MoveTowards(cursor.transform.position, bouqetUI.transform.position, step);
                    flower.transform.position = Vector3.MoveTowards(flower.transform.position, bouqetUI.transform.position, step);

                    if (Vector3.Distance(cursor.transform.position, bouqetUI.transform.position) < 0.001f){
                        StartCoroutine(updateCycle());
                    }
                    break;
            }
        }
    }

    IEnumerator updateCycle(){
        continueCycle = false;
        cycle++;

        cycle = cycle%2;

        yield return new WaitForSeconds(0.75f);

        if (cycle == 0){
            flower.transform.localScale = new Vector3(1, 1, 1);
            flower.GetComponent<CanvasGroup>().alpha = 1f;

            flower.transform.position = originalPos;

        } else if (cycle == 1){

            flower.transform.localScale = new Vector3(5, 5, 5);
            flower.GetComponent<CanvasGroup>().alpha = 0.5f;
        }

        continueCycle = true;
    }
}
