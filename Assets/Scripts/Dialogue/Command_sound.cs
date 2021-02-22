using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Command_sound : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    // Start is called before the first frame update

    void Awake()
    {
        dialogueRunner.AddCommandHandler(
        "Sound",  // the name of the command
        playSound // the method to run
        );

    }

    private void playSound(string[] noise)

    {
        Debug.Log("playing sound");
        string sound = noise[0];
        SoundManagerScript.PlaySound(sound);
    }

}
