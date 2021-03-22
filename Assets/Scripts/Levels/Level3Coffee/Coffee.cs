using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Coffee : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    public VariableStorageBehaviour CustomVariableStorage;
    public NodeVisitedTracker nodeVisitedTracker;
    public GameObject CoffeeMachine;


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
        }
    }
    void Start()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.loadItems();
            GameManager.instance.deleteItems();
        }
        if (GameManager.instance.readRecipeBook == 1)
        {
            nodeVisitedTracker.NodeComplete("RecipeBook");
        }
    }
}
