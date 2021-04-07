using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Item : ScriptableObject
{
    public string itemName;
    [TextArea]
    public string itemDescription;
    public Sprite icon;

    public virtual void Use()
    {
        Debug.Log("Using: " + itemName);
    }
    public virtual string DisplayInfo()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("<color=green>").Append("<size=30>").Append(itemName).Append("</color>").Append("</size>").AppendLine();
        builder.Append(itemDescription).AppendLine();
        
        return builder.ToString();
    }
}