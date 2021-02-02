using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInventory : MonoBehaviour
{
    public bool invActive = false;
    public GameObject Tooltip;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Menu();
        }
    }

    // made public to be accessed by ShowPuzzle.cs
    public void Menu()
    {
        GameObject ip = Inventory.instance.inventoryPanel;
        Tooltip.GetComponent<ToolTipDisplay>().HideInfo();
        invActive = !invActive;
        ip.SetActive(!ip.activeSelf);
    }
}
