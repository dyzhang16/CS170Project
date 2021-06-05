using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

/// <summary>
/// This class/script is used in the Environment GameObject for the
/// purpose of loading up all of the yarn scripts that is used
/// in most, if not all, of the Unity Scenes. This script is disabled
/// after it calls its <c>void Update()</c> function once.
/// </summary>
[System.Obsolete("This script should not be used until an actual use can be " +
    "found for this. The main reason is because it is much easier to add " +
    "YarnPrograms via the DialogueRunner in the Inspector.", false)]
public class LoadUniversalDialogue : MonoBehaviour
{
    /// <summary>
    /// DialogueRunner object to add scripts to. This is initialized in the
    /// void Start() function.
    /// </summary>
    private DialogueRunner dialogueRunner;

    /// <summary>
    /// List of the YarnPrograms that should be residing in the directory,
    /// <c>Assets/Scripts/Dialogue/Resources</c>, without the .yarn extension
    /// at the end.
    /// <para/>
    /// To add a new Universal YarnProgram file, add it to the
    /// <c>Assets/Scripts/Dialogue/Resources</c> directory. Then, in this 
    /// collection initializer, add the name of the YarnProgram file without
    /// the .yarn extension. Be sure to add a comma right after the previous
    /// YarnProgram filename.
    /// </summary>
    private readonly List<string> universalYarnProgramFilenames = new List<string>
    {
        "TrashItem"
    };

    /// <summary>
    /// This function will initialize the DialogueRunner object
    /// </summary>
    void Start()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();
    }

    /// <summary>
    /// This function will add all of the YarnPrograms based off of
    /// the filenames in the List field, universalYarnProgramFilenames.
    /// Then, it will be disabled.
    /// </summary>
    void Update()
    {
        if (!dialogueRunner) // if dialogue runner was not found
        {
            Debug.LogError("LoadUniversalDialogue.cs: DialogueRunner was not found! No Universal YarnPrograms were added");
        }
        else // if dialogue runner was found
        {
            // Add the scripts listed in universalYarnProgramFilenames
            foreach (string filename in universalYarnProgramFilenames)
            {
                // Debug.Log(string.Format("Added {0}.yarn", filename));
                dialogueRunner.Add(Resources.Load<YarnProgram>(filename));
            }
        }

        // disable this script at the end
        enabled = false;
    }
}
