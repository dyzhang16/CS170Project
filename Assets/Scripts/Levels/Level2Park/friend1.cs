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

    public BoxCollider collider;

    private Vector3 target;

    void Start(){
        if (GameManager.instance.officePuzzle == 1){
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
                } else if (GameManager.instance.walkToArcade == 1){
                    this.GetComponent<RunDialogue>().dialogueToRun = "friendGoingToArcade";
                }
            }
        }
    }

    //streetCoffee
    [YarnCommand("MoveToOffice")]
    public void MoveToOffice(){
        isWalking = true;
        collider.isTrigger = true;
        GameManager.instance.visitedAfterCoffee = 1;
        GameManager.instance.firstFriendMeeting = 2;
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
        collider.isTrigger = true;
        GameManager.instance.firstFriendMeeting = 3;
    }

    //cityOffice
    [YarnCommand("MoveToOffice3")]
    public void MoveToOffice3(){
        isWalking = true;
        collider.isTrigger = true;
        GameManager.instance.firstFriendMeeting = 4;
    }

    [YarnCommand("MoveToApartment1")]
    public void moveToApartment1(){
        isWalking = true;
        collider.isTrigger = true;
        GameManager.instance.walkedToApartment = 1;
    }

    [YarnCommand("MoveToApartment2")]
    public void moveToApartment2(){
        isWalking = true;
        collider.isTrigger = true;
        GameManager.instance.walkedToApartment = 2;
    }

    [YarnCommand("moveToArcade")]
    public void moveToArcade(){
        isWalking = true;
        collider.isTrigger = true;
        GameManager.instance.walkToArcade = 1;
    }

    [YarnCommand("Enter")]
    public void Enter(){
        this.gameObject.SetActive(false);
    }
}
