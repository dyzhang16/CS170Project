using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class ChangeSpeaker : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    public Text speakerNameText;
    public Image characterImage; // character on the left
    public Image secondCharacterImage; // character on the right

    // Dictionary for keeping track of character names (for focus)
    private List<(CharacterObj, Image)> speakerPairs;

    // constants
    static readonly Color IN_FOCUS = Color.white;
    static readonly Color NOT_FOCUS = new Color(0.5f, 0.5f, 0.5f, 1);

    public void Awake()
    {
        // Create a new command called 'ChangeSpeaker', which defines the speakers of dialogue.
        dialogueRunner.AddCommandHandler(
            "ChangeSpeaker",     // the name of the command
            ChangeImage // the method to run
        );
        // New YarnCommand for specifying focus
        dialogueRunner.AddCommandHandler(
            "Focus",
            SetFocus
        );
        // YarnCommand for setting expression of speakers
        dialogueRunner.AddCommandHandler(
            "SetExpression",
            SetExpression
        );

        // initialize speakerPairs
        speakerPairs = new List<(CharacterObj, Image)>();
    }
    private void ChangeImage(string[] parameters)
    {
        // speakerPairs focusPairs
        speakerPairs.Clear();

        // Inner function to change the image
        // https://stackoverflow.com/questions/43946997/net-4-7-returning-tuples-and-nullable-values
        // Returns tuple that can be null (check for this)
        (CharacterObj, Image)? DoChange(string person, Image characterImage)
        {
            // load character asset
            CharacterObj talkingCharacter = Resources.Load<CharacterObj>($"Characters/{person}");
            // if it cannot, then return null
            if (talkingCharacter == null)
            {
                Debug.LogError($"Cannot Find {talkingCharacter}");
                return null;
            }
            Debug.Log($"{talkingCharacter}: is talking.");
            // set sprite using talkingCharacter's default sprite
            characterImage.sprite = talkingCharacter.defaultSprite;
            if (talkingCharacter.defaultSprite == null)
            {
                //Warning for non-intended characters with no character image
                Debug.LogWarning("Not sure if this was intended, But there is no image for given character");
                //Hides Character Image in case character does not have dialogue sprite
                characterImage.gameObject.SetActive(false);
                return (talkingCharacter, characterImage);
            }
            else
            {
                //setsActive just in case if character images set inactive due to previous if statement
                characterImage.gameObject.SetActive(true);
                // return the tuple containing the CharacterObj and the Image corresponding to it
                return (talkingCharacter, characterImage);
            }
        };

        // ChangeImage for first character
        if (parameters[0] == "None") // None != NONE (None means no first character image)
        {
            characterImage.gameObject.SetActive(false);
            speakerNameText.text = " ";
        }
        else
        {
            characterImage.gameObject.SetActive(true);
            var firstChar = DoChange(parameters[0], characterImage);
            // if DoChange returns non-null, add to the speakerPairs
            if (firstChar != null)
                speakerPairs.Add(firstChar.Value); // get the value of the nullable type
            else
                Debug.LogError($"ChangeSpeaker: Could not find character with the name {parameters[0]}!");
        }

        // ChangeImage for second parameter (if it exists)
        if (parameters.Length >= 2)
        {
            // Show the second character image if it does exist
            secondCharacterImage.gameObject.SetActive(true);
            // Do the image change
            var secondChar = DoChange(parameters[1], secondCharacterImage);
            // if it isn't null, add to speakerPairs
            if (secondChar != null)
                speakerPairs.Add(secondChar.Value); // get the value of the nullable type
            else
                Debug.LogError($"ChangeSpeaker: Could not find character with the name {parameters[1]}!");
        }
        else
        {
            // Hide the second character image if it doesn't exist
            secondCharacterImage.gameObject.SetActive(false); // Hide the second character image
        }

        // After everything is done, set focus on first (or second, if first is None)
        if (characterImage.gameObject.activeInHierarchy)
        {
            SetFocus(new string[] { "FIRST" });
        }
        else if (secondCharacterImage.gameObject.activeInHierarchy)
        {
            SetFocus(new string[] { "SECOND" });
        }
    }

    private void SetFocus(string[] parameters)
    {

        // check for valid parameter count
        if (parameters.Length == 0)
        {
            Debug.LogError("\"Focus\" must be called with either the name of the character, or" +
                " one of the following: FIRST, SECOND, BOTH, NONE.");
            return;
        }

        // check the argument to see which character should be in focus
        switch (parameters[0])
        {
            case "FIRST":
                // set first character to be in focus
                characterImage.color = IN_FOCUS;
                // if second character image is active, set color to out of focus
                if (secondCharacterImage.gameObject.activeInHierarchy)
                {
                    secondCharacterImage.color = NOT_FOCUS;
                }
                // set the speaker name to first
                speakerNameText.text = GetCharacterFromImage(characterImage).characterName;
                break;
            case "SECOND":
                // if second character image is not active, then do nothing and log error
                if (!secondCharacterImage.gameObject.activeInHierarchy)
                {
                    Debug.LogError("Focus: Cannot set focus to second image if there is no second speaker");
                    return;
                }
                // set first character to be not in focus
                characterImage.color = NOT_FOCUS;
                // set second character to be in focus
                secondCharacterImage.color = IN_FOCUS;
                // set speaker name
                speakerNameText.text = GetCharacterFromImage(secondCharacterImage).characterName;
                break;
            case "BOTH":
                // set first character to be in focus
                characterImage.GetComponent<UnityEngine.UI.Image>().color = IN_FOCUS;
                // set speaker name to first character name
                speakerNameText.text = GetCharacterFromImage(characterImage).characterName;
                // if second character image is active, then set it to be in focus
                if (secondCharacterImage.gameObject.activeInHierarchy)
                {
                    // set speaker name to "Both" (override previous text)
                    speakerNameText.text = "Both";
                    secondCharacterImage.color = IN_FOCUS;
                }
                break;
            case "NONE":
                // set first character to not be in focus
                characterImage.color = NOT_FOCUS;
                // if second character image is active, set color to out of focus
                if (secondCharacterImage.gameObject.activeInHierarchy)
                {
                    secondCharacterImage.color = NOT_FOCUS;
                }
                // set speaker name to have no text
                speakerNameText.text = "";
                break;
            default: // When a name is provided
                // just reuse SetFocus to unfocus everything
                string[] noneParams = new string[1];
                noneParams[0] = "NONE";
                SetFocus(noneParams);
                // then, set focus based on parameters[0] (which should be a name)
                var characterImagePair = GetCharacterImagePairFromName(parameters[0]);
                if (!characterImagePair.HasValue) // error, where provided name was not found
                {
                    Debug.LogError($"Could not set focus to {parameters[0]}. Name was not found.");
                    break;
                }
                // set focus to be in focus
                characterImagePair.Value.Item2.color = IN_FOCUS;
                // set the speaker name to use the character name
                speakerNameText.text = characterImagePair.Value.Item1.characterName;
                break;
        }
    }
    
    // Using the image, get the corresponding CharacterObj
    private CharacterObj GetCharacterFromImage(Image image)
    {
        foreach ((CharacterObj, Image) pair in speakerPairs)
        {
            if (image == pair.Item2)
            {
                return pair.Item1;
            }
        }
        return null;
    }

    // Using the CharacterObj, get the corresponding character
    private Image GetImageFromCharacter(CharacterObj character)
    {
        foreach ((CharacterObj, Image) pair in speakerPairs)
        {
            if (character == pair.Item1)
            {
                return pair.Item2;
            }
        }
        return null;
    }

    // Using a name (as string), get the corresponding Character-Image pair
    private (CharacterObj, Image)? GetCharacterImagePairFromName(string name)
    {
        foreach ((CharacterObj, Image) pair in speakerPairs)
        {
            if (name == pair.Item1.characterName)
            {
                return pair;
            }
        }
        return null;
    }

    /// <summary>
    /// Sets the expression of the current focused speaker, or a specified speaker. This can take either
    /// one or two parameters.
    /// </summary>
    /// <param name="parameters">The expression, or FIRST/SECOND/character_name and the expression.</param>
    public void SetExpression(string[] parameters)
    {
        // validate parameters to be greater than 0
        if (parameters.Length == 0)
        {
            Debug.LogError("ChangeSpeaker.SetExpression: " +
                "Invalid number of parameters");
        }
        else if (parameters.Length > 2)
        {
            Debug.LogWarning("ChangeSpeaker.SetExpression: " +
                "Too many parameters were expected. The last few parameters will be ignored.");
        }

        // case 1: change the expression of the speaker in focus
        bool changeFocusedSpeakerExpression = parameters.Length == 1;
        if (changeFocusedSpeakerExpression)
        {
            // define some strings from parameters
            string expression = parameters[0];

            // update the first image's sprite and save its result
            bool firstCharacterImageChanged = UpdateImageSpriteExpression(characterImage, expression, true);

            // update the second image's sprite, but also log a warning if the expression was updated for both characters
            if (UpdateImageSpriteExpression(secondCharacterImage, expression, true) && firstCharacterImageChanged)
            {
                Debug.LogWarning("ChangeSpeaker.SetExpression: " +
                    "Both characters had their expressions updated. If this was not expected, this YarnCommand should " +
                    "be called with the character specified.");
            }
        }
        // case 2: change the expression of the specified speaker (does not have to be in focus)
        else
        {
            // define some strings from parameters
            string name = parameters[0];
            string expression = parameters[1];

            switch (name)
            {
                case "FIRST":
                    UpdateImageSpriteExpression(characterImage, expression);
                    break;

                case "SECOND":
                    UpdateImageSpriteExpression(secondCharacterImage, expression);
                    break;

                // this is if a name is specified
                default:
                    // get both the Image and CharacterObj from the specified name
                    var targetCharacterImagePair = GetCharacterImagePairFromName(name).Value;

                    // extract the image from the pair
                    Image targetImage = targetCharacterImagePair.Item2;

                    // update that image
                    UpdateImageSpriteExpression(targetImage, expression);

                    break;
            }
        }
    }

    /// <summary>
    /// Updates the specified Image to show a specified Sprite. There is a flag that can
    /// be used to indicate whether to only do an update if the sprite is in focus or not.
    /// <br></br>
    /// <br></br>
    /// It returns true if successfully changed.
    /// </summary>
    /// <param name="imageToUpdate">The Image object to update</param>
    /// <param name="expressionToChangeTo">The target expression to update to</param>
    /// <param name="inFocusOnly">Whether to only do this if the sprite is in focus</param>
    /// <returns>boolean indicating success</returns>
    private bool UpdateImageSpriteExpression(Image imageToUpdate, string expressionToChangeTo, bool inFocusOnly = false)
    {
        // failure condition: if the image itself is null, or not active and enabled
        if (imageToUpdate == null || !imageToUpdate.isActiveAndEnabled)
        {
            return false;
        }

        // failure condition: if we are trying to update an image that is only in focus, and the image is not in focus
        if (inFocusOnly && imageToUpdate.color == NOT_FOCUS)
        {
            return false;
        }

        // get the image's related CharacterObj
        CharacterObj character = GetCharacterFromImage(imageToUpdate);

        // failure condition: character is null
        if (character == null)
        {
            return false;
        }

        // get the sprite of the requested expression
        Sprite expressionSprite = character.GetExpression(expressionToChangeTo);

        // update the image
        imageToUpdate.sprite = expressionSprite;

        // return true for success
        return true;
    }
}
