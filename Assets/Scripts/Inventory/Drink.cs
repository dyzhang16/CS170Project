using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Drink", menuName = "Items/Drink")]
public class Drink : Item
{
    public int Sugar { get { return s_Added; } set { s_Added = value; } }
    [SerializeField]
    private int s_Added = 0;
    public int Cream { get { return c_Added; } set { c_Added = value; } }
    [SerializeField]
    private int c_Added = 0;

    public override void Use()
    {
        Debug.Log("Sugar Added: " + Sugar);
        Debug.Log("Cream Added: " + Cream);
    }
}
