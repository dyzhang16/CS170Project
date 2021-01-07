using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isMoving : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Character;
    public GameObject Destination;
    public float Speed;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Character.GetComponent<YarnCommand>().isWalking)
        {
            //Debug.Log("Movin");
            Vector3 distance = Character.transform.position - Destination.transform.position;
            distance = -distance.normalized;
            Character.transform.position += distance * Speed * Time.deltaTime;
            if (distance.y >= 0 && distance.x >= 0)          //Changes based on Positioning
            {
                Character.GetComponent<YarnCommand>().isWalking = false;
            }
        }
    }
}