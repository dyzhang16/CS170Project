using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketballMovement : MonoBehaviour
{
	// Unity Objects
	BasketballNet basketballNet;

	// Fields
	// This is the default position (where the basketball is currently situated)
	private Vector3 defaultPosition;
	private Rigidbody2D rb2D; // rigidbody of basketball

	// Boolean to test if the throw loop should be active or not
	/*
	[HideInInspector]
	public bool isThrowing = false;
	*/

	void Start()
	{
		// Initialize Fields
		defaultPosition = gameObject.transform.position;
		rb2D = gameObject.GetComponent<Rigidbody2D>();

		basketballNet = GameObject.Find("BasketballNet").GetComponent<BasketballNet>();

		StartCoroutine(ThrowCR());
	}

	void Update()
	{
		if (gameObject.transform.position.y < -12)
		{
			ResetPosition();
		}
	}

	void OnTriggerEnter2D(Collider2D collider2D)
	{
		// reset the position of the basketball if it collided with the basketball bounds
		if (collider2D.gameObject.name.Contains("BasketballBounds"))
		{
			ResetPosition();
			StartCoroutine(ThrowCR());
		}
		// event if the basketball falls onto the net (thus scoring)
		else if (collider2D.gameObject.name.Contains("BasketballNet") && rb2D.velocity.y <= 0f)
		{
			ResetPosition();
			StartCoroutine(ThrowCR());
		}
	}

	// Coroutine for Throwing the basketball
	IEnumerator ThrowCR()
	{
		// disable simulated movement (temporarily)
		rb2D.simulated = false;

		// reset the velocity
		rb2D.velocity = new Vector2(0f, 0f);

		// have 1 second delay
		yield return new WaitForSeconds(1f);

		// reenable simualted movement
		rb2D.simulated = true;

		// apply a force to the rigidbody of this object
		rb2D.velocity = new Vector2(Random.Range(-12f, 12f), 24f);
	}

	// Resets the basketball to the default position
	private void ResetPosition()
	{
		gameObject.transform.position = defaultPosition;
	}
}
