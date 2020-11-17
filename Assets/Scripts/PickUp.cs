using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Item item;
    Inventory inventory;
    RectTransform rectTrans;
    public GameObject player;
    private Player p;

    void Start()
    {
        rectTrans = transform as RectTransform;
        p = player.GetComponent<Player>();
    }
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Inventory.instance.AddItem(item);
            Destroy(gameObject);
        }
        else if (Input.GetKey(KeyCode.Mouse0) && !p.invActive){

            if (RectTransformUtility.RectangleContainsScreenPoint(rectTrans, Camera.main.ScreenToWorldPoint(Input.mousePosition))){
                Inventory.instance.AddItem(item);
                Destroy(gameObject);
            }
        }
    }

}