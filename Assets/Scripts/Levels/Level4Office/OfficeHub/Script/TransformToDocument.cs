using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Yarn.Unity;

public class TransformToDocument : MonoBehaviour
{
    [HideInInspector]public GameObject activeObj;
    public GameObject player;
    public GameObject guard;
    //[HideInInspector] public Vector3 lastPosition;
    private void Awake()
    {
        
    }
    private void Update()
    {
        if (activeObj != null)
        {
            activeObj.transform.position = player.transform.position + new Vector3(0, 3.4f, 6);   
        }
    }

    [YarnCommand("TransformIntoDocument")]
    public void PossessDocuments(string[] parameters)
    {
        SoundManagerScript.PlaySound("document_transform");
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
       // SoundManagerScript.PlaySound("document_transform");
        Debug.Log(parameters[0]);
        string Object = parameters[0];
        activeObj = GameObject.Find(Object);
        player.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        activeObj.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        player.GetComponentInChildren<BoxCollider>().tag = "Player";
        GetComponent<SpriteRenderer>().color = new Color(0f, 1f, 0f, 1f);
        activeObj = null;
    }
}
