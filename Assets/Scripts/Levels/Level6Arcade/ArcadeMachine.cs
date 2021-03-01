using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a script used for all the ArcadeMachines
/// </summary>
public class ArcadeMachine : MonoBehaviour
{
	public ArcadeTicketItem TicketItem;

	// Function to add COUNT tickets.
	public void AddTickets(int count)
	{
		if (count <= 0) // do nothing if count is <= 0
			return;
		foreach(Item item in Inventory.instance.itemList)
		{
			if (item is ArcadeTicketItem)
			{
				((ArcadeTicketItem)item).TicketCount += count;
				return;
			}
		}
		// If this point was reached, then this item is not in the inventory, so add it
		ArcadeTicketItem copy = TicketItem;
		copy.TicketCount = count;
		Inventory.instance.AddItem(copy);
	}

	// Function to add 1 ticket
	public void AddTicket()
	{
		AddTickets(1);
	}
}
