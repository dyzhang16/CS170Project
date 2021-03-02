using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityOffice : MonoBehaviour
{
    public NodeVisitedTracker tracker;

    public GameObject player;
    public GameObject Office;

    public GameObject exitToCityArcade;
    public GameObject exitToCityApartment;

    void Awake(){
        if (GameManager.instance != null)
        {
            if (GameManager.instance.previousScene == "CityArcade"){
                player.transform.position = exitToCityArcade.transform.position + new Vector3(10, 0, 0);
            } else if (GameManager.instance.previousScene == "CityApartment"){
                player.transform.position = exitToCityApartment.transform.position + new Vector3(-10, 0, 0);
            } else if (GameManager.instance.previousScene == "Office"){
                player.transform.position = Office.transform.position + new Vector3(0, 0, -10);
            }
        }
    }

    void Start()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.loadItems();
            GameManager.instance.deleteItems();
        }
    }
}
