using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Yarn.Unity;

public class TransformToDocument : MonoBehaviour//, IDropHandler
{
    public Sprite SpriteToChange;
    public GameObject player;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            //placeItem();
        }
    }

    [YarnCommand("TransformIntoDocument")]
    public void PossessDocuments()
    {
        player.GetComponent<SpriteRenderer>().sprite = SpriteToChange;
        //player.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        
        //player.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
    }

    public void placeItem()
    {
        player.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        //activeObj.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        //activeObj.transform.Find("collider").gameObject.SetActive(true);

        //activeObj = null;
    }
}
