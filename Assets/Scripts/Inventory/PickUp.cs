using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Yarn.Unity;

public class PickUp : MonoBehaviour
{
    public Item item;
    public GameObject Player;
    private Player p;

    void Start(){
        p = Player.GetComponent<Player>();
    }

    [YarnCommand("pickUpItem")]
    public void pickUpItem(){
        Inventory.instance.AddItem(item);
        p.activateMenu();

        if (GetComponent<RunDialogue>() != null){
            // Debug.Log("removing dialogue box");
            GetComponent<RunDialogue>().showDialogueCursor = false;
            GetComponent<RunDialogue>().dialogueCursor.SetActive(false);
        }
    }
}