using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class YarnCommand : MonoBehaviour
{
    public GameObject puzzlePanel;

    [YarnCommand("Show")]
    public void ShowPuzzle()
    {
        puzzlePanel.GetComponent<CanvasGroup>().alpha = 1;
    }

}
