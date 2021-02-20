using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMenus : MonoBehaviour
{
    public bool invActive = false;
    public GameObject Tooltip;
    public GameObject menuButtonsPanel;
    public bool paused;
    public GameObject buttonPanel;
    public GameObject menuPanel;
    public GameObject settingsPanel;
    public bool allowInv = true;

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
            OpenMenu();
        }
    }

    // made public to be accessed by ShowPuzzle.cs
    public void OpenInventory()
    {
        if (allowInv) 
        {
            GameObject ip = Inventory.instance.inventoryPanel;
            Tooltip.GetComponent<ToolTipDisplay>().HideInfo();
            invActive = !invActive;
            Inventory.instance.anim.SetBool("Inventory", invActive);
        }
    }
    public void AllowInv(bool allow)
    {
        allowInv = allow;
    }

    public void OpenMenu()
    {
        if (paused)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }

        allowInv = paused;
        buttonPanel.SetActive(paused);
        paused = !paused;
        menuPanel.SetActive(paused);
    }

    public void openSettings()
    {
        menuButtonsPanel.SetActive(settings);
        settings = !settings;
        settingsPanel.SetActive(settings);
    }
}
