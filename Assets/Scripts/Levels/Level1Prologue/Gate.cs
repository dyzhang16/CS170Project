using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Gate : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")){
            SoundManagerScript.PlaySound("wall_bump"); // play wall bump sound
            RunDialogue dia = GetComponent<RunDialogue>();
            dia.dialogueRunner.StartDialogue("gate");
        }
    }

    void OnCollisionStay(Collision collision){
        if (collision.gameObject.CompareTag("Player")){
            RunDialogue dia = GetComponent<RunDialogue>();
            dia.dialogueRunner.StartDialogue("gate");
        }
    }
}
