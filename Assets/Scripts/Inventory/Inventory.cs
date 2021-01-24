using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject inventoryPanel;
    public Item[] itemList = new Item[6];
    public InventorySlot[] inventorySlots = new InventorySlot[6];
    public static Inventory instance;

    private void Start()
    {
        instance = this;
        UpdateSlotUI();
        Inventory.instance.inventoryPanel.SetActive(false);
    }

    private bool Add(Item item)
    {
        for (int i = 0; i < itemList.Length; i++)
        {
            if (itemList[i] == null)
            {
                
                itemList[i] = item;
                SoundManagerScript.PlaySound("pickup_flower_2"); // plays sound when item added to inventory
                return true;
            }
        }
        return false;
    }

    private bool Remove(Item item)
    {
        for (int i = 0; i < itemList.Length; i++)
        {
            if (itemList[i] == item)
            {
                itemList[i] = null;
                return true;
            }
        }
        return false;
    }

    public void UpdateSlotUI()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i].UpdateSlot();
        }
    }

    public void AddItem(Item item)
    {
        bool hasAdded = Add(item);
        if (hasAdded)
        {
            UpdateSlotUI();
        }
    }

    public void RemoveItem(Item item) 
    {
        if (Remove(item)) {
            UpdateSlotUI();
        }
    }
    public void UseItem(Item item)
    {
        if (item != null)
        {
            item.Use();
        }
    }
}
