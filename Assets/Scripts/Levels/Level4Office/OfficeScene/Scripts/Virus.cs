using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : MonoBehaviour //https://answers.unity.com/questions/1369351/unity-2d-random-movement.html
{
    public float accelerationTime = 30f;
    public float maxSpeed = 30f;
    private Vector2 movement;
    private float timeLeft;
    public Rigidbody2D rb;

    void Start()
    {
        Debug.Log("Spawned a Virus");
    }
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            movement = new Vector2(Random.Range(-30f, 30f), Random.Range(-30f, 30f));
            timeLeft += accelerationTime;
        }

    }
    private void Shoot() 
    {

    }
    private void FixedUpdate()
    {
        rb.AddForce(movement * maxSpeed);
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "World Item")
        {
            movement = new Vector2(Random.Range(-30f, 30f), Random.Range(-30f, 30f));
        }
    }
    public void Destroy()
    {
        Destroy(gameObject);
        transform.GetComponentInParent<BulletHellPuzzle>().enemy = false;
    }

}
