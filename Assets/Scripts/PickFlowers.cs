using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickFlowers : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay2D(Collider2D collider)                          //https://www.youtube.com/watch?v=Bc9lmHjqLZc
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            Object.Destroy(this.gameObject);
            //Debug.Log("Collided with an inventory item");
        }
    }
}
