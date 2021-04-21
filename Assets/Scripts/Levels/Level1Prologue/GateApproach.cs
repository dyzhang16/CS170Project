using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class GateApproach : MonoBehaviour
{
	public DialogueRunner dialogueRunner;
	public VariableStorageBehaviour customVariableStorage;

	public GameObject Gate;
	public GameObject Player;
	public GameObject Destination;

	private bool playerMoving = false;
	
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

	void Update(){
		if (playerMoving){
			Player.transform.position = Vector3.MoveTowards(Player.transform.position, Destination.transform.position, 15*Time.deltaTime);

			if (Vector3.Distance(Player.transform.position, Destination.transform.position) < 0.001f)
			{
				playerMoving = false;
				Gate.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
			}
		}
	}

	[YarnCommand("MovePlayer")]
    public void MovePlayer(){
		Gate.GetComponent<BoxCollider>().isTrigger = true;
		Gate.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.25f);
        playerMoving = true;
		//play sound wooshing
    }
}
