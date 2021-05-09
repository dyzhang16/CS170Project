using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class ShowPuzzleTimer : MonoBehaviour
{
    public GameObject text;
    [YarnCommand("ShowTimer")]
    public void ShowTime() 
    {
        Timer Timer = Canvas.FindObjectOfType<Timer>();
        Debug.Log(Timer);
        Timer.timerIsRunning = true;
        text.SetActive(true);
    }
}
