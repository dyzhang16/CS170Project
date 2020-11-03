using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class YarnTest : MonoBehaviour
{
    DialogueRunner diaRun = null;
    public string startNode = null;
    public YarnProgram loadScript = null;

    // Start is called before the first frame update
    void Start()
    {
        diaRun = FindObjectOfType<DialogueRunner>();        //finds dialouge runner component
        diaRun.Add(loadScript);                             //loads script
        diaRun.StartDialogue(startNode);                    //starts running the system w/ the script
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
