using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class GuardPatrol : MonoBehaviour
{
    public GameObject Guard;
    public GameObject player;
    public GameObject resetPlayer;
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Document"))
        {
            player.transform.position = resetPlayer.GetComponent<TransformToDocument>().lastPosition;
        }
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Found Player");
        }
    }

}
