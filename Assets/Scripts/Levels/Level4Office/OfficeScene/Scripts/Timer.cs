﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Yarn.Unity;


public class Timer : MonoBehaviour //https://gamedevbeginner.com/how-to-make-countdown-timer-in-unity-minutes-seconds/
{
    private float timeRemaining = 30;
    public bool timerIsRunning = false;
    public TextMeshProUGUI timeText;
    public DialogueRunner DialogueRunner;
    public GameObject FinishedDocument, FriendDocument, Document;

    void Update()
    {
        if (timerIsRunning)             //will execute function once
        {
            if (timeRemaining > 0)      //while running
            {
                timeRemaining -= Time.deltaTime;    //gets time in seconds
            }
            else
            {
                // Debug.Log("Time has run out!");
                timeRemaining = 0;                  //reset variables to prevent function from executing repeatedly
                timerIsRunning = false;
                GameManager.instance.officeDeskPuzzle = 1;
                if (FinishedDocument.GetComponent<FinishedDocument>().documentFinished > FriendDocument.GetComponent<friendFinishedDocuments>().documentFinished)
                {
                    DialogueRunner.StartDialogue("PlayerWin");
                }
                else if (FinishedDocument.GetComponent<FinishedDocument>().documentFinished < FriendDocument.GetComponent<friendFinishedDocuments>().documentFinished)
                {
                    DialogueRunner.StartDialogue("FredricWin");
                }
                else
                {
                    DialogueRunner.StartDialogue("Tie");
                }
            }
        DisplayTime(timeRemaining);
        }
    }
    [YarnCommand("ResetTimer")]
    public void ResetTime()
    {
        FinishedDocument.GetComponent<FinishedDocument>().documentFinished = 0;
        FinishedDocument.GetComponent<FinishedDocument>().DisplayFinishedDocuments();
        GameObject c = Document.transform.GetChild(0).gameObject;
        Destroy(c);
        FinishedDocument.GetComponent<FinishedDocument>().SpawnDocument();
        FriendDocument.GetComponent<friendFinishedDocuments>().documentFinished = 0;
        
        timeRemaining = 30;
        timerIsRunning = false;
    }
    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float milliSeconds = (timeToDisplay % 1) * 1000;

        timeText.text = string.Format("Time Remaining: {0:00}:{1:00}", minutes, seconds);
    }
    
}
