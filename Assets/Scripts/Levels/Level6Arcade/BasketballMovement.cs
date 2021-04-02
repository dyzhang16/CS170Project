using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketballMovement : MonoBehaviour
{
	// Unity Objects
	public BasketballGameplay basketballGameplay;

	// Fields
	// This is the default position (where the basketball is currently situated)
	private Vector3 defaultPosition;
	private Rigidbody2D rb2D; // rigidbody of basketball

	// Boolean to test if the throw loop should be active or not
	[HideInInspector]
	public bool isThrowing = false;
	public bool currentlyThrown = false; // bool that is modified during the throwing coroutine

	void Start()
	{
		// Initialize Fields
		defaultPosition = gameObject.transform.position;
		rb2D = gameObject.GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		if (gameObject.transform.position.y < -12)
		{
			ResetPosition();
		}
	}

	// Call the throwing coroutine while setting the isThrowing variable to true
	public void StartThrowing()
	{
		ResetPosition();
		isThrowing = true;
		StartCoroutine(ThrowCR());
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
			basketballGameplay.IncrementScore();
			ResetPosition();
			StartCoroutine(ThrowCR());
		}
	}

	// Coroutine for Throwing the basketball
	IEnumerator ThrowCR()
	{
		// disable simulated movement (temporarily)
		rb2D.simulated = false;

		// set the velocity to zero
		rb2D.velocity = new Vector2(0f, 0f);

		// Stop the CR if no longer throwing
		if (!isThrowing)
		{
			currentlyThrown = false;
			yield break;
		}

		// have random short delay
		yield return new WaitForSeconds(Random.Range(0.4f, 0.9f));

		// reenable simualted movement
		rb2D.simulated = true;

		// apply a force to the rigidbody of this object
		rb2D.velocity = new Vector2(Random.Range(-12f, 12f), 24f);

		// Note that the basketball is being currently thrown
		currentlyThrown = true;
	}

	// Resets the basketball to the default position
	private void ResetPosition()
	{
		gameObject.transform.position = defaultPosition;
	}
}
