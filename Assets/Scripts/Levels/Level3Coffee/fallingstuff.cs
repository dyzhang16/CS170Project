using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingstuff : MonoBehaviour
{
    public GameObject coffeeCup;
    public bool timerIsRunning = false;
    public float timeRemaining;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float pos = transform.position.y;
        pos -= speed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, pos, transform.position.z);
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collider.gameObject.tag == "Coffee")
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            // Debug.Log("Hit Lid of Coffee");
            SoundManagerScript.PlaySound("plop");
            Destroy(this.gameObject);
        }
    }
}

