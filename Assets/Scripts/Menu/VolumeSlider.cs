using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class VolumeSlider : MonoBehaviour
{

    public GameObject volumeImage;
    public GameObject muteImage;

    public Text volumeText;

    public Slider textSpeedSlider;
    public Text textSpeedText;

    // Start is called before the first frame update
    void Start()
    {
        if (MusicManagerScript.instance != null){
            this.GetComponent<Slider>().value = MusicManagerScript.instance.musicVolume*10;
            
            volumeText.text = "" + MusicManagerScript.instance.musicVolume*10;
        }

        if (GameManager.instance != null){
            textSpeedSlider.value = GameManager.instance.textSpeed;

            textSpeedText.text = "" + GameManager.instance.textSpeed;

            GameObject dia = GameObject.Find("DialogueRunner");
            if (dia != null){
                dia.GetComponent<DialogueUI>().textSpeed = ((0.05f - 0.01f) * ((10f - GameManager.instance.textSpeed) / 10f)) + 0.01f;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void volumeSliderUpdate(float value){
        if (value == 0){
            volumeImage.SetActive(false);
            muteImage.SetActive(true);
        } else {
            volumeImage.SetActive(true);
            muteImage.SetActive(false);
        }
        float f = value/ 10f;
        if (MusicManagerScript.instance != null){
            MusicManagerScript.instance.updateVolume(f);
        }

        volumeText.text = "" + value;

        // Debug.Log(value);
    }

    public void textSpeedSliderUpdate(float value){
        if (GameManager.instance != null){
            GameManager.instance.textSpeed = value;
        }

        textSpeedText.text = "" + value;

        GameObject dia = GameObject.Find("DialogueRunner");
        if (dia != null){
            dia.GetComponent<DialogueUI>().textSpeed = ((0.05f - 0.01f) * ((10f - value) / 10f)) + 0.01f;
        }
    }
}
