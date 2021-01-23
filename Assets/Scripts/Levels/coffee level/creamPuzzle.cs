using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creamPuzzle : MonoBehaviour
{
    public GameObject creamPuzzlePanel, coffeeUI, creamUI, cream;
    public int creamAdded = 0;
    public bool creamShaking;

    public void addCream(){
        if (!creamShaking){
            creamShaking = !creamShaking;
            creamUI.transform.Rotate(0, 0, 35);

            ++creamAdded;
            Vector3 pos = new Vector3(creamUI.transform.position.x - 70, creamUI.transform.position.y, creamUI.transform.position.z);
            GameObject creams = Instantiate(cream, pos, creamUI.transform.localRotation, transform);
        }
        else
        {
            creamShaking = !creamShaking;
            creamUI.transform.Rotate(0, 0, -35);
        }
    }
}
