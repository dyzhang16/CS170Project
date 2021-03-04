using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Yarn.Unity;

public class YarnInventory : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
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
    }

    private void AddtoInventory(string[] parameters)
    {
        string Object = parameters[0];
        GameObject PotentialObject = GameObject.Find(Object);
        Inventory.instance.AddItem(PotentialObject.GetComponent<ItemAssignment>().item);
    }
    private void RemoveObject(string[] parameters)
    {
        string Object = parameters[0];
        Item item = Inventory.instance.FindItem(Object);
        Inventory.instance.RemoveItem(item);
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
}
