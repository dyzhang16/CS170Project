using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HidePuzzle : MonoBehaviour
{
    public GameObject puzzlePanel;
    public Button RemoveButton;
    public void Hide()
    {
        puzzlePanel.GetComponent<CanvasGroup>().alpha = 0;
        puzzlePanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
}
