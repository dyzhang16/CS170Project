using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour               //https://stackoverflow.com/questions/46760846/how-to-move-2d-object-with-wasd-in-unity
{
    public float speed;
    public Rigidbody2D rb;
    public float mouseSpeed;
    public GameObject player;
    private bool active = true;

    private void Start()
    {
        Inventory.instance.inventoryPanel.SetActive(false);
    }
    public void Update()
    {
        if (active)
        {
            Keyboard();
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                Menu();
            }
        }
    }

    void Mouse()
    {
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var direction = targetPosition - transform.position;

        rb.AddForce(direction * Time.deltaTime * this.mouseSpeed);
    }

    void Keyboard()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(h, v, 0) * this.speed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);
    }

    void Menu()
    {
        GameObject ip = Inventory.instance.inventoryPanel;
        ip.SetActive(!ip.activeSelf);
    }

    public void changeActive()
    {
        active = !active;
    }
}



