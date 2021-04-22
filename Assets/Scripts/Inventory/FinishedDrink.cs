using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "new Finished Drink", menuName = "Items/FinishedDrink")]
public class FinishedDrink : Drink
{
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
