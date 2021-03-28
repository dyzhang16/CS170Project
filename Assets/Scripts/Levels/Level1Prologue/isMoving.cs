using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;
using Cinemachine;

public class isMoving : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Destination;
    public GameObject insideGraveyard;
    public GameObject flower;
    public GameObject gate;
    public GameObject outsideGraveyard;

    public GameObject gravestone;
    public GameObject key;
    public GameObject player;
    Player p;

    public DialogueRunner dia;

    public CinemachineVirtualCamera gravekeeperCam;
    public CinemachineVirtualCamera keyCam;

    public bool isWalking;
    public bool timeToWalkBack;
    private bool alreadyWalkedBack = false;
    public float Speed;

    private int cycle = 0;
    public bool whistle_on = false;

    public Sprite openGate;

    void Awake(){
        p = player.GetComponent<Player>();

        gravekeeperCam.enabled = false;
        keyCam.enabled = false;
    }

    [YarnCommand("MoveNPC")]
    public void MoveNPC()
    {
        isWalking = true;
        StartCoroutine(restartText());
    }

    IEnumerator restartText() {
        yield return new WaitForSeconds(1.5f);
        RunDialogue dialogue = gravestone.GetComponent<RunDialogue>();
        dialogue.dialogueRunner.StartDialogue("prologue_gravekeeper");
    }

    public void playWhistle() // plays whistle sound when gravekeeper is moving
    {
        if (isWalking == false) 
        { 
            whistle_on = false; 
        }

        if (isWalking)
        {

            if(whistle_on == false)
            {
                SoundManagerScript.PlaySound("whistle");
                whistle_on = true;
            }
            
        }
    }
    


    // Update is called once per frame
    void Update()
    {
        if (isWalking)
        {
            this.GetComponent<RunDialogue>().enabled = false;
            //distance traveled per frame
            float step = Speed * Time.deltaTime;

            //first flower
            if (cycle == 0){
                transform.position = Vector3.MoveTowards(transform.position, Destination.transform.position, step);
                p.AllowMove(false);
    
                if (Vector3.Distance(transform.position, Destination.transform.position) < 0.001f)
                {
                    isWalking = false;
                    StartCoroutine(doneWalking());
                }
            } else if (cycle == 1){ //diggy hole or something
                transform.position = Vector3.MoveTowards(transform.position, insideGraveyard.transform.position, step);

                if (Vector3.Distance(transform.position, insideGraveyard.transform.position) < 0.001f)
                {
                    isWalking = false;
                    StartCoroutine(doneWalking());
                }
            } else if (cycle == 2){ //flower
                transform.position = Vector3.MoveTowards(transform.position, flower.transform.position, step);
                p.AllowMove(false);

                if (Vector3.Distance(transform.position, flower.transform.position) < 0.001f)
                {
                    isWalking = false;
                    StartCoroutine(doneWalking());
                }
            } else if (cycle == 3){ //gate
                transform.position = Vector3.MoveTowards(transform.position, gate.transform.position, step);

                if (Vector3.Distance(transform.position, gate.transform.position) < 0.001f)
                {
                    isWalking = false;
                    //open gate
                    gate.GetComponent<SpriteRenderer>().sprite = openGate;
                    StartCoroutine(doneWalking());
                }
            } else if (cycle == 4) {//outside graveyard
                transform.position = Vector3.MoveTowards(transform.position, outsideGraveyard.transform.position, step);

                if (Vector3.Distance(transform.position, outsideGraveyard.transform.position) < 0.001f)
                {
                    isWalking = false;
                    StartCoroutine(doneWalking());
                }

            }

            if (cycle == 0 || cycle == 1){
                playWhistle();
            }
        }
        else
        {
            this.GetComponent<RunDialogue>().enabled = true;
        }
    }

    IEnumerator doneWalking(){
        if (cycle == 0){
            yield return new WaitForSeconds(1);
            Destroy(Destination);
            isWalking = true;
            if (!dia.IsDialogueRunning){
                p.AllowMove(true);
            }
        } else if (cycle == 1){
            GetComponent<SpriteRenderer>().flipX = true;
        } else if (cycle == 2){
            if (!dia.IsDialogueRunning){
                p.AllowMove(true);
                isWalking = true;
            }
        } else if (cycle == 3){
            yield return new WaitForSeconds(0.5f);
            isWalking = true;
        }

        ++cycle;
        // Debug.Log(cycle);
    }

    [YarnCommand("walkBack")]
    public void walkBack(){
        isWalking = true;
        gravekeeperCam.enabled = true;
    }

    [YarnCommand("dropKey")]
    public void dropKey(){
        isWalking = true;
        StartCoroutine(dropKeyDelay());
    }

    IEnumerator dropKeyDelay(){
        yield return new WaitForSeconds(1);
        key.transform.position = this.gameObject.transform.position;
        key.SetActive(true);
        gravekeeperCam.enabled = false;
        keyCam.enabled = true;
        yield return new WaitForSeconds(2);
        keyCam.enabled = false;
    }
}