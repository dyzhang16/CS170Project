using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraGravity : MonoBehaviour
{
    public ConstantForce gravity;

    Rigidbody rigidbody;

    void Start(){
        rigidbody = this.gameObject.GetComponent<Rigidbody>();
        gravity = this.gameObject.AddComponent<ConstantForce>();
        gravity.force = new Vector3(0.0f, -50.0f, 0.0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }
}
