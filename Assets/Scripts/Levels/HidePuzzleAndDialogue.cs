using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class HidePuzzleAndDialogue : MonoBehaviour
{
    public GameObject puzzlePanel;
    public Button RemoveButton;
    public bool puzzleFinished = false;
    public DialogueRunner dialogueRunner;
    public string dialogueToRun;

    [Tooltip("Restart's the player's movement when a puzzle is hidden")]
    public bool allowPlayerMovement = false; // boolean to see if Hide() allows player movement

    public void Hide()
    {
        puzzlePanel.GetComponent<CanvasGroup>().alpha = 0;
        puzzlePanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
        // allow background interactions
        GameObject blocker = GameObject.Find("Canvas/Blocker");
        if (blocker)
        {
            blocker.GetComponent<CanvasGroup>().alpha = 0;
            blocker.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }

        // do allow player movement
        GameObject playerGameObject = GameObject.Find("Player");
        if (allowPlayerMovement && playerGameObject)
        {
            Player player = playerGameObject.GetComponent<Player>();
            if (player)
            {
                player.AllowMove(true);
            }
        }
        else if (!allowPlayerMovement && playerGameObject) // Warning if the player is not allowed to move when Hide() was called
        {
            Player player = playerGameObject.GetComponent<Player>();
            if (player && !player.allowMovement)
            {
                Debug.LogWarning(string.Format("HidePuzzle.cs: A puzzle panel was hidden, but the " +
                    "player was not allowed to move because the boolean" +
                    "'Allow Player Movement' was set to false for this puzzle panel. If this was not " +
                    "intended, then make sure to check this in the inspector."));
            }
        }
        if (puzzleFinished)
        {
            dialogueRunner.StartDialogue(dialogueToRun);
        }
    }
}

