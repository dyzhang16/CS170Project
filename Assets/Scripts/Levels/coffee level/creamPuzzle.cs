using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creamPuzzle : MonoBehaviour
{
    public GameObject creamPuzzlePanel, coffeeUI, creamUI, sweetnerUI, sugar, cream;
    public int creamAdded, sweetnerAdded = 0;
    public bool sweetShaking, creamShaking = false;
    
    public void Start(){
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void addCream(){
        if (!creamShaking){
            creamShaking = !creamShaking;
            creamUI.transform.Rotate(0, 0, -35);

            ++creamAdded;
            Vector3 pos = new Vector3(creamUI.transform.position.x + 70, creamUI.transform.position.y, creamUI.transform.position.z);
            GameObject creams = Instantiate(cream, pos, creamUI.transform.localRotation, transform);
        }
        else
        {
            creamShaking = !creamShaking;
            creamUI.transform.Rotate(0, 0, 35);
        }
    }

    public void addSweetner(){

        if (!sweetShaking) {
            sweetShaking = !sweetShaking;
            sweetnerUI.transform.Rotate(0, 0, 35);

            ++sweetnerAdded;
            Vector3 pos = new Vector3(sweetnerUI.transform.position.x - 70, sweetnerUI.transform.position.y, sweetnerUI.transform.position.z);
            GameObject sugs = Instantiate(sugar, pos, sweetnerUI.transform.localRotation, transform);
        }
        else
        {
            sweetnerUI.transform.Rotate(0, 0, -35);

            sweetShaking = !sweetShaking;
        }
    }
}
