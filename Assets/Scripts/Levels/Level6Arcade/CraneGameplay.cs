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
	public Button exitButton;

	// other fields
	public bool isPlaying = false;
	public int credits = 5; // # of tries
	public int activeTries = 0; // number of credits currently in the machine
	private CranePrize currentPrize; // this is primarily for the exit button interactability

	void Awake()
	{
		StopPlay();
		currentPrize = null;
	}

	void Update()
	{
		// This if-elseif block of code is just for the exit button interactibility to work
		/** Explanation: In order to hold onto the claw's current prize all the
		 * way until the prize drops into the receptacle, we have to ensure that
		 * we get the CranePrize component only once and use that to modify our
		 * local currentPrize.
		 * Then, while we have the current prize saved, we will only reset to null
		 * when it is done falling.
		 */
		// capture the current prize (for the first time)
		if (claw.currentPrize != null && currentPrize == null)
		{
			currentPrize = claw.currentPrize.GetComponent<CranePrize>();
		}
		else if (currentPrize != null)
		{
			if (!currentPrize.isFalling)
			{
				currentPrize = null;
			}
		}

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

		// interactibility of the exit button is modified here
		/* Explanation of the two lines of code below:
		 * The exitButton should not be interactable in three situations
		 *	- A game has started (because a coin has been inserted)
		 *	- The claw is doing its dropping routines (drop, rise, return to start)
		 *	- A prize may be falling. This is verified by checking if currentPrize is null
		 */
		if (isPlaying || claw.isDropping || currentPrize != null)
		{
			exitButton.interactable = false;
		}
		else
		{
			exitButton.interactable = true;
		}
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
		Debug.Log("Crane Gameplay Started");
	}

	/// <summary>
	/// Function called by the Drop button to stop gameplay
	/// </summary>
	public void StopPlay()
	{
		isPlaying = false;
	}
}
