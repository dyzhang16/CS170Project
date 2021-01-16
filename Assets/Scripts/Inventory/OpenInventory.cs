using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInventory : MonoBehaviour
{
    public bool invActive = false;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Menu();
        }
    }

    void Menu()
    {
        GameObject ip = Inventory.instance.inventoryPanel;
        invActive = !invActive;
        ip.SetActive(!ip.activeSelf);
    }
}
