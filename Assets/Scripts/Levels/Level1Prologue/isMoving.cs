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

    private Vector3 offmap;
    private int cycle = 0;
    public bool whistle_on = false;

    void Start() {
        target = Destination.transform.position;
        offmap = transform.position;
    }

    [YarnCommand("MoveNPC")]
    public void MoveNPC()
    {
        isWalking = true;
        p = player.GetComponent<Player>();
        StartCoroutine(restartText());
    }

    IEnumerator restartText() {
        yield return new WaitForSeconds(1.5f);
        RunDialogue dia = gravestone.GetComponent<RunDialogue>();
        dia.dialogueRunner.StartDialogue("prologue_gravekeeper");
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
            
            float step = Speed * Time.deltaTime;

            if (cycle == 0){
                transform.position = Vector3.MoveTowards(transform.position, target, step);
                p.AllowMove(false);
            } else if (cycle == 1){
                transform.position = Vector3.MoveTowards(transform.position, offmap, step);
            } else if (cycle == 2){
                transform.position = Vector3.MoveTowards(transform.position, target, step);
                p.AllowMove(false);
            } else if (cycle == 3){
                transform.position = Vector3.MoveTowards(transform.position, offmap, step);
            }

            if (cycle == 0 || cycle == 2){
                if (Vector3.Distance(transform.position, target) < 0.001f)
                {
                    isWalking = false;
                    StartCoroutine(doneWalking());
                }
            } else {
                if (Vector3.Distance(transform.position, offmap) < 0.001f)
                {
                    isWalking = false;
                    StartCoroutine(doneWalking());
                }
            }
            playWhistle();

        }
    }

    IEnumerator doneWalking(){
        if (cycle == 0){
            yield return new WaitForSeconds(1);
            if (!dia.IsDialogueRunning)
                p.AllowMove(true);
            Destroy(Destination);
            isWalking = true;
            GetComponent<SpriteRenderer>().flipX = true;
        } else if (cycle == 1){
            GetComponent<SpriteRenderer>().flipX = false;
        } else if (cycle == 2){
            if (!dia.IsDialogueRunning){
                p.AllowMove(true);
                isWalking = true;
            }

            GetComponent<SpriteRenderer>().flipX = true;
        }

        ++cycle;
        // Debug.Log(cycle);
    }

    [YarnCommand("walkBack")]
    public void walkBack(){
        //GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        isWalking = true;
    }

    [YarnCommand("dropKey")]
    public void dropKey(){
        key.SetActive(true);
        isWalking = true;
    }
}