using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class OfficeHub : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    public GameObject player;
    public GameObject exitToCityOffice;

    void Awake()
    {
        if (GameManager.instance != null)
        {
            if (GameManager.instance.previousScene == "StreetIntro")
            {
                player.transform.position = exitToCityOffice.transform.position + new Vector3(12, 0, 0);
            }
            if (GameManager.instance.followFriendinOffice == 1)
            {
                dialogueRunner.startAutomatically = false;
            }
        }
    }
    void Start()
    {

    }
}
