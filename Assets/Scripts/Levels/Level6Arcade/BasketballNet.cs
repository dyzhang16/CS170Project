using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketballNet : MonoBehaviour
{
	void Update()
	{
		float mousePercent = Input.mousePosition.x / Screen.width;

		// uses seemingly random number -6 -> 6. For a better fix, grab the sprite width. However, it seems unnecessary
		float xPos = Mathf.Clamp(-8 + (16 * mousePercent), -6, 6);


		transform.localPosition = new Vector3(xPos, transform.localPosition.y, transform.localPosition.z);

		// Debug.Log(xPos);
	}
}
