using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class OfficeGuard : MonoBehaviour
{
    //positions that the NPC will move to;
    public GameObject Destination1;
    public GameObject Destination2;
    public GameObject Destination3;
    public GameObject Destination4;
    public GameObject Destination5;
    public GameObject Destination6;
    public Collider West;
    public Collider East;
    public Collider South;
    public Collider North;
    public bool isWalking = false;

    public float Speed;
    //changes direction and indicates which movement an npc needs to walk towards
    private int cycle = 0;

    public DialogueRunner dialogueRunner;
    [HideInInspector] public Vector3 resetGuardPosition;
    [HideInInspector] public Vector3 resetPlayerPosition;

    private void Awake()
    {
        West.enabled = false;
        East.enabled = false;
        North.enabled = false;
        South.enabled = false;
        isWalking = true;
        resetGuardPosition = this.transform.position;
        dialogueRunner.AddCommandHandler(
        "ResetPosition",     // the name of the command
        CaughtPlayer // the method to run
        );

    }
    private void CaughtPlayer(string[] parameters)
    {
        string Player = parameters[0];
        GameObject player = GameObject.Find(Player);
        isWalking = true;
        this.transform.position = player.transform.position;
        player.transform.position = resetPlayerPosition;
        
    }

    void Update()
    {
        AutoPath();
    }

    //function that controls the path for the character attached with this script
    private void AutoPath() 
    {
        if (isWalking)
        {
            //distance traveled per frame
            float step = Speed * Time.deltaTime;

            //Walks South
            if (cycle == 0)
            {
                South.enabled = true;
                transform.position = Vector3.MoveTowards(transform.position, Destination1.transform.position, step);
                if (Vector3.Distance(transform.position, Destination1.transform.position) < 0.001f)
                {
                    isWalking = false;
                    South.enabled = false;
                    StartCoroutine(changeDirection());
                }
            }
            //Walking West
            else if (cycle == 1)
            {
                West.enabled = true;
                transform.position = Vector3.MoveTowards(transform.position, Destination2.transform.position, step);
                if (Vector3.Distance(transform.position, Destination2.transform.position) < 0.001f)
                {
                    isWalking = false;
                    West.enabled = false;
                    StartCoroutine(changeDirection());
                }
            }
            //Walking North
            else if (cycle == 2)
            {
                North.enabled = true;
                GetComponent<SpriteRenderer>().flipX = true;
                transform.position = Vector3.MoveTowards(transform.position, Destination3.transform.position, step);
                if (Vector3.Distance(transform.position, Destination3.transform.position) < 0.001f)
                {
                    isWalking = false;
                    North.enabled = false;
                    StartCoroutine(changeDirection());
                }
            }
            //Walking East
            else if (cycle == 3)
            {
                East.enabled = true;
                transform.position = Vector3.MoveTowards(transform.position, Destination4.transform.position, step);
                if (Vector3.Distance(transform.position, Destination4.transform.position) < 0.001f)
                {
                    isWalking = false;
                    East.enabled = false;
                    StartCoroutine(changeDirection());
                }
            }
            //Walking North
            else if (cycle == 4)
            {
                North.enabled = true;
                GetComponent<SpriteRenderer>().flipX = false;
                transform.position = Vector3.MoveTowards(transform.position, Destination5.transform.position, step);
                if (Vector3.Distance(transform.position, Destination5.transform.position) < 0.001f)
                {
                    isWalking = false;
                    North.enabled = false;
                    StartCoroutine(changeDirection());
                }
            }
            //Walking West
            else if (cycle == 5)
            {
                West.enabled = true;
                transform.position = Vector3.MoveTowards(transform.position, Destination6.transform.position, step);
                if (Vector3.Distance(transform.position, Destination6.transform.position) < 0.001f)
                {
                    isWalking = false;
                    West.enabled = false;
                    StartCoroutine(changeDirection());
                }
            }
        }
    }
    IEnumerator changeDirection()
    {
        if (cycle == 0)
        {
            isWalking = true;
            cycle = 1;
        }
        else if (cycle == 1)
        {
            isWalking = true;
            cycle = 2;
        }
        else if (cycle == 2)
        {
            isWalking = true;
            cycle = 3;
        }
        else if (cycle == 3)
        {
            isWalking = true;
            cycle = 4;
        }
        else if (cycle == 4)
        {
            isWalking = true;
            cycle = 5;
        }
        else if (cycle == 5)
        {
            isWalking = true;
            South.enabled = true;
            cycle = 0;
            yield return new WaitForSeconds(0.5f);
        }

        //Debug.Log(cycle);
    }
}
