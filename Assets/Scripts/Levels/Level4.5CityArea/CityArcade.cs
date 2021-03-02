﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityArcade : MonoBehaviour
{
    public NodeVisitedTracker tracker;

    public GameObject player;
    public GameObject Arcade;

    public GameObject exitToCityOffice;
    public GameObject exitToStreetCoffee;

    void Awake(){
        if (GameManager.instance != null){
            if (GameManager.instance.previousScene == "StreetCoffee"){
                player.transform.position = exitToStreetCoffee.transform.position + new Vector3(10, 0, 0);
            } else if (GameManager.instance.previousScene == "CityOffice"){
                player.transform.position = exitToCityOffice.transform.position + new Vector3(-10, 0, 0);
            } else if (GameManager.instance.previousScene == "ArcadeScene"){
                player.transform.position = Arcade.transform.position + new Vector3(0, -10, 0);
            }


        }
    }

    void Start()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.loadItems();
            GameManager.instance.deleteItems();
        }
    }
}