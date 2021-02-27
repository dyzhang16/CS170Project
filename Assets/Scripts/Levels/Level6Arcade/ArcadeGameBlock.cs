using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeGameBlock : MonoBehaviour
{
	// This yarncommand will disable interactions with a given machine
	[Yarn.Unity.YarnCommand("BlockMachine")]
	public void BlockMachine(string doBlock)
	{
		Debug.Log("BlockMachine() called!");
		GetComponent<CanvasGroup>().blocksRaycasts = !bool.Parse(doBlock);
	}
}
