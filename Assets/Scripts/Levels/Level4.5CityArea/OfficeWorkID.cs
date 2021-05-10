using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is specifically used just for the Office Work ID.
/// </summary>
public class OfficeWorkID : MonoBehaviour
{
	void Start()
	{
		// If the GameManager notes that this object has been picked up OR if it is not needed, then destroy it
		if (GameManager.instance.idPickedUp == 1 || GameManager.instance.idNeeded == 0)
		{
			Destroy(gameObject);
		}
	}
}
