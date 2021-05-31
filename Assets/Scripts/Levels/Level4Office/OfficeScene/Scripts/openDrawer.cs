using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class openDrawer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject drawer;
    public DialogueRunner diaRun;

    public void OnMouseDown()
    {
        if (diaRun != null && !diaRun.IsDialogueRunning && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            this.gameObject.SetActive(false);
            SoundManagerScript.PlaySound("drawer_sound");
            drawer.SetActive(true);
        }
    }

}

