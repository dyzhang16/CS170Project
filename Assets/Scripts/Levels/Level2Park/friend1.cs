using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class friend1 : MonoBehaviour
{

    public GameObject friend;
    public GameObject coffeeStand;
    public GameObject receipt;
    public VariableStorageBehaviour CustomVariableStorage;
    public bool isWalking = false;
    public float Speed;
    public GameObject Destination;

    public BoxCollider collider;

    private Vector3 target;

    void Awake(){
        target = Destination.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isWalking){
            float step = Speed * Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, target, step);

            if (Vector3.Distance(transform.position, target) < 0.001f){
                isWalking = false;
                this.gameObject.SetActive(false);
                GameManager.instance.visitedAfterCoffee = 1;
            }
        }
    }

    [YarnCommand("MoveToOffice")]
    public void MoveToOffice(){
        isWalking = true;
        collider.isTrigger = true;
    }

    [YarnCommand("DropReceipt")]
    public void DropReceipt(){
        receipt.SetActive(true);
        receipt.GetComponent<Animator>().SetTrigger("ReceiptFall");
        GameManager.instance.firstFriendMeeting = 1;
    }

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
}
