using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class OfficeFriend : MonoBehaviour
{
    //positions that the NPC will move to;
    public GameObject Destination1, Destination2, Destination3, Door, Exit;

    [HideInInspector]public bool isWalking = false;

    public float Speed;
    //changes direction and indicates which movement an npc needs to walk towards
    private int cycle = 0;

    [YarnCommand("Move")]
    public void Move()
    {
        isWalking = true;
    }
    [YarnCommand("Leave")]
    public void Leave()
    {
        cycle = 4;
        isWalking = true;
    }
    void Update()
    {
        if (isWalking)
        {
            //disable dialogue while NPC is moving
            this.GetComponent<RunDialogue>().enabled = false;
            //distance traveled per frame
            float step = Speed * Time.deltaTime;

            //first destination
            if (cycle == 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
                transform.position = Vector3.MoveTowards(transform.position, Destination1.transform.position, step);

                if (Vector3.Distance(transform.position, Destination1.transform.position) < 0.001f)
                {
                    isWalking = false;
                    StartCoroutine(changeDirection());
                }
            }
            //second destination
            else if (cycle == 1)
            {
                GetComponent<SpriteRenderer>().flipX = false;
                transform.position = Vector3.MoveTowards(transform.position, Destination2.transform.position, step);

                if (Vector3.Distance(transform.position, Destination2.transform.position) < 0.001f)
                {
                    isWalking = false;
                    StartCoroutine(changeDirection());
                }
            }
            //3rd destination
            else if (cycle == 2)
            {
                GetComponent<SpriteRenderer>().flipX = true;
                transform.position = Vector3.MoveTowards(transform.position, Destination3.transform.position, step);

                if (Vector3.Distance(transform.position, Destination3.transform.position) < 0.001f)
                {
                    isWalking = false;
                    StartCoroutine(changeDirection());
                }
            }
            //Moving Fredric to the door after exiting desk
            else if (cycle == 4)
            {
                transform.position = Vector3.MoveTowards(transform.position, Destination2.transform.position, step);
                if (Vector3.Distance(transform.position, Destination2.transform.position) < 0.001f)
                {
                    isWalking = false;
                    StartCoroutine(changeDirection());
                }
            }
            else if (cycle == 5)
            {
                transform.position = Vector3.MoveTowards(transform.position, Destination1.transform.position, step);
                if (Vector3.Distance(transform.position, Destination1.transform.position) < 0.001f)
                {
                    isWalking = false;
                    StartCoroutine(changeDirection());
                }
            }
            else if (cycle == 6)
            {
                transform.position = Vector3.MoveTowards(transform.position, Exit.transform.position, step);
                if (Vector3.Distance(transform.position, Exit.transform.position) < 20f)
                {
                    Door.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 0f);
                }
                if (Vector3.Distance(transform.position, Exit.transform.position) < 0.001f)
                {
                    isWalking = false;
                    StartCoroutine(changeDirection());
                }
            }
        }
        else
        {
            //enable dialogue while NPC is standing
            this.GetComponent<RunDialogue>().enabled = true;
            
        }
    }


    IEnumerator changeDirection()
    {
        if (cycle == 0)
        {
            isWalking = true;
        }
        else if (cycle == 1)
        {
            isWalking = true;
        }
        else if (cycle == 2)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            yield return new WaitForSeconds(0.5f);
        }
        else if (cycle == 4)
        {
            isWalking = true;
        }
        else if (cycle == 5)
        {
            isWalking = true;
        }
        else if (cycle == 6)
        {
            yield return new WaitForSeconds(0.5f);
            Door.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1);
            gameObject.SetActive(false);
        }
        ++cycle;
        //Debug.Log(cycle);
    }
}
