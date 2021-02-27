using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "new Arcade Ticket Item", menuName = "Arcade/Ticket (item)")]
public class ArcadeTicketItem : KeyItem
{
	public int TicketCount;

    public override string DisplayInfo()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("<color=yellow>").Append("<size=30>").Append(itemName).Append("</color>").Append("</size>").AppendLine();
        builder.Append(itemDescription).AppendLine();
        builder.Append(string.Format("{0} ticket{1}", TicketCount, (TicketCount == 1)?"":"s")).AppendLine();

        return builder.ToString();
    }
}
