﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement; // for getting the current scene

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

    // Fields (when player gets hit)
    private bool isFriendHit = false;
    private readonly float deadRotation = 90f;
    private readonly float flattenFactor = 2f;

    void Start(){
        if (GameManager.instance != null){
            if (GameManager.instance.officeDeskPuzzle == 1){
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
                //col.isTrigger = false;
                if (GameManager.instance.firstDateDia == 1){
                    this.GetComponent<RunDialogue>().dialogueToRun = "friendHitByCar";
                } else if (GameManager.instance.firstFriendMeeting == 5){
                    this.GetComponent<SpriteRenderer>().flipX = true;
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
        // this.GetComponent<SpriteRenderer>().flipX = true;
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
        // this.GetComponent<SpriteRenderer>().flipX = true;
    }

    //cityOffice
    [YarnCommand("MoveToOffice3")]
    public void MoveToOffice3(){
        isWalking = true;
        col.isTrigger = true;
        GameManager.instance.firstFriendMeeting = 4;
        // this.GetComponent<SpriteRenderer>().flipX = true;
    }

    [YarnCommand("MoveToOffice4")]
    public void MoveToOffice4(){
        isWalking = true;
        col.isTrigger = true;
        GameManager.instance.firstFriendMeeting = 5;
    }

    [YarnCommand("moveToStreet")]
    public void moveToStreet(){
        isWalking = true;
        col.isTrigger = true;
        GameManager.instance.walkToStreet = 1;
        this.GetComponent<SpriteRenderer>().flipX = true;
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
        // this.GetComponent<SpriteRenderer>().flipX = false;

        StartCoroutine(stopCar());
    }

    IEnumerator stopCar(){
        yield return new WaitForSeconds(4);
        car.SetActive(false);

        Dialoguerunner.GetComponent<DialogueRunner>().StartDialogue("endingCutscene");
    }

    // Detect collision between friend1 and car
    private void OnTriggerEnter(Collider other)
    {
        if (!isFriendHit)
        {
            car car = other.GetComponentInParent<car>();
            if (car != null)
            {
                // rotate the friend to be fallen over
                transform.Rotate(new Vector3(0, 0, -deadRotation));

                // flatten friend too just for fun
                transform.localScale = new Vector3(transform.localScale.x / flattenFactor, transform.localScale.y, transform.localScale.z);

                // prevent additional transform modifications (related to the car, at least)
                isFriendHit = true;
            }
        }
    }

    // Turn the friend into a ghost
    [YarnCommand("GhostifyFriend")]
    public void GhostifyFriend()
    {
        // First, return fredric's original rotation and scale
        transform.Rotate(new Vector3(0, 0, deadRotation)); // reverse the rotation
        // update the scale to be back to what it was before
        transform.localScale = new Vector3(transform.localScale.x * flattenFactor, transform.localScale.y, transform.localScale.z);

        // PUT THE GHOSTIFY-ING STUFF HERE

        // Temporary: just make him transparent
        Color friendColor = GetComponent<SpriteRenderer>().color;
        friendColor = new Color(friendColor.r, friendColor.g, friendColor.b, friendColor.a * 0.5f);
        GetComponent<SpriteRenderer>().color = friendColor;
    }

    // Run the run-into-friend cutscene that is seen in the Park scene
    [YarnCommand("DoRunIntoFriendCutscene")]
    public void RunIntoFriendCutscene()
    {
        // Make sure that the current scene is the Park scene before doing anything
        if (SceneManager.GetActiveScene().name == "Park")
        {
            // then, disable the friend's boxcollider component, which will be reenabled in RunToPosition
            GetComponentInChildren<BoxCollider>().enabled = false;

            // Then relocate the friend to be to the left of the player
            transform.position = new Vector3(player.transform.position.x - 100, player.transform.position.y, player.transform.position.z);

            // flip the sprite to make it face the right
            GetComponent<SpriteRenderer>().flipX = true;

            // then start a coroutine that makes friend keep running to the right for a long distance
            Vector3 destination = new Vector3(transform.position.x + 600, transform.position.y, transform.position.z);
            StartCoroutine(RunToPosition(destination, "RunIntoFriendCutscene"));
		}
	}

    // helper function that makes friend run to a specified Vector3 destination
    private IEnumerator RunToPosition(Vector3 destination, string caller = "")
	{
        while (transform.position != destination)
		{
            transform.position = Vector3.MoveTowards(transform.position, destination, 50 * Time.deltaTime);

            yield return null;
		}

        // this is specific to the RunIntoFriendCutscene. the friend will disable itself here
        if (caller == "RunIntoFriendCutscene")
		{
            gameObject.SetActive(false);
        }
    }
}
