using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusBullet : MonoBehaviour
{
    private Vector2 moveDirection;
    private float moveSpeed;
    private void OnEnable()
    {
        Invoke("Destroy", 4f);
    }
    void Start()
    {
        moveSpeed = 1f;
    }
    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "World Item")
        {
            Destroy();
        }
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Hit Player");
        }
    }
    public void SetMoveDirection(Vector2 dir)
    {
        moveDirection = dir;
    }
    private void Destroy()
    {
        Destroy(gameObject);
    }
    private void OnDisable()
    {
        CancelInvoke();
    }

}
