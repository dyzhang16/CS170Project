using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PrizeReceiver : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        CranePrize prize = col.GetComponent<CranePrize>();
        if (prize)
        {
            // This is where the prize should be collected and added to inventory
            col.gameObject.SetActive(false);
        }
    }
}
