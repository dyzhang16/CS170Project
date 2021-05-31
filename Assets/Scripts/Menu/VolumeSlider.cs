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
    // Start is called before the first frame update
    void Start()
    {
        if (MusicManagerScript.instance != null){
            this.GetComponent<Slider>().value = MusicManagerScript.instance.musicVolume*10;
            
            volumeText.text = "" + MusicManagerScript.instance.musicVolume*10;
        }
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

        //Debug.Log("The Volume value is: " + value);
    }
}
