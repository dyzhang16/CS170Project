using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject inventoryPanel;
    public Item[] itemList = new Item[6];
    public InventorySlot[] inventorySlots = new InventorySlot[6];
    public static Inventory instance;
    public Animator anim;

    private void Awake()
    {
        instance = this;
        UpdateSlotUI();
    }

    public bool IsFull()
    {
        // loop through all items in itemList
        for (int i = 0; i < itemList.Length; i++)
        {
            // if there is an open space, then it is not full
            if (itemList[i] == null)
            {
                return false;
            }
        }
        // if this point is reached, then the inventory is full
        return true;
    }

    private bool Add(Item item)
    {
        for (int i = 0; i < itemList.Length; i++)
        {
            if (itemList[i] == null)
            {
                
                itemList[i] = item;
                try
                {
                    SoundManagerScript.PlaySound("pickup_flower_2"); // plays sound when item added to inventory
                }
                catch (System.ArgumentNullException exc)
                {
                    Debug.LogError(exc.Message);
                }
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
    private bool Remove(int index)
    {
        if (itemList[index] != null)
        {
            itemList[index] = null;
            return true;
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
    public void RemoveItem(int index)
    {
        if (Remove(index))
        {
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
    public bool CheckItem(string Name)
    {
        return Array.Exists(itemList, itemList => itemList != null && itemList.itemName == Name);
    }
    public Item FindItem(string Name)
    {
        Item item = Array.Find(itemList, itemList => itemList != null && itemList.itemName == Name);  //https://stackoverflow.com/questions/41348249/null-exception-with-array-findindex-searching-in-a-string-array-in-c-sharp
        return item;
    }

    // new function for finding all items with provided name
    public Item[] FindAllItemsWithName(string name)
    {
        // same implementation as FindItem but uses FindAll
        Item[] items = Array.FindAll<Item>(itemList, itemList => itemList != null && itemList.itemName == name);
        return items;
    }
    public void RemoveItemsOfTypeDocument()
    {
        Item[] items = Array.FindAll<Item>(itemList, element => element as Document);
        Array.ForEach<Item>(items, RemoveItem);
    }
    public bool FindItemOfTypeDrink()
    {
        return Array.Exists(itemList, element => element as Drink);
    }

    public string DisplayItemName(Item item)
    {
        if (item != null)
        {
            string text = null;
            //string text = item.DisplayName();
            return text;
        }
        else
        {
            Debug.Log("Text Could not be found");
            return null;
        }
    }
    public string DisplayItemDescription(Item item)
    {
        if (item != null)
        {
            string text = null;
            //string text = item.DisplayDescription();
            return text;
        }
        else
        {
            Debug.Log("Text Could not be found");
            return null;
        }
    }
}
