using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketballMovement : MonoBehaviour
{
	// This is the default position (where the basketball is currently situated)
	private Vector3 defaultPosition;

	void Start()
	{
		defaultPosition = gameObject.transform.position;
	}

	// Coroutine for Throw Loop
	IEnumerator ThrowCR()
	{
		yield return null;
	}
}
