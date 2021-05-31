using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class menusounds : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public AudioSource click_sound;
    public AudioSource hover_sound;

    public bool playOnce = true;

    // start is called before the first frame update
    void start()
    {
   

    }

    // update is called once per frame
    void Update()
    {
        if (GameManager.instance != null){
            click_sound.volume = GameManager.instance.sfxSound/40f;
            hover_sound.volume = GameManager.instance.sfxSound/40f;
        }
    }

    public void play_sound()
    {
        click_sound.Play();
    }

    public void OnPointerEnter(PointerEventData eventData){
        if (playOnce){
            hover_sound.Play();
            playOnce = false;
        }
    }

    public void OnPointerExit(PointerEventData eventData){
        playOnce = true;
    }
}
