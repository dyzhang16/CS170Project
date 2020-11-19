using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PickUp : MonoBehaviour, IPointerDownHandler
{
    public Item item;
    Inventory inventory;
    RectTransform rectTrans;
    public GameObject player;
    private Player p;
    public bool coffee = false;

    void Start()
    {
        rectTrans = transform as RectTransform;
        p = player.GetComponent<Player>();
    }
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Inventory.instance.AddItem(item);
            Destroy(gameObject);
        }
        else if (Input.GetKey(KeyCode.Mouse0) && !p.invActive)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(rectTrans, Camera.main.ScreenToWorldPoint(Input.mousePosition))){
                Inventory.instance.AddItem(item);
                Destroy(gameObject);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (coffee)
        {
            Inventory.instance.AddItem(item);
            Destroy(gameObject);
        }
    }
}