using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class old_BasketballNet : MonoBehaviour
{
	// Attached Unity Objects

	// Fields
	public bool trackMouse = false;
	public static readonly float LEFT_LIMIT = -440f;
	public static readonly float RIGHT_LIMIT = LEFT_LIMIT * -1f;

	void Update()
	{
		// update the actual position using the mouse position
		transform.position = new Vector3(Input.mousePosition.x, transform.position.y, transform.position.z);
		// calculate an updated x position using the transform's local position, and update localPosition
		float xPos = Mathf.Clamp(transform.localPosition.x, LEFT_LIMIT, RIGHT_LIMIT);
		transform.localPosition = new Vector3(trackMouse?xPos:0f, transform.localPosition.y, transform.localPosition.z);
	}
}
