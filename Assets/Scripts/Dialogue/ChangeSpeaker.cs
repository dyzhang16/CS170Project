using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class ChangeSpeaker : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    public Text txt;
    public GameObject CharacterImage; // character on the left
    public GameObject SecondCharacterImage; // character on the right
    public GameObject TalkingCharacter;
    public GameObject SecondTalkingCharacter;

    // Dictionary for keeping track of character names (for focus)
    private Dictionary<string, UnityEngine.UI.Image> focusPairs; // maps character names to Images

    public void Awake()
    {
        txt = txt.GetComponent<Text>();
        // Create a new command called 'camera_look', which looks at a target.
        dialogueRunner.AddCommandHandler(
            "ChangeSpeaker",     // the name of the command
            ChangeImage // the method to run
        );
        // New YarnCommand for specifying focus
        dialogueRunner.AddCommandHandler(
            "Focus",
            SetFocus
        );

        // initialize focusPairs
        focusPairs = new Dictionary<string, UnityEngine.UI.Image>();
    }

    private void ChangeImage(string[] parameters)
    {
        // reset focusPairs
        focusPairs.Clear();

        // Inner function to change the image
        Action<string, GameObject, GameObject> DoChange = (string person, GameObject talkingCharacter, GameObject characterImage) =>
        {
            string Person = person;
            talkingCharacter = GameObject.Find(person);
            if (talkingCharacter == null)
            {
                Debug.Log($"Cannot Find {talkingCharacter}");
                return;
            }
            Debug.Log($"{talkingCharacter}: is talking.");
            Debug.Log("This is stored in text: " + txt.text);
            characterImage.GetComponent<UnityEngine.UI.Image>().sprite = talkingCharacter.GetComponent<SpriteRenderer>().sprite;
        };

        // ChangeImage for first character
        DoChange(parameters[0], TalkingCharacter, CharacterImage);
        focusPairs.Add(parameters[0], CharacterImage.GetComponent<UnityEngine.UI.Image>()); // map character name to image

        // ChangeImage for second parameter (if it exists)
        if (parameters.Length >= 2)
        {
            // Show the second character image if it does exist
            SecondCharacterImage.SetActive(true);
            DoChange(parameters[1], SecondTalkingCharacter, SecondCharacterImage);
            focusPairs.Add(parameters[1], SecondCharacterImage.GetComponent<UnityEngine.UI.Image>()); // map character name to image
        }
        else
        {
            // Hide the second character image if it doesn't exist
            SecondCharacterImage.SetActive(false); // Hide the second character image
        }
    }

    private void SetFocus(string[] parameters)
    {
        // constants
        Color IN_FOCUS = Color.white;
        Color NOT_FOCUS = new Color(0.2f, 0.2f, 0.2f, 1);

        // check for valid parameter count
        if (parameters.Length == 0)
        {
            Debug.LogError("\"Focus\" must be called with either the name of the character, or" +
                " one of the following: FIRST, SECOND, BOTH, NONE.");
            return;
        }

        // check the argument for this command
        switch (parameters[0])
        {
            case "FIRST":
                CharacterImage.GetComponent<UnityEngine.UI.Image>().color = IN_FOCUS;
                if (SecondCharacterImage.activeInHierarchy) // if second character image is active, set color to out of focus
                {
                    SecondCharacterImage.GetComponent<UnityEngine.UI.Image>().color = NOT_FOCUS;
                }
                break;
            case "SECOND":
                if (!SecondCharacterImage.activeInHierarchy)
                {
                    Debug.LogError("Focus: Cannot set focus to second image if there is no second speaker");
                    return;
                }
                CharacterImage.GetComponent<UnityEngine.UI.Image>().color = NOT_FOCUS;
                SecondCharacterImage.GetComponent<UnityEngine.UI.Image>().color = IN_FOCUS;
                break;
            case "BOTH":
                CharacterImage.GetComponent<UnityEngine.UI.Image>().color = IN_FOCUS;
                if (SecondCharacterImage.activeInHierarchy)
                {
                    SecondCharacterImage.GetComponent<UnityEngine.UI.Image>().color = IN_FOCUS;
                }
                break;
            case "NONE":
                CharacterImage.GetComponent<UnityEngine.UI.Image>().color = NOT_FOCUS;
                if (SecondCharacterImage.activeInHierarchy)
                {
                    SecondCharacterImage.GetComponent<UnityEngine.UI.Image>().color = NOT_FOCUS;
                }
                break;
            default: // When a name is provided
                // just reuse SetFocus to unfocus everything
                string[] paramsCopy = new string[1];
                paramsCopy[0] = "NONE";
                SetFocus(paramsCopy);
                // then, set focus based on parameters[0] (which should be a name)
                if (!focusPairs.ContainsKey(parameters[0]))
                    Debug.LogError($"Could not find name: {parameters[0]}");
                else
                    focusPairs[parameters[0]].color = IN_FOCUS;
                break;
        }
    }
}
