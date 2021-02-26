using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class ShowPuzzle : MonoBehaviour
{
    public GameObject puzzlePanel;
    public bool DoNotOpenInventory = false;

    [Tooltip("Stop the player's movement when a puzzle is shown")]
    public bool stopPlayerMovement = false; // used in Update()

    [YarnCommand("Show")]
    public void Puzzle()
    {
        Debug.Log("Showing puzzle");
        puzzlePanel.GetComponent<CanvasGroup>().alpha = 1;
        puzzlePanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
        // block background interactions
        GameObject blocker = GameObject.Find("Canvas/Blocker");
        if (blocker)
        {
            blocker.GetComponent<CanvasGroup>().alpha = 1;
            blocker.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }

        // if do not show inventory is on, do nothing else here, otherwise open the inventory
        if (!DoNotOpenInventory)
        {
            // show inventory (only if InventoryController or Player exists)
            GameObject inventoryController = GameObject.Find("InventoryController");
            GameObject player = GameObject.Find("Player");
            // Show inventory using an existing InventoryController GameObject (first-person)
            if (inventoryController)
            {
                OpenMenus openInventory = inventoryController.GetComponent<OpenMenus>();
                if (openInventory && !openInventory.invActive)
                {
                    // open the inventory if the InventoryController has OpenInventory
                    inventoryController.GetComponent<OpenMenus>().OpenInventory();
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

    // make private reference to the player gameobject for use in Update()
    private GameObject playerGameObject; // this is set in Awake()
    void Awake()
    {
        playerGameObject = GameObject.Find("Player");
    }
    void Update()
    {
        // if we want to stop the player movement, check if a puzzle panel is opened...
        if (stopPlayerMovement &&
            puzzlePanel.GetComponent<CanvasGroup>() &&
            puzzlePanel.GetComponent<CanvasGroup>().alpha == 1)
        {
            // ...then get a player object and then set AllowMove to false
            if (stopPlayerMovement && playerGameObject)
            {
                Player player = playerGameObject.GetComponent<Player>();
                if (player && player.allowMovement)
                {
                    player.AllowMove(false);
                }
            }
        }
    }
}
