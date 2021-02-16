using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class friend1 : MonoBehaviour
{

    public GameObject friend;

    void Awake(){
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [YarnCommand("GetCoffee")]
    public void GetCoffee(){
        friend.transform.position = new Vector3(390, 0, -240);
    }
}
