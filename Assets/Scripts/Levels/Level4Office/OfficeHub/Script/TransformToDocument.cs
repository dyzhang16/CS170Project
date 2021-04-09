using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Yarn.Unity;

public class TransformToDocument : MonoBehaviour
{
    public GameObject activeObj;
    public GameObject player;
    public GameObject guard;
    //[HideInInspector] public Vector3 lastPosition;
    private void Update()
    {
        if (activeObj != null)
        {
            activeObj.transform.position = player.transform.position;

            /*if (Input.GetKeyDown(KeyCode.R))
            {
                player.transform.position = lastPosition;
            }*/
        }
    }

    [YarnCommand("TransformIntoDocument")]
    public void PossessDocuments(string[] parameters)
    {
        string Object = parameters[0];
        activeObj = GameObject.Find(Object);
        activeObj.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.75f);
        player.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        guard.GetComponent<OfficeGuard>().resetPlayerPosition = player.transform.position;
        player.GetComponentInChildren<BoxCollider>().tag = "Document";
    }
    [YarnCommand("TransformIntoPlayer")]
    public void ReleasePossession(string[] parameters)
    {
        Debug.Log(parameters[0]);
        string Object = parameters[0];
        activeObj = GameObject.Find(Object);
        player.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        activeObj.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        player.GetComponentInChildren<BoxCollider>().tag = "Player";
        activeObj = null;
    }
}
