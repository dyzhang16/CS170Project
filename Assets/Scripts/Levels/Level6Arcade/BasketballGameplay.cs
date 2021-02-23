using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasketballGameplay : MonoBehaviour
{
	// Attach Unity Objects
	public BasketballMovement basketball;
	public BasketballNet basketballNet;
	public Button startButton;
	public Button exitButton;
	// scoreboard
	public GameObject scoreboard; // this will contain the timer and score UI elements
	[HideInInspector]
	public Text timerText; // taken from scoreboard GameObject
	[HideInInspector]
	public Text scoreText; // taken from scoreboard GameObject
	[HideInInspector]
	public Text highScoreText; // taken from scoreboard GameObject

	// Fields
	public int playerScore = 0;
	public int timer = 0;
	public int highScore = 15;
	public readonly int MAX_TIME = 30; // time that is set per game

	// Used for stopping the ThrowScheduler Coroutine
	private Coroutine throwSchedulerRoutine;

	/// <summary>
	/// Initialize things
	/// </summary>
	void Start()
	{
		timerText = scoreboard.transform.Find("PlayerTimeText").GetComponent<Text>();
		scoreText = scoreboard.transform.Find("PlayerScoreText").GetComponent<Text>();
		highScoreText = scoreboard.transform.Find("HighScoreText").GetComponent<Text>();
		UpdateTimer();
		UpdateScore();
		UpdateHighScore();
		EndGame();
	}

	/// <summary>
	/// Throwing Basketball loop
	/// </summary>
	IEnumerator ThrowScheduler()
	{
		// Throw the ball
		yield return StartCoroutine(basketball.ThrowRoutine());
		// Wait a bit (random wait)
		yield return new WaitForSeconds(Random.Range(0.2f, 1f));
		// Start another Coroutine instance (let this current coroutine end)
		throwSchedulerRoutine = StartCoroutine(ThrowScheduler());
	}

	/// <summary>
	/// Used to count down the timer and update the scoreboard,
	/// and ends the game after the timer is up.
	/// </summary>
	IEnumerator ScoreboardTimer()
	{
		while (timer > 0)
		{
			UpdateTimer();
			timer--;
			yield return new WaitForSecondsRealtime(1f);
		}
		UpdateTimer();

		// end the game after timer is up and the ball stopped moving
		while (basketball.isMoving)
		{
			yield return null;
		}
		EndGame();
	}

	/// <summary>
	/// Routine used for anything that requires a delayed start
	/// </summary>
	IEnumerator DelayedStartRoutine()
	{
		yield return new WaitForSeconds(1f); // Delay
		StartCoroutine(ScoreboardTimer());
		throwSchedulerRoutine = StartCoroutine(ThrowScheduler());
	}

	/// <summary>
	/// Starts the game (basketball will begin launching)
	/// </summary>
	public void StartGame()
	{
		Debug.Log("Basketball Gameplay Started");
		// initialize some fields
		timer = MAX_TIME;
		UpdateTimer();
		playerScore = 0;
		UpdateScore();
		basketballNet.trackMouse = true;
		startButton.interactable = false;
		exitButton.interactable = false;

		// Start Coroutines
		StartCoroutine(DelayedStartRoutine());
	}

	/// <summary>
	/// Ends the game, resetting some things to default.
	/// This is used in ScoreboardTimer()
	/// </summary>
	public void EndGame()
	{
		// reset variables
		basketballNet.trackMouse = false;
		startButton.interactable = true;
		exitButton.interactable = true;

		// update highscore if needed
		if (playerScore > highScore)
		{
			highScore = playerScore;
			UpdateHighScore();
		}

		// stop the throwing loop
		if (throwSchedulerRoutine != null)
		{
			StopCoroutine(throwSchedulerRoutine);
			throwSchedulerRoutine = null;
		}
	}

	/// <summary>
	/// Increases the score by one
	/// </summary>
	public void IncrementScore()
	{
		playerScore++;
		UpdateScore();
	}

	/// <summary>
	/// Updates the timer text UI element
	/// </summary>
	public void UpdateTimer()
	{
		timerText.text = string.Format(":{0}{1}", (timer <= 9)?"0":"", timer.ToString());
	}

	/// <summary>
	/// Updates the score text UI element
	/// </summary>
	public void UpdateScore()
	{
		scoreText.text = playerScore.ToString();
	}

	/// <summary>
	/// Updates the highscore text UI element
	/// </summary>
	public void UpdateHighScore()
	{
		highScoreText.text = string.Format("Highscore: {0}", highScore);
	}
}
