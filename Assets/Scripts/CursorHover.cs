using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorHover : MonoBehaviour
{

    public GameObject Cursor;
    public bool showCursor = true;

    public bool changePos;
    public Vector3 changeVector;

    void OnTriggerEnter(Collider collider){
        if (collider.gameObject.CompareTag("Player")){
            showCursor = true;
        }
    }

    void OnTriggerExit(Collider collider){
        if (collider.gameObject.CompareTag("Player")){
            showCursor = false;
            Cursor.SetActive(false);
        }
    }

    void OnTriggerStay(Collider collider){
        if (collider.gameObject.CompareTag("Player")){
            if (showCursor){
                Cursor.SetActive(true);
                if (changePos){
                    Cursor.transform.position = this.GetComponent<SpriteRenderer>().bounds.center + changeVector;
                } else {
                    Cursor.transform.position = this.GetComponent<SpriteRenderer>().bounds.center;
                }
            } else {
                Cursor.SetActive(false);
            }
        }
    }
}
