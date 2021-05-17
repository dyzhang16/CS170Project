using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMenus : MonoBehaviour
{
    public bool invActive = false;
    public GameObject Tooltip;
    public bool paused;
    public GameObject settingsPanel;
    public bool allowInv = true;

    //animator for inventory button
    public Animator inventoryButtonAnimator;

    public bool settings;
    public bool moving = false;
    public bool goingToFade;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            OpenInventory();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            openSettings();
        }
    }

    // made public to be accessed by ShowPuzzle.cs
    public void OpenInventory()
    {
        if (allowInv) 
        {
            // GameObject ip = Inventory.instance.inventoryPanel;
            Tooltip.GetComponent<ToolTipDisplay>().HideInfo();
            invActive = !invActive;
            Inventory.instance.anim.SetBool("Inventory", invActive);
            inventoryButtonAnimator.SetBool("InventoryOpen", invActive);
        }
    }

    public void forceOpenInv(){
        Tooltip.GetComponent<ToolTipDisplay>().HideInfo();
        invActive = true;
        Inventory.instance.anim.SetBool("Inventory", invActive);
        inventoryButtonAnimator.SetBool("InventoryOpen", invActive);
    }

    public void closeInv(){
        invActive = false;
        Inventory.instance.anim.SetBool("Inventory", false);
        inventoryButtonAnimator.SetBool("InventoryOpen", false);
    }

    public void AllowInv(bool allow)
    {
        allowInv = allow;
    }

    public void openSettings()
    {
        settings = !settings;
        settingsPanel.SetActive(settings);
    }
}
