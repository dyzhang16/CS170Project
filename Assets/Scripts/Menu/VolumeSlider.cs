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
            this.GetComponent<Slider>().value = MusicManagerScript.instance.musicVolume;

            volumeText.text = "" + Mathf.Round(MusicManagerScript.instance.musicVolume * 100f) / 100f;
        }

        if (GameManager.instance != null){
            textSpeedSlider.value = GameManager.instance.textSpeed;

            textSpeedText.text = "" + Mathf.Round((0.06f - GameManager.instance.textSpeed) * 4000f) / 100f;

            GameObject dia = GameObject.Find("DialogueRunner");
            if (dia != null){
                dia.GetComponent<DialogueUI>().textSpeed = GameManager.instance.textSpeed;
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

        if (MusicManagerScript.instance != null){
            MusicManagerScript.instance.updateVolume(value);
        }

        volumeText.text = "" + Mathf.Round(value * 100f) / 100f;

        // Debug.Log(value);
    }

    public void textSpeedSliderUpdate(float value){
        if (GameManager.instance != null){
            GameManager.instance.textSpeed = value;
        }

        textSpeedText.text = "" + Mathf.Round((0.06f - value) * 2000f) / 100f;

        GameObject dia = GameObject.Find("DialogueRunner");
        if (dia != null){
            dia.GetComponent<DialogueUI>().textSpeed = value;
        }
    }
}
