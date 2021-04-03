using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeDocument : MonoBehaviour
{
    private void Awake()
    {
        this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        this.transform.Find("collider").gameObject.SetActive(false);
    }
    public void setVisible()
    {
        this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
    }
}
