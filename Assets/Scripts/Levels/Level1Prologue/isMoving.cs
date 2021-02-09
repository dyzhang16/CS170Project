using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class isMoving : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Character;
    public GameObject Destination;
    private Vector3 target;
    public GameObject gravestone;
    public GameObject player;
    Player p;
    public GameObject key;

    public DialogueRunner dia;

    public bool isWalking;
    public bool timeToWalkBack;
    private bool alreadyWalkedBack = false;
    public float Speed;

    private Vector3 offmap = new Vector3(150, -3, -55);
    private bool left = true;

    void Start(){
        target = Destination.transform.position;
    }

    [YarnCommand("MoveNPC")]
    public void MoveNPC()
    {
        isWalking = true;
        p = player.GetComponent<Player>();
        StartCoroutine(restartText());
    }

    IEnumerator restartText(){
        yield return new WaitForSeconds(2);
        RunDialogue dia = gravestone.GetComponent<RunDialogue>();
        dia.dialogueRunner.StartDialogue("prologue_gravekeeper");
    }

    // Update is called once per frame
    void Update()
    {
        if (isWalking)
        {
            //Debug.Log("Movin");
            Vector3 distance;
            if (left){
                distance = Character.transform.position - target;
                p.AllowMove(false);
            } else {
                distance = Character.transform.position - offmap;
            }

            distance = -distance.normalized;
            Character.transform.position += distance * Speed * Time.deltaTime;

            Character.transform.Rotate(0,0,60*Time.deltaTime);

            if (distance.y >= 0 && distance.x >= 0)          //Changes based on Positioning
            {
                isWalking = false;
                StartCoroutine(doneWalking());
            }
        } else if (timeToWalkBack){
            float step = Speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, step);

            p.AllowMove(false);

            Character.transform.Rotate(0,0,60*Time.deltaTime);

            if (Vector3.Distance(transform.position, target) < 0.001f)
            {
                timeToWalkBack = false;
            }
        }
    }

    IEnumerator doneWalking(){
        if (left){
            left = false;
            yield return new WaitForSeconds(1);
            if (!dia.IsDialogueRunning)
                p.AllowMove(true);
            Destroy(Destination);
        }
        
        if (alreadyWalkedBack){
            isWalking = false;
        } else {
            isWalking = true;
        }
    }

    [YarnCommand("walkBack")]
    public void walkBack(){
        timeToWalkBack = true;
        alreadyWalkedBack = true;
    }

    [YarnCommand("dropKey")]
    public void dropKey(){
        timeToWalkBack = false;
        key.SetActive(true);
        isWalking = true;
    }
}