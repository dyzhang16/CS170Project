using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "new Finished Drink", menuName = "Items/Drink/FinishedDrink")]
public class FinishedDrink : Item
{
    public Sprite sleeveAndLidIcon;

    public int Sugar { get { return s_Added; } set { s_Added = value; } }
    [SerializeField]
    private int s_Added = 0;
    public int Cream { get { return c_Added; } set { c_Added = value; } }
    [SerializeField]
    private int c_Added = 0;

    public override void Use()
    {

    }
    public override string DisplayInfo()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("<color=yellow>").Append("<size=30>").Append(itemName).Append("</color>").Append("</size>").AppendLine();
        builder.Append(itemDescription).AppendLine();

        return builder.ToString();
    }
}
