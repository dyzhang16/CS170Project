using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Yarn.Unity;

public class YarnInventory : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    private GameObject player;
    private Player p;

    // Start is called before the first frame update
    void Awake()
    {
        dialogueRunner.AddCommandHandler(
        "Add",     // the name of the command
        AddtoInventory // the method to run
        );
        dialogueRunner.AddCommandHandler(
        "Remove",     // the name of the command
        RemoveObject // the method to run
        );
        dialogueRunner.AddCommandHandler(
        "HideObject",     // the name of the command
        HideObject // the method to run
        );
        dialogueRunner.AddCommandHandler(
        "Destroy",     // the name of the command
        DestroyObject // the method to run
        );
        dialogueRunner.AddCommandHandler(
        "Trash",
        TrashInventory.DoTrashItem
        );

        // adding function to test inventory size
        dialogueRunner.AddFunction(
            "IsInventoryFull",
            0,
            (Yarn.Value[] parameters) =>
            {
                return Inventory.instance.IsFull();
            }
        );

        // function for checking if player has item in Inventory
        //  Usage in Yarn: HasItem("<name of item>")
        //  Returns a boolean
        dialogueRunner.AddFunction(
            "HasItem",
            1,
            (Yarn.Value[] parameters) =>
            {
                return HasItem(parameters[0].AsString);
            }
        );

        player = GameObject.Find("Player");
        if (player != null){
            p = player.GetComponent<Player>();
        }
    }

    private void AddtoInventory(string[] parameters)
    {
        string Object = parameters[0];
        GameObject PotentialObject = GameObject.Find(Object);
        Inventory.instance.AddItem(PotentialObject.GetComponent<ItemAssignment>().item);
        if (PotentialObject.GetComponent<RunDialogue>() != null){
            PotentialObject.GetComponent<RunDialogue>().showDialogueCursor = false;
            if (PotentialObject.GetComponent<RunDialogue>().dialogueCursor)
            {
                PotentialObject.GetComponent<RunDialogue>().dialogueCursor.SetActive(false);
            }
            else 
            {
                Debug.LogWarning("There is no dialogueCursor on this object.");
            }
            
        }

        if (parameters.Length == 2){
            if (parameters[1] != "False"){
                StartCoroutine(closeInventory());
            }
        } else {
            if (p != null){
                p.activateMenu();
            }
        }
    }
    private void RemoveObject(string[] parameters)
    {
        string Object = parameters[0];
        Item item = Inventory.instance.FindItem(Object);
        Inventory.instance.RemoveItem(item);
    }
    [YarnCommand("RemoveDocuments")]
    public void RemoveObjectofType()
    {
        Debug.LogWarning("Hit Here");
        Inventory.instance.RemoveItemsOfTypeDocument();
    }

    private void HideObject(string[] parameters)
    {
        string Object = parameters[0];
        GameObject PotentialObject = GameObject.Find(Object);
        Debug.Log("HidingObject" + PotentialObject);
        PotentialObject.SetActive(false);
    }
    private void DestroyObject(string[] parameters)
    {
        string Object = parameters[0];
        Destroy(GameObject.Find(Object));
    }

    // checks if an item exists in inventory
    private bool HasItem(string query)
    {
        Item foundItem = Inventory.instance.FindItem(query);
        return foundItem != null;
    }

    IEnumerator closeInventory(){
        if (p != null){
            p.activateMenu();
        }

        yield return new WaitForSeconds(0.75f);
    }
}
