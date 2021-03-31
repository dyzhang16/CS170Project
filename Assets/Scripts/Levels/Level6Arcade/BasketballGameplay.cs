using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketballGameplay : MonoBehaviour
{
	Camera basketballCam;

	GameObject basketballCamRenderTexture;
	GameObject basketballGame;

	void Start()
	{
		basketballCam = GameObject.Find("Basketball Camera").GetComponent<Camera>();
		basketballCamRenderTexture = GameObject.Find("BasketballCam Render Texture");
		Debug.LogWarning("TODO: When old basketball is deprecated, rename without the underscore!!!");
		basketballGame = GameObject.Find("_BasketballGame");
	}
}
