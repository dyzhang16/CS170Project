using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour                                     //https://www.youtube.com/watch?v=rxyvB-97h4I
{
    public float speed = 10.4f;
    
    public void Update()
    {
        Vector3 pos = transform.position;

        if(Input.GetKey("w"))
        {
            pos.y += speed * Time.deltaTime; //for 3D can use pos.z
        }
        if(Input.GetKey("s"))
        {
             pos.y -= speed * Time.deltaTime;
        }
        if(Input.GetKey("d"))
        {
             pos.x += speed * Time.deltaTime;
        }
        if(Input.GetKey("a"))
        {
             pos.x -= speed * Time.deltaTime;
        }

        transform.position = pos;
    }
}
