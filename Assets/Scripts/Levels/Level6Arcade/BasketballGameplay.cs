using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasketballGameplay : MonoBehaviour
{
	// Unity Objects
	public Button startButton;
	public Button exitButton;

	// Basketball-specific Unity Objects
	public BasketballNet basketballNet;
	public BasketballMovement basketball;
	public Text playerScoreText;
	public Text highScoreText;
	public Text playerCounterText;

	// Fields
	private const int pointValue = 1; // how much each basket is worth
	private int playerScore;
	public int highScore = 10 * pointValue; // player's high score scaled by pointValue
	public int playerCounter = 15;     // player's current counter (either time or # of throws) (playerCounter <= counterLimit)
	public int counterLimit = 15;      // max counter to reset to when game is done
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
	//		Also handles if the same button is pressed to prematurely end the game
	public void StartGame()
	{
		// First check if the "start" button was pressed again to stop the game early
		//	by checking if the exit button is not interactable
		bool endGameEarly = exitButton.interactable == false;
		if (endGameEarly)
		{
			// call the function to end the game
			EndGameImmediate();
			// do not run the rest of the start function
			return;
		}

		// Disable the exit button
		exitButton.interactable = false;
		// Change the start button to be a quit button
		startButton.GetComponentInChildren<Text>().text = "Quit";

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

	// This function is called if the player wants to end the game prematurely
	public void EndGameImmediate()
	{
		// Set the playerCounter to 0 to stop any running coroutine
		playerCounter = 0;
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

		// Change the "quit" button to be a start button
		startButton.GetComponentInChildren<Text>().text = "Start";
		// Reenable the exit button
		exitButton.interactable = true;
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

	// Increment the score
	public void IncrementScore()
	{
		playerScore += pointValue;
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
		// format the high score
		string highScoreString = string.Format("High Score: {0}", highScore);
		// update the UI element
		highScoreText.text = highScoreString;
	}

	// Refresh the time (i.e. counter) text element
	public void RefreshCounter()
	{
		// if the playerCounter is negative, set it to 0 before updating the UI element
		if (playerCounter < 0)
		{
			playerCounter = 0;
		}

		// update the UI element
		playerCounterText.text = playerCounter.ToString();
	}
}

