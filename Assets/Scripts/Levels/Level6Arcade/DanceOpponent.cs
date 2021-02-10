using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DanceOpponent : MonoBehaviour
{
    // Image component of DanceOpponent to modify
    [HideInInspector]
    public Image image;

    // Sprites to switch to
    public Sprite def; // when not doing Up/Down/Left/Right
    public Sprite down;
    public Sprite left;
    public Sprite right;
    public Sprite up;

    // define image on start
    void Awake()
    {
        image = GetComponent<Image>();
        if (def == null)
        {
            // if def is not defined, then it will just copy the original sprite
            def = image.sprite;
        }
    }

    // Functions to change what image to display
    public void DisplayUp()
    {
        image.sprite = up;
    }
    public void DisplayDown()
    {
        image.sprite = down;
    }
    public void DisplayLeft()
    {
        image.sprite = left;
    }
    public void DisplayRight()
    {
        image.sprite = right;
    }
    public void DisplayDefault()
    {
        image.sprite = def;
    }
}
