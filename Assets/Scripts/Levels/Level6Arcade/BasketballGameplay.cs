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
	public Text playerTimeText;

	// Fields
	private int playerScore;
	public int highScore = 30;
	private int playerTime = 30;    // player's current time (playerTime <= timeLimit)
	public int timeLimit = 30;      // max time to reset to

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
		// Reset the timer
		playerTime = timeLimit;

		// start throwing the basketball
		basketball.StartThrowing();
		// start the timer
		StartCoroutine(CountdownTimerCR());

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
		while (basketball.isThrowing && playerTime > 0)
		{
			yield return new WaitForSeconds(1);
			playerTime--;
			RefreshTime();
		}
		RefreshTime();
		// When timer runs out, stop throwing the basketball
		basketball.isThrowing = false;
		// Do the Game End callback (once the basketball is no longer being thrown)
		while (basketball.currentlyThrown)
		{
			yield return null;
		}
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
		RefreshTime();
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

	// Refresh the time text element
	void RefreshTime()
	{
		playerTimeText.text = playerTime.ToString();
	}
}

