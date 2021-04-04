using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Document"))
        {
            //turns off Trigger if object cannot pass through solid objects
            GetComponent<BoxCollider>().isTrigger = false;
        }
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.GetComponentInChildren<BoxCollider>().tag == "Player")
        {
            //turns on trigger if object can pass through solid object
            GetComponent<BoxCollider>().isTrigger = true;
        }
    }
}
