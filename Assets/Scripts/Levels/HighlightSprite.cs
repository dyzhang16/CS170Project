using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class HighlightSprite : MonoBehaviour
{
    public GameObject interact;
    public Sprite Sprite1;
    public Sprite Sprite2;

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
        // Bottom URL links to how to block UI stuff
        // https://answers.unity.com/questions/822273/how-to-prevent-raycast-when-clicking-46-ui.html?childToView=862598#answer-862598
        if (diaRun != null && !diaRun.IsDialogueRunning && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            Color highlightColor = Color.white;
            if (interact.CompareTag("Inventory Item"))
            {
                highlightColor = Color.yellow;
            }
            else if (interact.CompareTag("World Item"))
            {
                highlightColor = Color.cyan;
            }
            else if (interact.CompareTag("Special"))
            {
                highlightColor = Color.green;
            }

            interact.GetComponent<SpriteRenderer>().color = highlightColor;
            //interact.GetComponent<SpriteRenderer>().sprite = Sprite1;
        }
    }
    void OnMouseExit()
    {
        if (interact.CompareTag("Item") || interact.CompareTag("Inventory Item") || interact.CompareTag("World Item")
            || interact.CompareTag("Special"))
        {
            interact.GetComponent<SpriteRenderer>().color = Color.white;
            //interact.GetComponent<SpriteRenderer>().sprite = Sprite2;
        }
    }
}
