using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class YarnTest : MonoBehaviour
{
    DialogueRunner diaRun;
    public string startNode;
    public YarnProgram[] scriptsToLoad;
    public List<GameObject> npcs;

    // Start is called before the first frame update
    void Start()
    {
        diaRun = FindObjectOfType<DialogueRunner>();        //finds dialouge runner component

      //   for(int i = 0;i < scriptsToLoad.Length;i++){
      //      diaRun.Add(scriptsToLoad[i]);
      //   }

        foreach(YarnProgram scripts in scriptsToLoad){
            diaRun.Add(scripts);
        }
        //diaRun.StartDialogue(startNode);                    //starts running the system w/ the script
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [YarnCommand("gravekeeper")]
    public void fuck() {
        GameObject gravekeeper;

        Vector3 target = new Vector3(13, 3, 0);

        gravekeeper.transform = Vector3.MoveTowards(transform.position, target, Time.deltaTime * 5);
    }
}
