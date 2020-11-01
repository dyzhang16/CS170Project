using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "new Consumable", menuName = "Items/Consumable")]
public class Consumable : Item
{
    public override void Use()
    {
        GameObject player = Inventory.instance.player;
        Inventory.instance.Remove(this);
    }
}
