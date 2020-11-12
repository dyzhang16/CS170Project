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
    }
    private bool Add(Item item)
    {
        for (int i = 0; i < itemList.Length; i++)
        {
            if (itemList[i] == null)
            {
                itemList[i] = item;
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
}
