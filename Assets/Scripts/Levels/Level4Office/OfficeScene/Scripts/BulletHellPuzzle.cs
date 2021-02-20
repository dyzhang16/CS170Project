using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class BulletHellPuzzle : MonoBehaviour
{
    public GameObject Virus;
    public GameObject Player;
    [HideInInspector] public bool enemy = false;
    [HideInInspector] public int destroyed = 0;
    public GameObject westWall, eastWall, northWall, southWall;

    public DialogueRunner DialogueRunner;
    public string dialogueToRun;

    private float x, y;
    

    // Update is called once per frame
    void Update()
    {
        x = Random.Range(westWall.transform.position.x + 15, eastWall.transform.position.x - 15);
        y = Random.Range(southWall.transform.position.y + 15, northWall.transform.position.y - 15);
        var pos = new Vector3(x, y, 111.1422f);
        if (!enemy)
        {
            enemy = true;
            Instantiate(Virus, pos, transform.localRotation, transform);
        }
        if (destroyed == 5)
        {
            GameManager.instance.officePuzzle = 1;
            Debug.Log("Destroyed 5 Viruses");
            DialogueRunner.StartDialogue(dialogueToRun);
        }

    }
}
