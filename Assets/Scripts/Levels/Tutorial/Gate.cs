using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")){
            RunDialogue dia = GetComponent<RunDialogue>();
            dia.dialogueRunner.StartDialogue("gate");
        }
    }
}
