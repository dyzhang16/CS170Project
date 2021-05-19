using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Coffee : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    public VariableStorageBehaviour CustomVariableStorage;
    public NodeVisitedTracker nodeVisitedTracker;
    public GameObject CoffeeMachine, lDoor, mDoor, rDoor, recipeBook;


    void Awake()
    {
        if (GameManager.instance != null)
        {
            if(GameManager.instance.visitedCoffee == 1)
            {
                dialogueRunner.startAutomatically = false;
            } else if (GameManager.instance.wrongCoffee == 1){

                if (GameManager.instance.hasReceipt == 0){
                    dialogueRunner.startNode = "wrong_coffee_without_receipt";
                } else {
                    dialogueRunner.startNode = "wrong_coffee_with_receipt";
                }

                dialogueRunner.startAutomatically = true;
            } else if (GameManager.instance.wrongCoffee == 2){
                dialogueRunner.startNode = "no_lid_or_sleeve";
                dialogueRunner.startAutomatically = true;
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
            
        if (MusicManagerScript.instance != null){
            MusicManagerScript.instance.sceneChecker();
        }
    }
    void Start()
    {
        if (GameManager.instance != null)
        {
            if (GameManager.instance.clearInventory){
                GameManager.instance.deleteItems();
                GameManager.instance.clearItems();
                GameManager.instance.clearInventory = false;
            } else {
                GameManager.instance.loadItems();
                GameManager.instance.deleteItems();
            }
        }
        if (GameManager.instance.readBook == 1)
        {
            recipeBook.GetComponent<RunDialogueMC>().dialogueToRun = "ShowRecipeBook";
        }

    }
}
