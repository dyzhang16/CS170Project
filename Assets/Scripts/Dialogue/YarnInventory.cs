using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        "Destroy",     // the name of the command
        DestroyObject // the method to run
        );
    }

    private void AddtoInventory(string[] parameters)
    {
        string Object = parameters[0];
        GameObject PotentialObject = GameObject.Find(Object);
        Inventory.instance.AddItem(PotentialObject.GetComponent<ItemAssignment>().item);
    }
    private void DestroyObject(string[] parameters)
    {
        string Object = parameters[0];
        Destroy(GameObject.Find(Object));
    }
}
