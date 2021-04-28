using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class friend1 : MonoBehaviour
{

    public GameObject coffeeStand;
    public GameObject receipt;
    public VariableStorageBehaviour CustomVariableStorage;
    public bool isWalking = false;
    public float Speed;
    public GameObject Destination;
    public GameObject DestinationAfterApartment;
    public GameObject DestinationAfterOffice1;
    public GameObject DestinationAfterOffice2;
    
    public GameObject car;
    public GameObject Dialoguerunner;
    public GameObject player;

    public BoxCollider col;

    private Vector3 target;
    private bool playerMove;

    void Start(){
        if (GameManager.instance != null){
            if (GameManager.instance.officeDeskPuzzle == 1){
                if (GameManager.instance.walkedToApartment == 0){
                    target = DestinationAfterOffice1.transform.position;
                } else if (GameManager.instance.walkedToApartment == 1){
                    target = DestinationAfterOffice2.transform.position;
                }
            } else if (GameManager.instance.firstDateDia == 1){
                target = DestinationAfterApartment.transform.position;
            } else {
                target = Destination.transform.position;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isWalking){
            float step = Speed * Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, target, step);

            if (Vector3.Distance(transform.position, target) < 0.001f){
                isWalking = false;
                if (GameManager.instance.walkedToApartment == 2){
                    this.GetComponent<RunDialogue>().dialogueToRun = "friendGoingToApartment";
                } else if (GameManager.instance.firstDateDia == 1){
                    this.GetComponent<RunDialogue>().dialogueToRun = "friendHitByCar";
                }
            }
        }

        if (playerMove){
            player.GetComponent<Player>().allowMovement = false;
        }
    }

    //streetCoffee
    [YarnCommand("MoveToOffice")]
    public void MoveToOffice(){
        isWalking = true;
        col.isTrigger = true;
        GameManager.instance.visitedAfterCoffee = 1;
        GameManager.instance.firstFriendMeeting = 2;
        this.GetComponent<SpriteRenderer>().flipX = true;
    }

    //streetCoffee
    [YarnCommand("DropReceipt")]
    public void DropReceipt(){
        receipt.SetActive(true);
        receipt.GetComponent<Animator>().SetTrigger("ReceiptFall");
        GameManager.instance.firstFriendMeeting = 1;
    }

    //streetCoffee
    [YarnCommand("CheckDrink")]
    public void CheckCompletedCoffee()
    {
        if (Inventory.instance.FindItemOfTypeDrink())
        {
            Debug.Log("Found item");
            CustomVariableStorage.SetValue("$CompletedDrinkExists", 1);
        }
        else
        {
            Debug.Log("Didnt find Item");
            CustomVariableStorage.SetValue("$CompletedDrinkExists", 0);
        }
    }

    //cityArcade
    [YarnCommand("MoveToOffice2")]
    public void MoveToOffice2(){
        isWalking = true;
        col.isTrigger = true;
        GameManager.instance.firstFriendMeeting = 3;
    }

    //cityOffice
    [YarnCommand("MoveToOffice3")]
    public void MoveToOffice3(){
        isWalking = true;
        col.isTrigger = true;
        GameManager.instance.firstFriendMeeting = 4;
    }

    [YarnCommand("MoveToApartment1")]
    public void moveToApartment1(){
        isWalking = true;
        col.isTrigger = true;
        GameManager.instance.walkedToApartment = 1;
    }

    [YarnCommand("MoveToApartment2")]
    public void moveToApartment2(){
        isWalking = true;
        col.isTrigger = true;
        GameManager.instance.walkedToApartment = 2;
    }

    [YarnCommand("moveToStreet")]
    public void moveToStreet(){
        isWalking = true;
        col.isTrigger = true;
        GameManager.instance.walkToStreet = 1;
    }

    [YarnCommand("Enter")]
    public void Enter(){
        this.gameObject.SetActive(false);
    }

    [YarnCommand("hitByCar")]
    public void hitByCar(){
        car.transform.position = this.transform.position + new Vector3(-100, 0, 0);
        car.GetComponent<car>().cycle = 2;
        playerMove = true;

        StartCoroutine(stopCar());
    }

    IEnumerator stopCar(){
        yield return new WaitForSeconds(4);
        car.SetActive(false);

        Dialoguerunner.GetComponent<DialogueRunner>().StartDialogue("endingCutscene");
    }
}
