using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class UnityRichTextWriter : MonoBehaviour
{
	// Unity Objects
	private Text dialogueText;

	// Fields
	private RichTextWriter writer = new RichTextWriter();
	private int index = 0;

	// Unity Methods
	void Start()
	{
		dialogueText = GetComponent<Text>();
	}

	// Methods

	// sets the text for the RichTextWriter (called in On Line Start)
	public void SetText(string text)
	{
		// set the RichTextWriter up
		writer.ReceiveText(text);
		writer.ParseText();

		// reset index
		index = 0;
	}

	// displays the next character (called in On Line Update)
	public void ShowNextCharacter(string request)
	{
		// if Text is broken, don't run the function
		if (dialogueText == null) return;

		// if the writer has no text, do default behavior
		if (writer.parsedString.Count <= 0)
		{
			dialogueText.text = request;
			return;
		}

		// if the request is the entire string, then just display the entire string
		if (request == writer.parsedString[writer.parsedString.Count - 1])
		{
			FinishDisplayingText();
		}
		// if index is less than parsedString, then use the parsedString for the dialogue text
		else if (index < writer.parsedString.Count)
		{
			// update the dialogue text
			dialogueText.text = writer.parsedString[index];

			// move the index up
			index++;
		}
		// otherwise, use the last item in the parsedString and indicate that the line is complete
		else if (writer.parsedString.Count > 0)
		{
			dialogueText.text = writer.parsedString[writer.parsedString.Count - 1];

			// also invoke the on line finished updating to prevent more sound from playing
			DialogueUI dialogueUI = GameObject.Find("DialogueRunner").GetComponent<DialogueUI>();
			dialogueUI.MarkLineComplete();
		}
		
	}

	// finish displaying text
	public void FinishDisplayingText()
	{
		if (writer.parsedString.Count > 0)
		{
			dialogueText.text = writer.parsedString[writer.parsedString.Count - 1];
		}
	}
}
