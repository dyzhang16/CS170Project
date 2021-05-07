using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeDocument : MonoBehaviour
{
    public void Start()
    {
        if(GameManager.instance.documentNeeded == 0)
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
            GetComponentInChildren<BoxCollider>().enabled = false;
        }
    }
    public void Update() 
    {
        if (GameManager.instance.documentNeeded == 1)
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            GetComponentInChildren<BoxCollider>().enabled = true;
        }
    }
}
