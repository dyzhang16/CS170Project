using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sugarPuzzle : MonoBehaviour
{
    public GameObject sugarPuzzlePanel, coffeeUI, sugarUI, sugar;
    public int sugarAdded = 0;
    public bool sweetShaking;
    public void addSugar()
    {

        if (!sweetShaking)
        {
            sweetShaking = !sweetShaking;
            sugarUI.transform.Rotate(0, 0, -35);

            ++sugarAdded;
            Vector3 pos = new Vector3(sugarUI.transform.position.x + 70, sugarUI.transform.position.y, sugarUI.transform.position.z);
            GameObject sugs = Instantiate(sugar, pos, sugarUI.transform.localRotation, transform);
        }
        else
        {
            sugarUI.transform.Rotate(0, 0, 35);

            sweetShaking = !sweetShaking;
        }
    }
}
