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
        transform.Rotate(0,0,-80*Time.deltaTime);

        Vector3 id = transform.position;
        id.y -=1;
        transform.position = id;

        RectTransform c = coffeeCup.transform as RectTransform;

        if (timerIsRunning)             //will execute function once
        {
            if (timeRemaining > 0)      //while running
            {
                Debug.Log("Time Remaining: " + timeRemaining);
                timeRemaining -= Time.deltaTime;    //gets time in seconds
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;                  //reset variables to prevent function from executing repeatedly
                timerIsRunning = false;
                Destroy(gameObject);
                SoundManagerScript.PlaySound("plop");
            }
        }
    }
}
