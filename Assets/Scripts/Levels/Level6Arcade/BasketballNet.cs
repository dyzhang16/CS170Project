using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketballNet : MonoBehaviour
{
	// Unity Objects
	public BasketballMovement basketball;
	private Collider2D leftCollider;
	private Collider2D rightCollider;

	// Fields
	private Vector3 defaultPosition;
	private float moveSpeed = 12f;

	void Start()
	{
		defaultPosition = transform.localPosition;

		leftCollider = transform.Find("NetLeftWall").gameObject.GetComponent<Collider2D>();
		rightCollider = transform.Find("NetRightWall").gameObject.GetComponent<Collider2D>();
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
			
			if (xPos > transform.localPosition.x && GetComponent<SpriteRenderer>().flipX)
			{
				GetComponent<SpriteRenderer>().flipX = false;
			}
			else if (xPos < transform.localPosition.x)
			{
				GetComponent<SpriteRenderer>().flipX = true;
			}

			// Move localPosition using MoveTowards (so there is a max speed it will move)
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(xPos, transform.localPosition.y, transform.localPosition.z), moveSpeed * Time.deltaTime);
		}
		else if (transform.localPosition != defaultPosition && !basketball.currentlyThrown)
		{
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, defaultPosition, moveSpeed * Time.deltaTime);
		}

		// modify the net's side colliders to be triggers when the ball is rising and colliders when the ball is falling
		if (basketball.rb2D.velocity.y >= 0)
		{
			// set to trigger
			leftCollider.isTrigger = true;
			rightCollider.isTrigger = true;
		}
		else
		{
			// set to collider
			leftCollider.isTrigger = false;
			rightCollider.isTrigger = false;
		}
	}
}
