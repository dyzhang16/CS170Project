using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CranePrize : MonoBehaviour
{
	public readonly float BOTTOM_LIMIT = ClawMovement.BOTTOM_LIMIT - 30;
	public PrizeItem prizeItem; // assign this

	public bool isFalling = false;

	void Awake()
	{
		if (prizeItem)
		{
			GetComponent<UnityEngine.UI.Image>().sprite = prizeItem.icon;
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.name == "Hole Separator")
		{
			isFalling = false;
		}
	}

	public IEnumerator FallCoroutine()
	{
		isFalling = true;
		while (transform.localPosition.y > BOTTOM_LIMIT && isFalling)
		{
			Vector3 prizeTransform = transform.localPosition;
			prizeTransform.y -= 1;
			prizeTransform.y = Mathf.Clamp(prizeTransform.y, BOTTOM_LIMIT, ClawMovement.TOP_LIMIT);
			transform.localPosition = prizeTransform;
			transform.Rotate(0, 0, -1);
			yield return null;
		}
		isFalling = false;
	}
}
