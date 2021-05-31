using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Transparent : MonoBehaviour
{
    public bool doTransparency = true;

    void OnTriggerStay(Collider collider) {
        
        if (doTransparency) {
            if (collider.gameObject.tag == "Player") {
                if (GetComponent<SpriteRenderer>() != null) {
                    float dist = Vector3.Distance(this.transform.position, collider.gameObject.transform.position)/ (GetComponent<SpriteRenderer>().bounds.size.x);
                    if (GameObject.Find("DialogueRunner") != null)
                    {
                        GameObject dia = GameObject.Find("DialogueRunner");
                        DialogueRunner diaRunner = dia.GetComponent<DialogueRunner>();
                        if (!diaRunner.IsDialogueRunning)
                        {
                            SoundManagerScript.PlaySound("woosh");
                        }
                    }
                    GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, dist);
                } else if (transform.parent.GetComponent<SpriteRenderer>() != null){
                    float dist = Vector3.Distance(this.transform.parent.position, collider.gameObject.transform.position)/ (transform.parent.GetComponent<SpriteRenderer>().bounds.size.x);

                    // Debug.Log(dist);

                    transform.parent.GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, dist);
                }
                
            }
        }
    }

    void OnTriggerExit(Collider collider) {

        if (collider.gameObject.tag == "Player") {
             if (doTransparency) {
                if (GetComponent<SpriteRenderer>() != null) {
                    GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, 1f);
                } else if (transform.parent.GetComponent<SpriteRenderer>() != null) {
                    transform.parent.GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, 1f);
                }
            }
        }
    }

   
    // void OnCollisionStay(Collision collision){
    //     GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.25f);
    // }

    // void OnCollisionExit(Collision collision){
    //     GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
    // }
}
