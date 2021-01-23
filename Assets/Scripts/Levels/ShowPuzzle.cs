using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class ShowPuzzle : MonoBehaviour
{
    public GameObject puzzlePanel;
    [YarnCommand("Show")]
    public void Puzzle()
    {
        puzzlePanel.GetComponent<CanvasGroup>().alpha = 1;
        puzzlePanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
        // block background interactions
        GameObject blocker = GameObject.Find("Canvas/Blocker");
        blocker.GetComponent<CanvasGroup>().alpha = 1;
        blocker.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
