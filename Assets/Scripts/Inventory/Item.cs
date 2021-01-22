using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite icon;

    public virtual void Display()
    {
        //Debug.Log("Displaying: " + itemName);
    }
    public virtual void HideDisplay()
    {
        //Debug.Log("Hiding Display: " + itemName);
    }
    public virtual void Use()
    {
        Debug.Log("Using: " + itemName);
    }
}