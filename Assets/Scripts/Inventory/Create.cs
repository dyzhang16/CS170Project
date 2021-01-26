using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create : MonoBehaviour
{
    public Drink toClone;

    public void CreateDrink() 
    {
        var so = Instantiate(toClone);
    }

}
