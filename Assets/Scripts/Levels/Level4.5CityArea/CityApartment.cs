using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityApartment : MonoBehaviour
{
    public NodeVisitedTracker tracker;

    public GameObject player;
    public GameObject Apartment;

    public GameObject exitToCityOffice;

    void Awake(){
        if (GameManager.instance != null)
        {
            if (GameManager.instance.previousScene == "CityOffice"){
                player.transform.position = exitToCityOffice.transform.position + new Vector3(10, 0, 0);
            } else if (GameManager.instance.previousScene == "ApartmentScene"){
                player.transform.position = Apartment.transform.position + new Vector3(0, -10, 0);
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
