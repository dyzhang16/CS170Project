using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class car : MonoBehaviour
{
    public GameObject DestinationTopLeft;
    public GameObject DestinationBotLeft;
    public GameObject DestinationBotRight;
    public GameObject DestinationTopRight;

    public DialogueRunner dia;

    public bool moving;
    public SpriteRenderer render;

    public float Speed;

    private int cycle = 0;

    // Update is called once per frame
    void Update()
    {
        if (dia.IsDialogueRunning){
            moving = false;
        } else {
            moving = true;
        }

        if (moving){
            Vector3 target;
            if (cycle == 0){
                target = DestinationTopLeft.transform.position;
                render.flipX = true;
            } else if (cycle == 1){
                target = DestinationBotLeft.transform.position;
            } else if (cycle == 2){
                target = DestinationBotRight.transform.position;
                render.flipX = false;
            } else {
                target = DestinationTopRight.transform.position;
            }

            float step = Speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, step);

            if (Vector3.Distance(transform.position, target) < 0.001f)          //Changes based on Positioning
            {
                cycle = (++cycle)%4;
            }
        }
    }
}
