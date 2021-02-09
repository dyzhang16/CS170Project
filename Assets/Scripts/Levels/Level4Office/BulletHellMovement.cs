using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHellMovement : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public GameObject player;
    public GameObject bullet;
    public SpriteRenderer render;
    public bool moving = false;
    public bool allowMovement = true;
    public Vector2 movement = new Vector2(0, 0);

    public void Update()
    {
        if (allowMovement)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            if (h != 0 || v != 0)
            {
                movement.y = v;
                movement.x = h;
                moving = true;

                //animantor.SetFloat("Speed", Mathf.Abs(v) + Mathf.Abs(h));

                if (h > 0)
                {
                    render.flipX = true;
                }
                else
                {
                    render.flipX = false;
                }
            }
            else
            {
                moving = false;
                movement = new Vector2(0, 0);
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }
    public void Fire() 
    {
        Vector3 shootDirection;
        shootDirection = Input.mousePosition;
        shootDirection.z = 0.0f;
        shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
        shootDirection = shootDirection - transform.position;
        GameObject bul = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)), transform);
        Physics2D.IgnoreCollision(bul.GetComponent<CircleCollider2D>(), player.GetComponent<BoxCollider2D>(), true);
        bul.GetComponent<Bullet>().SetMoveDirection(shootDirection);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
