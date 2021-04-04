using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class DisableDialogue : MonoBehaviour
{
    [YarnCommand("DisableDialogue")]
    public void Disable() 
    {
        this.GetComponent<RunDialogue>().enabled = false;
        this.GetComponent<BoxCollider>().enabled = false;
    }
}
