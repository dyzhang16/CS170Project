using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingstuff : MonoBehaviour
{
    public GameObject coffeeCup;
    public bool timerIsRunning = false;
    public float timeRemaining;
    

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(transform);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(0, 0, -80 * Time.deltaTime);

        Vector3 id = transform.position;
        id.y -= 3;
        transform.position = id;
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collider.gameObject.tag == "Coffee")
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            gameObject.SetActive(false);
            Debug.Log("Hit Lid of Coffee");
            SoundManagerScript.PlaySound("plop");
        }
    }
}

