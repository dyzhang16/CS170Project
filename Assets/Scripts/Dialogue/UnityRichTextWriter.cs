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

	// sets the text for the RichTextWriter
	public void SetText(string text)
	{
		// set the RichTextWriter up
		writer.ReceiveText(text);
		writer.ParseText();

		// reset index
		index = 0;
	}

	// displays the next character
	public void ShowNextCharacter(string request)
	{
		// if the writer has no text, do default behavior
		if (writer.parsedString.Count <= 0)
		{
			dialogueText.text = request;
		}
		// if Text is broken, don't run the function
		if (dialogueText == null) return;
		// if index is less than parsedString, then use the parsedString for the dialogue text
		if (index < writer.parsedString.Count)
		{
			// update the dialogue text
			dialogueText.text
				= writer.parsedString[index];

			// move the index up
			index++;
		}
		// otherwise, use the last item in the parsedString
		else if (writer.parsedString.Count > 0)
		{
			dialogueText.text = writer.parsedString[writer.parsedString.Count - 1];

			// also invoke the on line finished updating 
			DialogueUI dialogueUI = GameObject.Find("DialogueRunner").GetComponent<DialogueUI>();
			dialogueUI.MarkLineComplete();
		}
	}

	// finish displaying text
	public void FinishDisplayingText()
	{

	}
}
