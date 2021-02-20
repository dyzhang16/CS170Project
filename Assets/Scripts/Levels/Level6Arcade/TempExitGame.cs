using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class TempExitGame : MonoBehaviour
{
	DialogueRunner dialogueRunner;
	void Awake()
	{
		dialogueRunner = GameObject.FindObjectOfType<DialogueRunner>();
	}
	void Start()
	{
		dialogueRunner.AddCommandHandler(
			"ExitGame",
			(string[] args) =>
			{
				Debug.Log("Game Exited");
				Application.Quit();
			}
		);
	}
}
