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
        "AddCoffee",     // the name of the command
        AddCoffeetoInventory // the method to run
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
    }

    private void AddtoInventory(string[] parameters)
    {
        string Object = parameters[0];
        GameObject PotentialObject = GameObject.Find(Object);
        if (PotentialObject.GetComponent<ItemAssignment>().item is Drink)
        {
            Debug.Log("Adding Drink!");
            var so = ScriptableObject.CreateInstance<Drink>();
            so.itemName = "Random Coffee";
            so.icon = PotentialObject.GetComponent<SpriteRenderer>().sprite;
            so.Sugar = 0;
            so.Cream = 0;
            Inventory.instance.AddItem(so);
        }
        else 
        {
            Inventory.instance.AddItem(PotentialObject.GetComponent<ItemAssignment>().item);
        }
    }
    private void AddCoffeetoInventory(string[] parameters)
    {
        string Object = parameters[0];
        GameObject PotentialObject = GameObject.Find(Object);
        var boa = PotentialObject.GetComponent<CoffeeAssignment>().droppedCoffee as Drink;
        if (boa.Cream == 0 && boa.Sugar == 0)
        {
            Inventory.instance.AddItem(PotentialObject.GetComponent<CoffeeAssignment>().Coffee1);
            Debug.Log("Added Black Death");
        }
        else if (boa.Cream == 1 && boa.Sugar == 2)
        {
            Inventory.instance.AddItem(PotentialObject.GetComponent<CoffeeAssignment>().Coffee2);
            Debug.Log("Added Macho-iatto");
        }
        else if (boa.Cream == 3 && boa.Sugar == 4)
        {
            Inventory.instance.AddItem(PotentialObject.GetComponent<CoffeeAssignment>().Coffee3);
            Debug.Log("Added Cold Boooo");
        }
        else if (boa.Cream == 7 && boa.Sugar == 6)
        {
            Inventory.instance.AddItem(PotentialObject.GetComponent<CoffeeAssignment>().Coffee4);
            Debug.Log("Added Moo-cha");
        }
        else if (boa.Cream == 10 && boa.Sugar == 10)
        {
            Inventory.instance.AddItem(PotentialObject.GetComponent<CoffeeAssignment>().Coffee5);
            Debug.Log("Added Milk™");
        }
        else
        {
            Inventory.instance.AddItem(PotentialObject.GetComponent<CoffeeAssignment>().droppedCoffee);
        }
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
