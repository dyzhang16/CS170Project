using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Character", menuName = "Characters/Character")]
public class CharacterObj : ScriptableObject
{
	public string characterName;
	public Sprite defaultSprite;
}
