using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
public class HideDoor : MonoBehaviour
{
    private DialogueRunner diaRun = null;
    public GameObject itemsUnder;
    
    private void Start()
    {
        diaRun = GameObject.Find("DiaSystem/DialogueRunner").GetComponent<DialogueRunner>();
    }
    private void OnMouseDown()
    {
        if (diaRun != null && !diaRun.IsDialogueRunning && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            this.GetComponent<Animator>().SetTrigger("OpenDoor");
            StartCoroutine(DestroyDoor());
            itemsUnder.SetActive(true);
        }
        if (gameObject.name == "LeftDoor")
        {
            GameManager.instance.openLeftDoor = 1;
        }
        else if (gameObject.name == "MidDoor")
        {
            GameManager.instance.openMidDoor = 1;
        }
        else if(gameObject.name == "RightDoor")
        {
            GameManager.instance.openRightDoor = 1;
        }
    }

    IEnumerator DestroyDoor(){
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
    private void OnMouseOver()
    {
        if (diaRun != null && !diaRun.IsDialogueRunning && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;
        }
    }
    private void OnMouseExit()
    {
        if (diaRun != null && !diaRun.IsDialogueRunning && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
