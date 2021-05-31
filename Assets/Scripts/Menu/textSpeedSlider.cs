using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class textSpeedSlider : MonoBehaviour
{
    public Text textSpeedText;
    void Start()
    {
        if (GameManager.instance != null)
        {
            this.GetComponent<Slider>().value = GameManager.instance.textSpeed;
            textSpeedText.text = "" + GameManager.instance.textSpeed;
        }
        GameObject dia = GameObject.Find("DialogueRunner");
        if (dia != null)
        {
            dia.GetComponent<DialogueUI>().textSpeed = ((0.05f - 0.01f) * ((10f - GameManager.instance.textSpeed) / 10f)) + 0.01f;
        }      
    }
    public void textSpeedSliderUpdate(float value)
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.textSpeed = value;
        }

        GameObject dia = GameObject.Find("DialogueRunner");
        if (dia != null)
        {
            dia.GetComponent<DialogueUI>().textSpeed = ((0.05f - 0.01f) * ((10f - value) / 10f)) + 0.01f;
        }
        textSpeedText.text = "" + value;
        //Debug.LogWarning("The Text value is: " + value);
    }
}
