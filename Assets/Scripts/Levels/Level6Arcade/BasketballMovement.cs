using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasketballMovement : MonoBehaviour
{
	// Attached Unity Objects
	[SerializeField]
	private BasketballGameplay gameplayComponent;

	// Fields
	public bool isMoving = false; // Used in ThrowRoutine to detect when it is done being called
	private bool hasMissed = false; // I should rename this, as this is used when missing and scoring
	private bool isFalling = false;

	void Start()
	{
		if (gameplayComponent == null)
		{
			gameplayComponent = GameObject.Find("BasketballPanel").GetComponent<BasketballGameplay>();
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.name.Contains("BasketballMissZone"))
		{
			hasMissed = true;
		}
		if (isFalling && col.gameObject.name.Contains("BasketballNet"))
		{
			hasMissed = true;
			gameplayComponent.IncrementScore();
		}
	}

	/// <summary>
	/// The routine that gets run to simulate throwing.
	/// </summary>
	public IEnumerator ThrowRoutine()
	{
		isMoving = true; // THIS MUST BE FIRST

		// save the original position
		Vector3 originalPosition = transform.localPosition;

		// reset the booleans
		isFalling = false;
		hasMissed = false;

		// math calculations
		float rad = 0;
		float delta = Mathf.Sin(rad) * 300;
		float maxY = originalPosition.y;
		float direction = Random.Range(-2.5f, 2.5f);
		while (!hasMissed)
		{
			transform.localPosition = new Vector3(
				transform.localPosition.x + direction,
				originalPosition.y + (delta),
				transform.localPosition.z);
			rad += 0.014f;
			delta = Mathf.Sin(rad) * 375;
			if (transform.localPosition.y >= maxY)
			{
				maxY = transform.localPosition.y;
			}
			else if (!isFalling)
			{
				// Debug.Log(prefix + "Falling");
				isFalling = true;
			}
			yield return null;
		}

		transform.localPosition = originalPosition;

		isMoving = false; // THIS MUST BE LAST
	}
}
