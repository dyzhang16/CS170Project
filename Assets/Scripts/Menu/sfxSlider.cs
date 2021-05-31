using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class sfxSlider : MonoBehaviour
{
    public GameObject volumeImage;
    public GameObject muteImage;
    public Text sfxText;

    void Start()
    {
        if (SoundManagerScript.instance != null)
        {
            if (GameManager.instance != null)
            {
                this.GetComponent<Slider>().value = GameManager.instance.sfxSound;
            }
            sfxText.text = "" + GameManager.instance.sfxSound;
        }
    }

    public void sfxVolumeSliderUpdate(float value)
    {
        if (value == 0)
        {
            volumeImage.SetActive(false);
            muteImage.SetActive(true);
        }
        else
        {
            volumeImage.SetActive(true);
            muteImage.SetActive(false);
        }
        if (GameManager.instance != null)
        {
            GameManager.instance.sfxSound = value;
        }
        float f = value / 10f;
        if (SoundManagerScript.instance != null)
        {
            SoundManagerScript.instance.updateSfxVol(f);
        }

        sfxText.text = "" + value;

        //Debug.LogWarning("The SFX value is: " + value);
    }
}
