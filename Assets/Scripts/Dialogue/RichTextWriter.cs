using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// This is a class that is primarily meant for rendering Rich Text in dialogue-rich games.
/// If the game engine supports Rich Text, it may accidentally render the raw tags which should
/// not be the case. RichTextWriter is meant to fix this issue.
/// <br></br>
/// <br></br>
/// To use, just construct a RichTextWriter, provide some text via the <seealso cref="ReceiveText(string)"/>
/// function, run the <seealso cref="ParseText"/> function, and then use the <seealso cref="parsedString"/>
/// field and iterate through it to see the text generation character-by-character.
/// </summary>
/// 
/// Written by Emmanuel Butor (github.com/emman-b)
/// 
/// MIT License 
/// Copyright(c) 2021 Emmanuel Butor
/// 
public class RichTextWriter
{
	// TODO: Support nested tags. See below for possible implementation.
	//	- instead of having (string, string) tag-content pairs, be able to support multiple tags
	//		(possible ways: (string[], string) or (List<string>, string) )
	//	- then, be able to handle it when writing the text in the ProcessStrings() function



	// === Fields ===
	private string fullText;
	public List<string> parsedString { get; private set; }

	// === Constructors ===
	public RichTextWriter()
	{
		parsedString = new List<string>();
	}

	// === Methods ===

	/// <summary>
	/// Updates the fullText that is going to be processed.
	/// </summary>
	/// <param name="text"></param>
	public void ReceiveText(string text)
	{
		fullText = text;
	}

	/// <summary>
	/// It parses the fullText (set in <seealso cref="ReceiveText(string)"/>) and converts it into
	/// a List of strings, where each element represents the full text being written character-by-character.
	/// </summary>
	public void ParseText()
	{
		// first clear parsedString
		parsedString.Clear();

		// then start parsing

		// first do a split on all tags
		List<(string, string)> unprocessedSplit = SplitStringByTags(fullText, "b", "i", "color=red");

		// then iterate through the strings in the unprocessedSplit and process them
		StringBuilder fullStringBuilder = new StringBuilder();
		ProcessStrings(ref unprocessedSplit, ref fullStringBuilder);
	}

	/// <summary>
	/// This processes the List of string pairs (tag, content) and updates the parsedString field
	/// to have the correct character-by-character representation of Rich Text.
	/// </summary>
	/// <param name="unprocessedSplit">List of unprocessed tag-content pairs</param>
	/// <param name="fullStringBuilder">StringBuilder used when building the string character-by-character.</param>
	private void ProcessStrings(ref List<(string, string)> unprocessedSplit, ref StringBuilder fullStringBuilder)
	{
		foreach ((string, string) pair in unprocessedSplit)
		{
			// extract the tag and content
			string tag = pair.Item1;
			string content = pair.Item2;

			// if the tag is null, we can just add the content
			if (tag == null)
			{
				foreach (char c in content)
				{
					// add one character
					fullStringBuilder.Append(c);
					// push it to parsedString
					parsedString.Add(fullStringBuilder.ToString());
				}
			}
			// if not, then we have to do more by wrapping new characters with the corresponding tag
			else
			{
				// create strings for open and close tag
				string openTag = string.Format("<{0}>", tag);
				// close tag should not contain the full "=...", so split on "=" and take the first thing
				string closeTag = string.Format("</{0}>", tag.Split('=')[0].Trim());

				// have another SB for tagged content (character-by-character for content only)
				StringBuilder taggedContent = new StringBuilder();

				// loop through the content
				foreach (char c in content)
				{
					// add one character to tagged content ONLY
					taggedContent.Append(c);

					// add the taggedContent string wrapped in the corresponding tags to the parsedString
					//	with the following format:
					//	fullStringBuilder + open tag + taggedContent + closed tag
					parsedString.Add(string.Format("{0}{1}{2}{3}",
						fullStringBuilder.ToString(),
						openTag,
						taggedContent.ToString(),
						closeTag
						)
					);
				}

				// then at the end, we append to the full string builder what is currently in taggedContent
				//	we should be the full content wrapped in open and closing tags
				fullStringBuilder.Append(openTag + taggedContent.ToString() + closeTag);
			}
		}
	}

	/// <summary>
	/// Splits a string by tags, returning a List of string pairs. The first item represents the
	/// corresponding tag of the string, and the second item represents the content that the tag
	/// is being applied to.
	/// </summary>
	/// <param name="content">The full content that is going to be checked.</param>
	/// <param name="requestedTag">The tag that is being searched for.</param>
	/// <returns>List of tag-content pairs (both are strings)</returns>
	// Returns a list of string pairs, where the first item is the tag and the second item is the content
	public List<(string, string)> SplitStringByTag(string content, string requestedTag)
	{
		// tagless output
		List<string> taglessOutput = new List<string>();

		// openTagSeparator takes the tag and wraps it in angle brackets
		string openTagSeparator = string.Format("<{0}>", requestedTag);
		// closeTagSeparators have two separators:
		//	one that takes the entire requested tag and wraps it in angle brackets and a slash
		//	and another that does the same wrapping as above, but the requested tag is up until
		//		the first equals sign
		string[] closeTagSeparators = new string[] {
				string.Format("</{0}>", requestedTag),
				string.Format("</{0}>", requestedTag.Split('=')[0].Trim())
			};

		// first split the string by the open tags
		string[] openTagSplitArray = content.Split(new string[] { openTagSeparator }, StringSplitOptions.None);
		foreach (string firstStrSplit in openTagSplitArray)
		{
			// then split the string by the close tags
			string[] closeTagSplitArray = firstStrSplit.Split(closeTagSeparators, StringSplitOptions.None);
			foreach (string secondStrSplit in closeTagSplitArray)
			{
				// add it to parsedString
				taglessOutput.Add(secondStrSplit);
			}
		}

		// in theory, every other index (odd indices) in the parsedString will be something that was enclosed in tags
		//	so store the full output as a pair of strings (tag, content)
		List<(string, string)> result = new List<(string, string)>();
		for (int i = 0; i < taglessOutput.Count; i++)
		{
			// null tag = no tag is applied
			string tag = null;

			// on odd indices, the tag is applied
			if (i % 2 == 1)
			{
				tag = requestedTag;
			}

			// add it to the result list
			result.Add((tag, taglessOutput[i]));
		}

		return result;
	}

	/// <summary>
	/// Wraps the <seealso cref="SplitStringByTag(string, string)"/> function, but now allows for handling
	/// multiple tags.
	/// </summary>
	/// <param name="content">The full content that is going to be checked.</param>
	/// <param name="tags">An array of tags to split by</param>
	/// <returns>List of tag-content pairs (both are strings)</returns>
	public List<(string, string)> SplitStringByTags(string content, params string[] tags)
	{
		// first, the function will split by the first tag
		List<(string, string)> result = SplitStringByTag(content, tags[0]);

		// then iterate through the other tags
		foreach (string tag in tags)
		{
			// ignore the first tag
			if (tag == tags[0]) continue;

			// loop through the result string pairs
			for (int i = 0; i < result.Count; i++)
			{
				// get the pair
				(string, string) pair = result[i];

				// extract the content
				string pairContent = pair.Item2;

				// check to see if the content of the pair (item2) contains any other tags
				if (IsTagInString(pairContent, tag))
				{
					// if so, then split that
					List<(string, string)> unprocessedItems = SplitStringByTag(pairContent, tag);

					// then remove the original unprocessed pair
					result.Remove(pair);

					// then insert the unprocessed items into the list
					result.InsertRange(i, unprocessedItems);
				}
			}
		}

		return result;
	}

	/// <summary>
	/// Prints the parsedString field.
	/// </summary>
	public void PrintParsedString()
	{
		StringBuilder output = new StringBuilder();
		output.Append("[");
		for (int i = 0; i < parsedString.Count; i++)
		{
			output.Append(string.Format(" \"{0}\" ", parsedString[i]));
		}
		output.Append("]");
		Console.WriteLine(output.ToString());
	}

	/// <summary>
	/// Prints the parsedString field, but each element is separated by newline.
	/// </summary>
	public void PrintParsedStringByLine()
	{
		for (int i = 0; i < parsedString.Count; i++)
		{
			Console.WriteLine("\"" + parsedString[i] + "\"");
		}
	}

	/// <summary>
	/// Checks for a tag existing in a string.
	/// </summary>
	/// <param name="content">The string to search through.</param>
	/// <param name="tag">The tag to find.</param>
	/// <returns>boolean result of whether it was found (true) or not (false0</returns>
	public static bool IsTagInString(string content, string tag)
	{
		// this is for the closing tag for tags that can be something like <color=red></color>
		//	where tag = "color=red"
		string closeTagString = tag.Split('=')[0].Trim();

		// construct opening and closing tags
		string openTag = string.Format("<{0}>", tag);
		string closeTag = string.Format("</{0}>", closeTagString);

		// check the content for existence of these tags
		if (content.Contains(openTag) && content.Contains(closeTag))
		{
			return true;
		}
		return false;
	}

	/// <summary>
	/// Main Function, to be used for testing.
	/// </summary>
	public static void Main(string[] args)
	{
		RichTextWriter richTextWriter = new RichTextWriter();
		string inputString = "<b>I</b> need to find <b>coffee beans</b> and <i>I guess</i> a <b>filter</b> and then <color=red>use</color> them in a <i>coffee machine</i>, right?";
		Console.WriteLine(inputString);
		richTextWriter.ReceiveText(inputString);
		richTextWriter.ParseText();
		richTextWriter.PrintParsedStringByLine();
	}
}
