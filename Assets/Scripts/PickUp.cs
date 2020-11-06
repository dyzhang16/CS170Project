using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Item Flowers;
    Inventory inventory;
    void Start()
    {

    }
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0))
        {
            Inventory.instance.Add(Flowers);
            Destroy(gameObject);
        }
    }

}