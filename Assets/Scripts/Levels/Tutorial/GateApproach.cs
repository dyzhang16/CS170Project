using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class GateApproach : MonoBehaviour
{
	public DialogueRunner dialogueRunner;
	public VariableStorageBehaviour customVariableStorage;
	
	private void OnTriggerEnter(Collider other)
	{
		// do not run the rest of the function if dialogueRunner or customVariableStorage is null
		if (dialogueRunner == null || customVariableStorage == null)
		{
			return;
		}
		// if collision is with player...
		if (other.gameObject.CompareTag("Player"))
		{
			// access the puzzle yarn value
			Yarn.Value puzzle = customVariableStorage.GetValue("$puzzle");
			if (puzzle != null && puzzle.AsNumber == 1)
			{
				// run gate_approach node if puzzle is 1
				if (!dialogueRunner.IsDialogueRunning)
				{
					dialogueRunner.StartDialogue("gate_approach");
				}
			}
		}
	}
}
