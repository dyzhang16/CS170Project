using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class HighlightSprite : MonoBehaviour
{
    public GameObject interact;
    public Sprite Sprite1;
    public Sprite Sprite2;
    public Item item;

    private DialogueRunner diaRun = null;

    // Start is called before the first frame update
    void Start()
    {
        diaRun = GameObject.Find("DiaSystem/DialogueRunner").GetComponent<DialogueRunner>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnMouseOver()
    {
        if (interact.tag == "Item" && diaRun != null && !diaRun.IsDialogueRunning)
        {
            interact.GetComponent<SpriteRenderer>().color = Color.yellow;
            //interact.GetComponent<SpriteRenderer>().sprite = Sprite1;
        }
    }
    void OnMouseExit()
    {
        if (interact.tag == "Item")
        {
            interact.GetComponent<SpriteRenderer>().color = Color.white;
            //interact.GetComponent<SpriteRenderer>().sprite = Sprite2;
        }
    }
}
