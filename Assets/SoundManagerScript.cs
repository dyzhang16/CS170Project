﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManagerScript : MonoBehaviour { 
    public static AudioClip pickFlower;
    public static AudioClip flowerSuccess;
    public static AudioClip wallBump;
    public static AudioClip click;
    public static AudioClip playerTalk;
    public static AudioClip pourCoffee;
    public static AudioClip plopSound;
    public static AudioClip placeCup;
    public static AudioClip placeFilter;
    public static AudioClip placeCoffee;
    public static AudioClip openGate;
    public static AudioClip whistle;
    public static AudioClip placeFlower;
    public static AudioClip dialogueSound;



    static AudioSource audioSrc;


    // Start is called before the first frame update

    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        Debug.Log(sceneName);


        audioSrc = GetComponent<AudioSource>();
    }
    void Awake()
    {
        //moved to awake in an attempt to fix webgl bugs
        pickFlower = Resources.Load<AudioClip>("pickup_flower_2");
        flowerSuccess = Resources.Load<AudioClip>("flower_success");
        wallBump = Resources.Load<AudioClip>("wallbump_1");
        playerTalk = Resources.Load<AudioClip>("player_talk");
        pourCoffee = Resources.Load<AudioClip>("pour_coffee");
        plopSound = Resources.Load<AudioClip>("plop");
        placeCup = Resources.Load<AudioClip>("place_cup");
        placeFilter = Resources.Load<AudioClip>("place_filter");
        placeCoffee = Resources.Load<AudioClip>("coffee_sound");
        openGate = Resources.Load<AudioClip>("open_gate");
        whistle = Resources.Load<AudioClip>("whistling_1");
        placeFlower = Resources.Load<AudioClip>("place_flower");
        dialogueSound = Resources.Load<AudioClip>("dialogue_sound");
        // click = Resources.Load<AudioClip>("button_click");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void lineStop()
    {
        audioSrc.Stop();
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "pickup_flower_2":
                audioSrc.PlayOneShot(pickFlower);
                break;
            case "flower_success":
                audioSrc.PlayOneShot(flowerSuccess);
                break;
            case "wall_bump":
                audioSrc.PlayOneShot(wallBump, 0.2f);
                break;
            case "click":
                audioSrc.PlayOneShot(click);
                break;
            case "player_talk":
                audioSrc.PlayOneShot(playerTalk);
                break;
            case "pour_coffee":
                audioSrc.PlayOneShot(pourCoffee);
                break;
            case "plop":
                audioSrc.PlayOneShot(plopSound);
                break;
            case "place_cup":
                audioSrc.PlayOneShot(placeCup);
                break;
            case "place_filter":
                audioSrc.PlayOneShot(placeFilter);
                break;
            case "place_coffee":
                audioSrc.PlayOneShot(placeCoffee);
                break;
            case "open_gate":
                audioSrc.PlayOneShot(openGate);
                break;
            case "whistle":
                audioSrc.PlayOneShot(whistle);
                break;
            case "place_flower":
                audioSrc.PlayOneShot(placeFlower);
                break;
            case "dialogue_sound":
                audioSrc.PlayOneShot(dialogueSound);
                break;



        }

    }

    public void diaSoundStart()
    {
        audioSrc.PlayOneShot(dialogueSound, 0.1f);
       
    }

    public void MainVolumeControl(float vol)
    {   // currently not working
        //Debug.Log("vol is: " + vol);
        audioSrc.volume = vol+'f';
    }
}
