using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeDocument : MonoBehaviour
{
    private void Awake()
    {
        
        //this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        
        GetComponentInChildren<BoxCollider>().enabled = false;
    }
    public void setVisible()
    {
        //this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
    }
}
