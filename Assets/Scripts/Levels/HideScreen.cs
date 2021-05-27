using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class HideScreen : MonoBehaviour
{
    public Animator transition;
    [YarnCommand("FadeOut")]
    public void RemoveScreen()
    {
        transition.SetTrigger("End");
    }
}
