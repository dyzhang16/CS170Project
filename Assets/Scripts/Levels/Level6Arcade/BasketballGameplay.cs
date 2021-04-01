using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketballGameplay : MonoBehaviour
{
	// Unity Objects
	Camera basketballCam;
	GameObject basketballCamRenderTexture;
	// Basketball-specific Unity Objects
	BasketballNet basketballNet;
	BasketballMovement basketball;

	void Start()
	{
		basketballCam = GameObject.Find("Basketball Camera").GetComponent<Camera>();
		basketballCamRenderTexture = GameObject.Find("BasketballCam Render Texture");
	}
}
