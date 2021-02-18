using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DanceGameplay : MonoBehaviour
{
	// enum for movements (castable to integers)
	public enum Move
	{
		DOWN	= 0,
		LEFT	= 1,
		UP		= 2,
		RIGHT	= 3
	}

	// Attached Unity Objects
	public DanceOpponent opponent;
	public Button[] playerControls;
	public Button startGameButton;
	public Button endGameButton;
	public Text roundAccuracy; // accuracy for a given round
	public Text overallAccuracy; // accuracy overall
	public Text roundCounter;
	public GameObject resultsPanel; // to show at end of game (shows results)
	public Button exitButton;

	// bool for checking if the moves are ready to show
	private bool readyToShow = false;

	// list of moves to do
	public List<Move> movesToDo;

	// list of player's current moves
	public List<Move> playerMoves;

	// level (i.e., number of moves)
	public int level = 3;
	public readonly int MIN_LEVEL = 3;
	public readonly int MAX_LEVEL = 7;

	// Number of rounds to play
	public readonly int NUM_ROUNDS = 2;
	public int round = 0; // increments on GenerateMoves call

	// Fields used in calculating accuracy
	public readonly float PASSING_ACCURACY = 0.75f;
	public int numCurrentCorrectMoves = 0; // Correct moves in current round
	public int numTotalCorrectMoves; // Correct moves in total
	public int totalMoves = 0; // incremented during move list generation

	// initialization
	void Awake()
	{
		// At start, treat it as if the game ended
		EndCurrentGame();
	}

	/// <summary>
	/// Creates a new game.
	/// </summary>
	public void InitializeNewGame()
	{
		exitButton.interactable = false;
		numCurrentCorrectMoves = 0;
		numTotalCorrectMoves = 0;
		totalMoves = 0;
		round = 0;

		// accuracy text components
		UpdateRoundAccuracy(0);
		roundAccuracy.enabled = false;
		UpdateOverallAccuracy(0);
		overallAccuracy.enabled = true;

		// Round counter text components
		UpdateRoundCounter();
		roundCounter.enabled = true;

		endGameButton.gameObject.SetActive(true);
		startGameButton.gameObject.SetActive(false);
		movesToDo = new List<Move>();
		playerMoves = new List<Move>();

		GenerateMoves(MIN_LEVEL);
		StartCoroutine(ShowMovesRoutine());
	}

	/// <summary>
	/// Ends the current game
	/// </summary>
	public void EndCurrentGame()
	{
		// Back end reset
		DisablePlayerControls();
		StopAllCoroutines();

		// Visual reset
		startGameButton.gameObject.SetActive(true);
		roundAccuracy.enabled = false;
		overallAccuracy.enabled = false;
		opponent.DisplayDefault();
		roundCounter.enabled = false;
		endGameButton.gameObject.SetActive(false);
		exitButton.interactable = true;
	}

	/// <summary>
	/// Wrapper for EndCurrentGame(), calling functions related to
	/// the results panel
	/// </summary>
	public void EndCurrentGameWithResults()
	{
		UpdateResultsPanel();
		ShowResultsPanel();
		EndCurrentGame();
	}

	/// <summary>
	/// Does a player move (adding to playerMoves list).
	/// If a player move is the last move, a new move list will be generated.
	/// If the player's accuracy (i.e. correct moves / total moves in that round) is
	/// less than 50%, the player will go down a level.
	/// </summary>
	/// <param name="move">Move number (casted to Move enum)</param>
	public void PlayerMove(int move)
	{
		// if valid move
		if (move >= 0 && move <= 3)
		{
			// if the move is correct, increment numCorrectMoves
			if ((int)movesToDo[playerMoves.Count] == move)
			{
				numCurrentCorrectMoves++;
				numTotalCorrectMoves++;
			}

			// Add the move to the playerMoves list
			playerMoves.Add((Move)move);

			// If the # of player moves == # of moves in that round, calculate
			//	the current accuracy and decide how to generate the next round
			if (playerMoves.Count >= movesToDo.Count)
			{
				// Show accuracy of this round
				float accuracy = ((float)numCurrentCorrectMoves) / movesToDo.Count;
				UpdateRoundAccuracy(accuracy);
				Debug.Log("Accuracy of this round: " + (accuracy * 100) + "%");
				numCurrentCorrectMoves = 0; // reset this
				if (accuracy >= 0.5) // Pass
				{
					GenerateNextLevel();
					StartCoroutine(ShowCorrectRoutine());
				}
				else // Fail
				{
					GenerateMoves(level - 1);
					StartCoroutine(ShowWrongRoutine());
				}
			}
		}
	}
	/// <summary>
	/// Does a player move using a string (adding to playerMoves list)
	/// </summary>
	/// <param name="move">Move as string ("down", "left", "right", "up")</param>
	public void PlayerMove(string move)
	{
		string lowerCase = move.ToLower();
		switch (lowerCase)
		{
			case "down":
				PlayerMove((int)Move.DOWN);
				break;
			case "left":
				PlayerMove((int)Move.LEFT);
				break;
			case "right":
				PlayerMove((int)Move.RIGHT);
				break;
			case "up":
				PlayerMove((int)Move.UP);
				break;
			default:
				throw new System.Exception(string.Format("PlayerMove called with invalid string: \"{0}\". Accepted values (case insensitive): " +
					"[\"down\", \"left\", \"right\", \"up\"]", move));
		}
	}


	/// <summary>
	/// This function generates the moves for a given level. This
	/// also updates the overall accuracy text component.
	/// </summary>
	/// <param name="level">The number of moves for a level, constrained by
	/// MIN_LEVEL and MAX_LEVEL.</param>
	public void GenerateMoves(int level)
	{
		round++; // update round number
		if (round > NUM_ROUNDS)
		{
			return;
		}
		else
		{
			UpdateRoundCounter();
		}
		// Show overall accuracy
		if (totalMoves > 0)
		{
			Debug.Log(string.Format("Overall accuracy: {0}%", ((float)numTotalCorrectMoves / (float)totalMoves) * 100));
			UpdateOverallAccuracy((float)numTotalCorrectMoves / (float)totalMoves);
		}
		// moves not ready to show yet
		readyToShow = false;
		// Disable player controls
		DisablePlayerControls();
		// assign level
		this.level = Mathf.Clamp(level, MIN_LEVEL, MAX_LEVEL);
		// clear the move lists
		movesToDo.Clear();
		playerMoves.Clear();
		// Randomly add moves to movesToDo list
		for (int i = 0; i < this.level; i++)
		{
			movesToDo.Add((Move)Random.Range(0, 4));
			totalMoves++;
		}

		// indicate that the moves are now ready to show
		readyToShow = true;
	}

	/// <summary>
	/// This function generates the moves for the next level (level + 1),
	/// constrained by MAX_LEVEL.
	/// </summary>
	public void GenerateNextLevel()
	{
		GenerateMoves(level + 1);
	}

	/// <summary>
	/// This function will show the moves at a defined delay.
	/// </summary>
	/// <returns></returns>
	IEnumerator ShowMovesRoutine()
	{
		if (!readyToShow)
		{
			throw new System.Exception("ShowMovesRoutine(): Moves were not ready to show yet!");
		}
		yield return new WaitForSeconds(0.25f);
		foreach (Move move in movesToDo)
		{
			ShowOneMove(move);
			yield return new WaitForSeconds(0.50f);
			opponent.DisplayDefault();
			yield return new WaitForSeconds(0.15f);
		}
		EnablePlayerControls();
	}

	/// <summary>
	/// This function is used to show "correct"-ness.
	/// In the current iteration, it is used to show the accuracy in
	/// the color green.
	/// </summary>
	IEnumerator ShowCorrectRoutine()
	{
		roundAccuracy.enabled = true; // enable the text component
		Color defColor = roundAccuracy.color; // save the current color
		roundAccuracy.color = Color.green; // change text color
		yield return new WaitForSeconds(0.7f);
		roundAccuracy.color = defColor; // revert to old color
		roundAccuracy.enabled = false; // disable text component
		yield return new WaitForSeconds(0.35f);
		if (round > NUM_ROUNDS)
		{
			EndCurrentGameWithResults();
		}
		else
		{
			yield return StartCoroutine(ShowMovesRoutine());
		}
	}

	/// <summary>
	/// This function is used to show "incorrect"-ness.
	/// In the current iteration, it is used to show the accuracy in
	/// the color red.
	/// </summary>
	IEnumerator ShowWrongRoutine()
	{
		roundAccuracy.enabled = true; // enable the text component
		Color defColor = roundAccuracy.color; // save the current color
		roundAccuracy.color = Color.red; // change text color
		yield return new WaitForSeconds(0.7f);
		roundAccuracy.color = defColor; // revert to old color
		roundAccuracy.enabled = false; // disable text component
		yield return new WaitForSeconds(0.35f);
		if (round > NUM_ROUNDS)
		{
			EndCurrentGameWithResults();
		}
		else
		{
			yield return StartCoroutine(ShowMovesRoutine());
		}
	}

	/// <summary>
	/// This function will call a Display____() function on
	/// the DanceOpponent.
	/// </summary>
	/// <param name="move"></param>
	private void ShowOneMove(Move move)
	{
		switch(move)
		{
			case Move.DOWN:
				opponent.DisplayDown();
				break;
			case Move.LEFT:
				opponent.DisplayLeft();
				break;
			case Move.RIGHT:
				opponent.DisplayRight();
				break;
			case Move.UP:
				opponent.DisplayUp();
				break;
		}
	}

	/// <summary>
	/// Disable the player controls. This should be called when the game is
	/// generating new moves (in GenerateMoves()).
	/// </summary>
	public void DisablePlayerControls()
	{
		foreach(Button button in playerControls)
		{
			button.interactable = false;
		}
	}
	/// <summary>
	/// Enable the player controls. This should be called when the game is
	/// done showing the new moves (in ShowMovesRoutine()).
	/// </summary>
	public void EnablePlayerControls()
	{
		foreach (Button button in playerControls)
		{
			button.interactable = true;
		}
	}


	/// <summary>
	/// Updates the text value for the accuracy of the current round
	/// </summary>
	/// <param name="accuracy">Accuracy as a number between 0 and 1.</param>
	public void UpdateRoundAccuracy(float accuracy)
	{
		roundAccuracy.text = Mathf.Floor(accuracy * 100).ToString() + "%";
	}
	/// <summary>
	/// Updates the text value for the overall accuracy
	/// </summary>
	/// <param name="accuracy">Accuracy as a number between 0 and 1.</param>
	public void UpdateOverallAccuracy(float accuracy)
	{
		overallAccuracy.text = Mathf.Floor(accuracy * 100).ToString() + "%";
	}

	/// <summary>
	/// Updates the round counter with the current value
	/// </summary>
	public void UpdateRoundCounter()
	{
		roundCounter.text = string.Format("{0} / {1}", round, NUM_ROUNDS);
	}

	/// <summary>
	/// Updates the results panel with the relevant info
	/// </summary>
	public void UpdateResultsPanel()
	{
		// Update Grade

		// Update Accuracy
		Text resultsAccuracy = resultsPanel.transform.Find("Accuracy/PlayerAccuracyText").GetComponent<Text>();
		float rawAccuracy = ((float)numTotalCorrectMoves) / totalMoves;
		Debug.Log(rawAccuracy);
		resultsAccuracy.text = string.Format("{0}%", Mathf.Floor(rawAccuracy * 100));
	}

	/// <summary>
	/// Shows the results panel (by enabling it)
	/// </summary>
	public void ShowResultsPanel()
	{
		resultsPanel.SetActive(true);
	}
}
