﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketballNet : MonoBehaviour
{
	// Unity Objects
	public BasketballMovement basketball;

	// Fields
	private Vector3 defaultPosition;
	private float moveSpeed = 0.065f;

	void Start()
	{
		defaultPosition = transform.localPosition;
	}

	void Update()
	{
		// If the basketball is being thrown, move the basketball net with the mouse
		if (basketball.isThrowing || basketball.currentlyThrown)
		{
			float mousePercent = Input.mousePosition.x / Screen.width;

			// uses seemingly random number -6 -> 6. For a better fix, grab the sprite width. However, it seems unnecessary
			float xPos = Mathf.Clamp(-8 + (16 * mousePercent), -7, 7);

			// Move localPosition based on actual mouse x position
			// transform.localPosition = new Vector3(xPos, transform.localPosition.y, transform.localPosition.z);
			
			// Move localPosition using MoveTowards (so there is a max speed it will move)
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(xPos, transform.localPosition.y, transform.localPosition.z), moveSpeed);
		}
		else if (transform.localPosition != defaultPosition && !basketball.currentlyThrown)
		{
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, defaultPosition, moveSpeed);
		}
	}
}
