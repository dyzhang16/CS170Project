using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Character", menuName = "Characters/Character")]
public class CharacterObj : ScriptableObject
{
	public string characterName;
	[Tooltip("Also used as the neutral expression")]
	public Sprite defaultSprite; // also known as neutral

	// More expressions that can possibly be used, but will fallback to default sprite if needed.
	//		If new expressions are added, place them here and update GetExpression to handle it.
	public Sprite angrySprite;
	public Sprite happySprite;
	public Sprite hmmSprite;
	public Sprite shockedSprite;

	// Retrieves the requested expression (or the default sprite, if not set)
	public Sprite GetExpression(string expression)
	{
		// switch-case with the name but in lowercase to prevent possible issues
		switch (expression.ToLower())
		{
			case "angry":
			case "mad":
				return VerifySprite(angrySprite);

			case "happy":
				return VerifySprite(happySprite);

			case "hmm":
				return VerifySprite(hmmSprite);

			case "shock":
			case "shocked":
				return VerifySprite(shockedSprite);

			// defaultSprite will be used if these keywords are specified, or if the requested expression is unavailable
			case "default":
			case "neutral":
			default:
				return defaultSprite;
		}
	}

	// Helper function that checks if the sprite is null. It will either return itself, or the default sprite
	private Sprite VerifySprite(Sprite sprite)
	{
		// return defaultSprite if the requested sprite is null
		if (sprite == null)
		{
			return defaultSprite;
		}
		// otherwise, return the sprite
		else
		{
			return sprite;
		}
	}
}
