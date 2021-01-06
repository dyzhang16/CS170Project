﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePuzzle : MonoBehaviour
{
    public GameObject puzzlePanel;


    void OnTriggerStay2D(Collider2D collider)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            puzzlePanel.SetActive(!puzzlePanel.activeSelf);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        puzzlePanel.SetActive(false);
    }
}