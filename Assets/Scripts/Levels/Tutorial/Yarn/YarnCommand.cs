using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class YarnCommand : MonoBehaviour
{
    public GameObject puzzlePanel;
    public string sceneToChange;
    public bool isWalking;


    [YarnCommand("Show")]
    public void ShowPuzzle()
    {
        puzzlePanel.GetComponent<CanvasGroup>().alpha = 1;
    }
    [YarnCommand("ChangeScene")]
    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneToChange);
    }
    [YarnCommand("MoveNPC")]
    public void MoveNPC()
    {
        isWalking = true;
    }

}
