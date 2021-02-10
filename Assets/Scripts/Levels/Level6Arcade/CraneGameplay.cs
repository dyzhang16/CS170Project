using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraneGameplay : MonoBehaviour
{
	// Attached Unity Objects
	public Button coinButton; // insert a coin
	public Slider joystickSlider; // moving claw left and right
	public Button dropButton; // drop the claw
	public ClawMovement claw; // movement component from claw

	// other fields
	public bool isPlaying = false;
	public int credits = 5; // # of tries
	public int activeTries = 0; // number of credits currently in the machine

	void Awake()
	{
		StopPlay();
	}

	void Update()
	{
		// If user has no credits, the coin button shouldn't work
		if (credits <= 0)
		{
			coinButton.interactable = false;
			coinButton.gameObject.GetComponentInChildren<Text>().text = "No more credits!";
		}
		else
		{
			coinButton.gameObject.GetComponentInChildren<Text>().text =
				string.Format("Insert Coin ({0} coin{1} left)", credits, (credits != 1)?"s":"");
		}

		// If a game has not been started, and the claw isn't dropping, and the player
		//	has more than 0 active tries
		if (!isPlaying && !claw.isDropping && activeTries > 0)
		{
			StartPlay();
		}

		// interactability of controls depends on isPlaying and whether the claw is dropping or not
		joystickSlider.interactable = isPlaying && !claw.isDropping;
		dropButton.interactable = isPlaying && !claw.isDropping;
	}

	/// <summary>
	/// Function called by the Coin Button to update credits and activeTries
	/// </summary>
	public void InsertCoin()
	{
		if (credits > 0)
		{
			credits--;
			activeTries++;
		}
	}

	/// <summary>
	/// Function called in Update() to set isPlaying to true
	/// </summary>
	public void StartPlay()
	{
		isPlaying = true;
		activeTries--;
	}

	/// <summary>
	/// Function called by the Drop button to stop gameplay
	/// </summary>
	public void StopPlay()
	{
		isPlaying = false;
	}
}
