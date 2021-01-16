using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class ChangeSpeaker : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    public GameObject CharacterImage;
    public GameObject TalkingCharacter;

    public void Awake()
    {

        // Create a new command called 'camera_look', which looks at a target.
        dialogueRunner.AddCommandHandler(
            "ChangeSpeaker",     // the name of the command
            ChangeImage // the method to run
        );
    }

    private void ChangeImage(string[] parameters)
    {
        string Person = parameters[0];
        TalkingCharacter = GameObject.Find(Person);
        if (TalkingCharacter == null)
        {
            Debug.Log($"Cannot Find {TalkingCharacter}");
            return;
        }
        Debug.Log($"{TalkingCharacter}: is talking.");
        CharacterImage.GetComponent<UnityEngine.UI.Image>().sprite = TalkingCharacter.GetComponent<SpriteRenderer>().sprite;
    }

}
