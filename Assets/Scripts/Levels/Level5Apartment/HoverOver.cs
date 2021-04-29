using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Yarn.Unity;

public class HoverOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool hovering;

    private DialogueRunner diaRun = null;
    private Vector3 defaultValue;
    // Start is called before the first frame update
    void Start()
    {
        diaRun = GameObject.Find("DiaSystem Prefab 1/DialogueRunner").GetComponent<DialogueRunner>();
        defaultValue = this.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (hovering){
            this.transform.localScale = Vector3.MoveTowards(this.transform.localScale, defaultValue * 1.3f,
                20f * Time.deltaTime);
        } else {
            this.transform.localScale = Vector3.MoveTowards(this.transform.localScale, defaultValue,
                20f * Time.deltaTime);
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        hovering = true;

        if (diaRun != null & !diaRun.IsDialogueRunning) {
            Color highlightColor = Color.white;
            if (this.CompareTag("Inventory Item"))
            {
                highlightColor = Color.yellow;
            }
            else if (this.CompareTag("World Item"))
            {
                highlightColor = Color.cyan;
            }
            else if (this.CompareTag("Special"))
            {
                highlightColor = Color.green;
            }

            this.GetComponent<Image>().color = highlightColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        hovering = false;

        this.GetComponent<Image>().color = Color.white;
    }
}
