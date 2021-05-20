using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class ApproachDialogue : MonoBehaviour
{
	// Unity Objects
	private DialogueRunner dialogueRunner; // found during gameplay
	[SerializeField]
	[Tooltip("This is the GameObject that can trigger the dialogue if it approaches (default: null).")]
	private GameObject approachee;

	// Fields
	[SerializeField]
	[Tooltip("This can allow (or prevent) an object with the Player tag to trigger the dialogue (default: true).")]
	private bool triggerOnPlayerTag = true;

	[SerializeField]
	[Tooltip("This is the dialogue node that will be attempted to run")]
	private string dialogueToRun;

	[SerializeField]
	[Tooltip("Specifies if the approach should be only done once. If true, the default behavior is that this " +
		"game object will deactivate itself, but it does support destroying itself if destroySelfOnApproach is true. " +
		"This is only considered if the dialogue successfully runs, so if it fails to run, the game object " +
		"will still be in the scene.")]
	private bool approachOnce = false;

	[SerializeField]
	[Tooltip("Specifies if the game object should destroy itself after the approach happens. By default, " +
		"the game object will deactivate itself (default: false).")]
	private bool destroySelfOnApproach = false;

	// Unity Functions

	// Start function finds and initializes the dialogue runner
	void Start()
	{
		dialogueRunner = GameObject.Find("DialogueRunner").GetComponent<DialogueRunner>();
	}

	// Checks the gameobject that entered the trigger, determining whether to run the dialogue
	private void OnTriggerEnter(Collider other)
	{
		// Check if the other gameObject is the approachee or if it contains the Player tag
		if ((other.gameObject == approachee) || (other.gameObject.CompareTag("Player")))
		{
			// if so, run the dialogue
			RunDialogue();
		}
	}

	// Functions

	/// <summary>
	/// Runs the dialogue indicated by <seealso cref="dialogueToRun"/>.
	/// </summary>
	private void RunDialogue()
	{
		// Error case: if the dialogueRunner is null, log the error
		if (dialogueRunner == null)
		{
			Debug.LogError($"Tried to run dialogue on an approach, but DialogueRunner was not found in scene.");
			return;
		}

		// Error case: the dialogue to run is invalid
		if (!dialogueRunner.NodeExists(dialogueToRun))
		{
			Debug.LogError($"There is no node \"{dialogueToRun}\" to run on approach.");
			return;
		}

		// Run the dialogue
		dialogueRunner.StartDialogue(dialogueToRun);
		// check if the approach game object should be destroyed/deactivated
		if (approachOnce)
		{
			if (destroySelfOnApproach)
			{
				Destroy(gameObject);
			}
			else
			{
				gameObject.SetActive(false);
			}
		}
	}
}
