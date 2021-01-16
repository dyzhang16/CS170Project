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
    public GameObject gravestone;
    public GameObject player;
    public bool isWalking;
    public float Speed;

    private Vector3 offmap = new Vector3(115, -3, -55);
    private bool left = true;

    void Start()
    {

    }

    [YarnCommand("MoveNPC")]
    public void MoveNPC()
    {
        isWalking = true;
        Player p = player.GetComponent<Player>();
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
                distance = Character.transform.position - Destination.transform.position;
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
        }
    }

    IEnumerator doneWalking(){
        if (left){
            left = false;
            yield return new WaitForSeconds(3);
            Destroy(Destination);
            isWalking = true;
        }
    }
}