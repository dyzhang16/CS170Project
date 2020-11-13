using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dropper : MonoBehaviour
{
    public List<Item> keyItems;
    public string itemName;
    public string itemDescription;

    private Item recievedItem;

    public virtual bool CheckForRecievedDrop(Vector2 pos, Item item) {
        RectTransform rectTrans = transform as RectTransform;
        Vector2 worldVec = Camera.main.ScreenToWorldPoint(pos);

        if (RectTransformUtility.RectangleContainsScreenPoint(rectTrans, worldVec))
        {
            foreach (Item i in keyItems) {
                if (i == item) {
                    recievedItem = item;
                    Debug.Log("good item");
                    return true;
                }
            }

            Debug.Log("inside rect, not correct item");
            return false;
        }
        else
        {
            Debug.Log("outside of rect");
            return false;
        }
    }
}
