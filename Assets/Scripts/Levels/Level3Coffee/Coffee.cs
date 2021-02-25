using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Coffee : MonoBehaviour
{
    public DialogueRunner dialogueRunner;

    public GameObject blender;


    void Awake()
    {
        if (GameManager.instance != null)
        {
            if (GameManager.instance.blender == 1)
            {
                Destroy(blender);
            }
            if(GameManager.instance.visitedCoffee == 1)
            {
                dialogueRunner.startAutomatically = false;
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
