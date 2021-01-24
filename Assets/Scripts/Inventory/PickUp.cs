﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Yarn.Unity;

public class PickUp : MonoBehaviour
{
    public Item item;
    Inventory inventory;
    RectTransform rectTrans;

    public GameObject player;
    private Player p;

    public bool coffee = false;
    private bool runPickUp = false;

    public DialogueRunner dialogueRunner;

    void Start()
    {
        rectTrans = transform as RectTransform;
        p = player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (runPickUp && !dialogueRunner.IsDialogueRunning){
            if (Input.GetKeyDown(KeyCode.Space)){
                Inventory.instance.AddItem(item);
                
                runPickUp = false;
                p.movement = new Vector3(0, 0, 0);
            }
        }
    }

    void OnTriggerStay(Collider collider)                          //https://www.youtube.com/watch?v=Bc9lmHjqLZc
    {
        if (collider.gameObject.CompareTag("Player") && !runPickUp)
        {
            runPickUp = true;
        }
    }

    void OnTriggerExit(Collider collider){
        if (collider.gameObject.CompareTag("Player"))
        {
            runPickUp = false;
        }
    }

    // void OnTriggerStay2D(Collider2D collider)
    // {
    //     if(Input.GetKeyDown(KeyCode.Space))
    //     {
    //         Inventory.instance.AddItem(item);
    //         Destroy(gameObject);
    //     }
    //     else if (Input.GetKey(KeyCode.Mouse0) && !p.invActive)
    //     {
    //         if (RectTransformUtility.RectangleContainsScreenPoint(rectTrans, Camera.main.ScreenToWorldPoint(Input.mousePosition))){
    //             Inventory.instance.AddItem(item);
    //             Destroy(gameObject);
    //         }
    //     }
    // }
}