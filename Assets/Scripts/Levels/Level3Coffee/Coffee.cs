using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Coffee : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    public VariableStorageBehaviour CustomVariableStorage;
    public NodeVisitedTracker nodeVisitedTracker;
    public GameObject CoffeeMachine, lDoor, mDoor, rDoor;


    void Awake()
    {
        if (GameManager.instance != null)
        {
            if(GameManager.instance.visitedCoffee == 1)
            {
                dialogueRunner.startAutomatically = false;
            }
            if (GameManager.instance.addedCoffeeMachineItem == 1)
            {
                CoffeeMachine.GetComponent<RunDialogueMC>().dialogueToRun = "StaleIngredients";
            }

            if (GameManager.instance.openLeftDoor == 1)
            {
                lDoor.SetActive(false);
                lDoor.GetComponent<HideDoor>().itemsUnder.SetActive(true);
            }
            if (GameManager.instance.openMidDoor == 1)
            {
                mDoor.SetActive(false);
                mDoor.GetComponent<HideDoor>().itemsUnder.SetActive(true);
            }
            if (GameManager.instance.openRightDoor == 1)
            {
                rDoor.SetActive(false);
                rDoor.GetComponent<HideDoor>().itemsUnder.SetActive(true);
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
