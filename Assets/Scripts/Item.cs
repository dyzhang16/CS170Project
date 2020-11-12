using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite icon;

    public virtual void Use()
    {

    }
}