using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transparent : MonoBehaviour
{

    public Player player;

    void OnTriggerStay(Collider collider) {
        
        float dist = Vector3.Distance(this.transform.position, player.transform.position)/ (GetComponent<SpriteRenderer>().bounds.size.x/2);

        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, dist);
    }

    void OnTriggerExit(Collider collider) {

        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);

    }
}
