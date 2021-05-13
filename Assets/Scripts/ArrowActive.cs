using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class ArrowActive : MonoBehaviour
{
    [YarnCommand("cursorActive")]
    public void cursorActive(){
        this.GetComponent<BoxCollider>().enabled = true;
    }
}
