using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour               //https://stackoverflow.com/questions/46760846/how-to-move-2d-object-with-wasd-in-unity
{
    public float speed;
    public Rigidbody2D rb;
    public float mouseSpeed;
    public GameObject player;
    
    public bool allowMovement = true;
    public bool mouseMovement = false;
    public bool invActive = false;
    public bool moving = false;

    private void Start()
    {
        
    }

    public void Update()
    {
        if (allowMovement)
        {
            if (mouseMovement) {
                moving = false;
                if (Input.GetMouseButton(0) && !invActive){
                    Mouse();
                }
            } else {
                moving = false;

                float h = Input.GetAxis("Horizontal");
                float v = Input.GetAxis("Vertical");

                if (h != 0 || v != 0){
                    Keyboard(h, v);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab)) {
            Menu();
        }
    }

    void Mouse()
    {
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        rb.MovePosition(Vector3.MoveTowards(transform.position, targetPosition, mouseSpeed));

        moving = true;
    }

    void Keyboard(float h, float v)
    {
        Vector3 movement = new Vector3(h, v, 0) * this.speed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);

        moving = true;
    }

    void Menu()
    {
        GameObject ip = Inventory.instance.inventoryPanel;
        invActive = !invActive;
        ip.SetActive(!ip.activeSelf);
    }

    public void changeAllowMovement()
    {
        allowMovement = !allowMovement;
    }
}



