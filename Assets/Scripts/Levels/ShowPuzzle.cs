﻿using System.Collections;
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
    }
}
