using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class gravekeeperending : MonoBehaviour
{
    public DialogueRunner dia;

    public GameObject gate;
    public Sprite openGate;
    private Sprite closedGate;
    public GameObject thirdGravestone;
    public GameObject outside;

    public GameObject player;
    Player p;

    public float Speed;
    public bool isWalking;
    private int cycle = 0;

    [YarnCommand("EndingCutscene")]
    public void EndingCutscene(){
        isWalking = true;
        StartCoroutine(runDialogue());
    }

    void Awake(){
        p = player.GetComponent<Player>();

        closedGate = gate.GetComponent<SpriteRenderer>().sprite;
    }

    void Update(){
        if (isWalking)
        {
            this.GetComponent<RunDialogue>().enabled = false;
            //distance traveled per frame
            float step = Speed * Time.deltaTime;

            //walk to gate
            if (cycle == 0){
                transform.position = Vector3.MoveTowards(transform.position, gate.transform.position, step);
                p.AllowMove(false);
    
                if (Vector3.Distance(transform.position, gate.transform.position) < 0.001f)
                {
                    isWalking = false;
                    gate.GetComponent<SpriteRenderer>().sprite = openGate;
                    StartCoroutine(doneWalking());
                }
            } else if (cycle == 1){ //3rd gravestone
                transform.position = Vector3.MoveTowards(transform.position, thirdGravestone.transform.position, step);
                p.AllowMove(false);
    
                if (Vector3.Distance(transform.position, thirdGravestone.transform.position) < 0.001f)
                {
                    isWalking = false;
                    StartCoroutine(doneWalking());
                }
            } else if (cycle == 2){ //back to gate
                transform.position = Vector3.MoveTowards(transform.position, gate.transform.position, step);
                p.AllowMove(false);
    
                if (Vector3.Distance(transform.position, gate.transform.position) < 0.001f)
                {
                    isWalking = false;
                    gate.GetComponent<SpriteRenderer>().sprite = openGate;
                    StartCoroutine(doneWalking());
                }
            } else if (cycle == 3){ //outside of gate
                transform.position = Vector3.MoveTowards(transform.position, outside.transform.position, step);
    
                if (Vector3.Distance(transform.position, outside.transform.position) < 0.001f)
                {
                    isWalking = false;
                }
            }
        }
    }

    IEnumerator doneWalking(){
        if (cycle == 0){
            yield return new WaitForSeconds(0.5f);
            gate.GetComponent<SpriteRenderer>().sprite = closedGate;
            isWalking = true;
        } else if (cycle == 1){
            yield return new WaitForSeconds(1f);
            thirdGravestone.SetActive(true);
            GetComponent<SpriteRenderer>().flipX = true;
            isWalking = true;
        } else if (cycle == 2){
            yield return new WaitForSeconds(0.5f);
            gate.GetComponent<SpriteRenderer>().sprite = closedGate;
            isWalking = true;
        }

        ++cycle;
    }

    IEnumerator runDialogue(){
        yield return new WaitForSeconds(1f);
        dia.StartDialogue("endingScene");
    }
}
