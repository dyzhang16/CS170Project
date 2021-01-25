using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car : MonoBehaviour
{
    public GameObject DestinationTopLeft;
    public GameObject DestinationBotLeft;
    public GameObject DestinationBotRight;
    public GameObject DestinationTopRight;

    public SpriteRenderer render;

    public float Speed;

    private int cycle = 0;

    // Update is called once per frame
    void Update()
    {
        Vector3 distance;
        if (cycle == 0){
            distance = transform.position - DestinationTopLeft.transform.position;
            render.flipX = true;
        } else if (cycle == 1){
            distance = transform.position - DestinationBotLeft.transform.position;
        } else if (cycle == 2){
            distance = transform.position - DestinationBotRight.transform.position;
            render.flipX = false;
        } else {
            distance = transform.position - DestinationTopRight.transform.position;
        }

        distance = -distance.normalized;
        transform.position += distance * Speed * Time.deltaTime;

        if (distance.z >= 0 && distance.x >= 0)          //Changes based on Positioning
        {
            cycle = (++cycle)%4;
        }
    }
}
