using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// This script is involved with handling the movement of the claw GameObject.
/// </summary>
/// This script handles most of the claw movement using events and Coroutines.
public class ClawMovement : MonoBehaviour
{
	// Unity-related fields
	public GameObject claw; // UI image
	public GameObject joystick; // GameObject with UI slider
	public EventSystem eventSystem; // EventSystem
	public GameObject currentPrize; // GameObject with PrizeMovement component
	private Slider joystickSlider; // UI Slider from joystick GameObject

	// speed in which claw moves
	public readonly float clawSpeed = 1;
	public readonly float dropSpeed = 1;
	public readonly float riseSpeed = 0.6f;

	// positional limits (local position) of claw machine
	public static readonly float RIGHT_LIMIT = 420;
	public static readonly float LEFT_LIMIT = -420;
	public static readonly float BOTTOM_LIMIT = -90;
	public static readonly float TOP_LIMIT = 190;

	// to check whether to the claw is currently dropping/rising
	private bool isDropping;
	private bool isMoving;

	/// <summary>
	/// Initialize some variables
	/// </summary>
	void Awake()
	{
		joystickSlider = joystick.GetComponent<Slider>();
		isDropping = false;
	}

	/// <summary>
	/// This just updates the position of the currentPrize (if it is set in GrabPrize())
	/// </summary>
	void Update()
	{
		if (currentPrize)
		{
			currentPrize.transform.localPosition = claw.transform.localPosition;
		}
	}

	/// <summary>
	/// This handles any collisions with GameObjects with the CranePrize component
	/// </summary>
	void OnTriggerEnter2D(Collider2D col)
	{
		Debug.Log(col.gameObject.name);
		if (col.gameObject.name.Contains("Hole Separator"))
		{
			StopAllCoroutines();
			StartCoroutine(RiseCoroutine());
			return;
		}
		CranePrize prize = col.gameObject.GetComponent<CranePrize>();
		if (prize && !prize.isFalling)
		{
			// Set the currentPrize to be this GameObject
			GrabPrize(col.gameObject);
		}
	}

	/// <summary>
	/// This handles setting the currentPrize.
	/// </summary>
	/// <param name="prize">GameObject to set currentPrize as.</param>
	void GrabPrize(GameObject prize)
	{
		if (currentPrize == null)
		{
			// set currentPrize as long as there is no other currentPrize
			currentPrize = prize;
		}
	}

	/// <summary>
	/// This will start a Coroutine, FallCoroutine(), on the currentPrize's CranePrize
	/// component, and also set currentPrize to null.
	/// </summary>
	void DropPrize()
	{
		if (currentPrize != null)
		{
			StartCoroutine(currentPrize.GetComponent<CranePrize>().FallCoroutine());
			currentPrize = null;
		}
	}

	/// <summary>
	/// This Coroutine will drop the claw's y position until it reaches
	/// the BOTTOM_LIMIT, where it will start
	/// another Coroutine, RiseCoroutine().
	/// </summary>
	IEnumerator DropCoroutine()
	{
		// dropping
		isDropping = true;
		while (claw.transform.localPosition.y > BOTTOM_LIMIT)
		{
			Vector3 clawTransform = claw.transform.localPosition;
			clawTransform.y -= dropSpeed;
			clawTransform.y = Mathf.Clamp(clawTransform.y, BOTTOM_LIMIT, TOP_LIMIT);
			claw.transform.localPosition = clawTransform;
			yield return null;
		}
		yield return StartCoroutine(RiseCoroutine());
	}

	/// <summary>
	/// This Coroutine will first wait, then increase the claw's y position until it reaches
	/// the TOP_LIMIT, where it will then change the boolean to isDropping
	/// to false and allowing the joystickSlider to be interactable.
	/// </summary>
	IEnumerator RiseCoroutine()
	{
		yield return new WaitForSeconds(2);
		// rising
		while (claw.transform.localPosition.y < TOP_LIMIT)
		{
			Vector3 clawTransform = claw.transform.localPosition;
			clawTransform.y += riseSpeed;
			clawTransform.y = Mathf.Clamp(clawTransform.y, BOTTOM_LIMIT, TOP_LIMIT);
			claw.transform.localPosition = clawTransform;
			yield return null;
		}
		// no longer rising
		yield return StartCoroutine(ReturnToStartCoroutine());
	}

	/// <summary>
	/// This Coroutine will return the claw to the starting position (which
	/// is at the upper right, where the prize hole is). This will also call
	/// DropPrize().
	/// </summary>
	IEnumerator ReturnToStartCoroutine()
	{
		while (claw.transform.localPosition.x < RIGHT_LIMIT)
		{
			Vector3 clawTransform = claw.transform.localPosition;
			clawTransform.x += clawSpeed;
			clawTransform.y = Mathf.Clamp(clawTransform.y, LEFT_LIMIT, RIGHT_LIMIT);
			claw.transform.localPosition = clawTransform;
			yield return null;
		}
		// Now the claw should be at the starting position
		isDropping = false;
		joystickSlider.interactable = true; // joystick slider is set to interactable
		DropPrize();
	}

	/// <summary>
	/// This Coroutine will change the claw's x position using the joystick slider's
	/// value. This Coroutine will stop once StopMove() is called.
	/// </summary>
	IEnumerator MoveCoroutine()
	{
		// moving
		isMoving = true;
		while (isMoving)
		{
			// copy the transform of the claw
			Vector3 clawPos = claw.transform.localPosition;
			// change the copied transform's x value (clamped to a specific limit)
			clawPos.x += clawSpeed * joystickSlider.value;
			clawPos.x = Mathf.Clamp(clawPos.x, LEFT_LIMIT, RIGHT_LIMIT);
			// update the transform of the claw using the modified copy
			claw.transform.localPosition = clawPos;
			yield return null;
		}
	}

	/// <summary>
	/// Drops the claw (by calling the DropCoroutine). This function
	/// should be called when the Drop button is clicked. The joystickSlider
	/// is no longer interactable until the MoveCoroutine() is finished.
	/// </summary>
	public void DoDrop()
	{
		joystickSlider.interactable = false;
		if (!isDropping)
		{
			StartCoroutine(DropCoroutine());
		}
	}

	/// <summary>
	/// Moves the claw (by calling the MoveCoroutine).
	/// </summary>
	/// This should be called in the JoystickControl GameObject, in the
	/// "PointerDown" event trigger. The MoveCoroutine's stop condition
	/// is when StopMove() is called, which should be called in the
	/// JoystickControl GameObject, in the "PointerUp" event trigger.
	public void DoMove()
	{
		StartCoroutine(MoveCoroutine());
	}

	/// <summary>
	/// Stops moving the claw (by setting isMoving to false, thus allowing
	/// the MoveCoroutine to complete).
	/// </summary>
	/// This should be called in the JoystickControl GameObject, in the
	/// "PointerUp" event trigger.
	public void StopMove()
	{
		isMoving = false;
	}
}
