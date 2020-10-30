using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeMainScene2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collider)                          //https://www.youtube.com/watch?v=Bc9lmHjqLZc
    {
        if(collider.gameObject.CompareTag("Player")){
            SceneManager.LoadScene("Scene1");
            //Debug.Log("Collision Detected.");
        }
    }
}
