using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class ShowPuzzle : MonoBehaviour
{
    public GameObject puzzlePanel;

    [YarnCommand("Show")]
    public void Puzzle()
    {
        puzzlePanel.GetComponent<CanvasGroup>().alpha = 1;
        puzzlePanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
        // block background interactions
        GameObject blocker = GameObject.Find("Canvas/Blocker");
        if (blocker)
        {
            blocker.GetComponent<CanvasGroup>().alpha = 1;
            blocker.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }

        // show inventory (only if InventoryController or Player exists)
        GameObject inventoryController = GameObject.Find("InventoryController");
        GameObject player = GameObject.Find("Player");
        // Show inventory using an existing InventoryController GameObject (first-person)
        if (inventoryController)
        {
            OpenInventory openInventory = inventoryController.GetComponent<OpenInventory>();
            if (openInventory && !openInventory.invActive)
            {
                // open the inventory if the InventoryController has OpenInventory
                inventoryController.GetComponent<OpenInventory>().Menu();
            }
        }
        // Show inventory using existing Player GameObject
        else if (player)
        {
            Player playerScript = player.GetComponent<Player>();
            if (playerScript && !playerScript.invActive)
            {
                playerScript.activateMenu();
            }
        }
    }
}
