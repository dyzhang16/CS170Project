using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasketballGameplay : MonoBehaviour
{
	// Unity Objects
	public Button startButton;

	// Basketball-specific Unity Objects
	public BasketballNet basketballNet;
	public BasketballMovement basketball;
	public Text playerScoreText;
	public Text highScoreText;
	public Text playerCounterText;

	// Fields
	private int playerScore;
	public int highScore = 30;
	[HideInInspector]
	public int playerCounter = 30;     // player's current counter (either time or # of throws) (playerCounter <= counterLimit)
	public int counterLimit = 30;      // max counter to reset to when game is done
	public BasketballMode currentMode = BasketballMode.LimitedThrows; // play mode

	// Extra Stuff
	public enum BasketballMode
	{
		Timed, // player has to make as many baskets within a given time limit
		LimitedThrows // player has a limited number of throws
	};

	void Start()
	{
		RefreshScoreboard();
	}

	// Function used for starting the game, called by the start button
	public void StartGame()
	{
		// Disable the start button
		startButton.interactable = false;

		// Reset the player's score
		playerScore = 0;
		// Reset the counter
		playerCounter = counterLimit;

		// start the throwing the basketball
		basketball.StartThrowing();

		// check the current mode to change how the internal counter works
		switch (currentMode)
		{
			case BasketballMode.Timed:
				// start the timer
				StartCoroutine(CountdownTimerCR());
				break;
			case BasketballMode.LimitedThrows:
				// start the limited throws coroutine
				StartCoroutine(CountdownThrowsCR());
				break;
		}

		// Refresh the scoreboard
		RefreshScoreboard();
	}

	// Callback function for when the game ends
	private void EndGameCallback()
	{
		// Update the high score
		if (highScore < playerScore)
		{
			highScore = playerScore;
			RefreshHighScore();
		}

		// Reenable the start button
		startButton.interactable = true;
	}

	// Coroutine for counting down a timer
	IEnumerator CountdownTimerCR()
	{
		// loop condition: keep looping until the timer has expired
		while (basketball.isThrowing && playerCounter > 0)
		{
			yield return new WaitForSeconds(1);
			playerCounter--;
			RefreshCounter();
		}
		RefreshCounter();
		// When timer runs out, stop throwing the basketball
		basketball.isThrowing = false;
		// Do the Game End callback (once the basketball is no longer being thrown)
		while (basketball.currentlyThrown)
		{
			yield return null;
		}
		EndGameCallback();
	}

	// Coroutine for counting down the number of throws
	IEnumerator CountdownThrowsCR()
	{
		// Wait until the playerCounter number is 0 or less
		//	NOTE: playerCounter in this mode is updated in BasketballMovement.ThrowCR() if the currentMode is LimitedThrows
		yield return new WaitUntil(() => playerCounter <= 0);
		// update visuals
		RefreshCounter();
		// stop throwing the basketball
		basketball.isThrowing = false;
		// Do the Game End callback (once the basketball is no longer being thrown)
		yield return new WaitUntil(() => !basketball.currentlyThrown);
		EndGameCallback();
	}

	// Increment the score (by 2)
	public void IncrementScore()
	{
		playerScore += 2;
		RefreshPlayerScore();
	}

	// Called when a value is changed in the inspector, not for actual use
	void OnValidate()
	{
		// Update the highscore text (since it seems to be the only editable one)
		RefreshHighScore();
	}

	// Refreshes the scoreboard elements
	void RefreshScoreboard()
	{
		RefreshPlayerScore();
		RefreshHighScore();
		RefreshCounter();
	}

	// Refresh the player score text element
	void RefreshPlayerScore()
	{
		playerScoreText.text = playerScore.ToString();
	}

	// Refresh the high score text element
	void RefreshHighScore()
	{
		highScoreText.text = highScore.ToString();
	}

	// Refresh the time (i.e. counter) text element
	public void RefreshCounter()
	{
		playerCounterText.text = playerCounter.ToString();
	}
}

