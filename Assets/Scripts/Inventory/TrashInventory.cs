﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Yarn.Unity;

public class TrashInventory : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
	// dialogue runner
	public DialogueRunner dialogueRunner;

	// current item ready to trash (static due to DoTrashItem function)
	private static Item itemToTrash;
	private static int indexToTrash;

	// Images for trash open and trash closed
	public Sprite trashOpened = null;
	public Sprite trashClosed = null;

	// affects what Image should be shown
	private bool showOpenTrash = false;

	void Update()
	{
		if (showOpenTrash)
		{
			GetComponent<UnityEngine.UI.Image>().sprite = trashOpened;
		}
		else
		{
			GetComponent<UnityEngine.UI.Image>().sprite = trashClosed;
		}
	}

	// Below two functions handle when the trash sprite should be opened or closed
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (eventData.dragging)
		{
			// Checks if the dragged item contains this Component (to make sure no other draggable objects trigger this)
			ItemDragHandler dragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();
			if (dragHandler)
			{
				showOpenTrash = true;
			}
		}
	}
	public void OnPointerExit(PointerEventData eventData)
	{
		// No special checks here, as when the pointer exits the trash should be closed
		showOpenTrash = false;
	}


	// Below function handles when an Item is dropped onto the Trash Slot
	public void OnDrop(PointerEventData eventData)
	{
		if (dialogueRunner && !dialogueRunner.IsDialogueRunning)
		{
			// line of code used from InventorySlot.OnDrop
			indexToTrash = eventData.pointerDrag.GetComponent<ItemDragHandler>().transform.parent.GetSiblingIndex();
			itemToTrash = Inventory.instance.itemList[indexToTrash];
			// If KeyItem, do not trash. Otherwise, give trash option.
			dialogueRunner.StartDialogue((itemToTrash is KeyItem)?"CannotTrash":"TrashItem");
		}
		else if (dialogueRunner == null)
		{
			Debug.LogError("Dialogue Runner is not attached to the trash functionality!");
		}
		// Set trash to be closed
		showOpenTrash = false;
	}

	// Function that will trash the item
	// This was made static so that YarnInventory.cs can call it.
	public static void DoTrashItem(string[] parameters)
	{
		if (parameters[0] == "Confirm" && itemToTrash && indexToTrash >= 0)
		{
			Inventory.instance.RemoveItem(indexToTrash);
			itemToTrash = null;
			indexToTrash = -1;
		}
	}
}
