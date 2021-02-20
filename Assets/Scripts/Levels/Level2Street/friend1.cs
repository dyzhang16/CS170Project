using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class friend1 : MonoBehaviour
{

    public GameObject friend;

    public bool isWalking = false;
    public float Speed;
    public GameObject Destination;
    private Vector3 target;

    void Awake(){
        target = Destination.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isWalking){
            float step = Speed * Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, target, step);

            if (Vector3.Distance(transform.position, target) < 0.001f){
                isWalking = false;
            }
        }
    }

    [YarnCommand("GetCoffee")]
    public void GetCoffee(){
        friend.transform.position = new Vector3(390, 0, -240);
    }

    [YarnCommand("MoveToOffice")]
    public void MoveToOffice(){
        isWalking = true;
    }
}
