using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour               //https://stackoverflow.com/questions/46760846/how-to-move-2d-object-with-wasd-in-unity
{
    public float speed;
    public Rigidbody rb;
    public GameObject player;
    
    public bool allowMovement = true;
    public bool invActive = false;
    public bool moving = false;

    private void Start()
    {
        
    }

    public void Update()
    {
        if (moving){
            moving = false;
            rb.velocity = new Vector3(0, 0, 0);
        }

        if (allowMovement)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            if (h != 0 || v != 0){
                Keyboard(h, v);
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab)) {
            Menu();
        }
    }

    void Mouse()
    {
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        rb.MovePosition(Vector3.MoveTowards(transform.position, targetPosition, 1f));

        moving = true;
    }

    void Keyboard(float h, float v)
    {
        // Vector3 movement = new Vector3(h, 0, v) * this.speed * Time.deltaTime;
        // rb.MovePosition(transform.position + movement);

        rb.velocity = new Vector3(h, 0, v) * this.speed;
        moving = true;
    }

    void Menu()
    {
        GameObject ip = Inventory.instance.inventoryPanel;
        invActive = !invActive;
        ip.SetActive(!ip.activeSelf);
    }

    public void AllowMove(bool allow)
    {
        allowMovement = allow;
    }
}



